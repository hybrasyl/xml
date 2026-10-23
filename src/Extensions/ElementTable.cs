// This file is part of Project Hybrasyl.
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Affero General Public License as published by
// the Free Software Foundation, version 3.
// 
// This program is distributed in the hope that it will be useful, but
// without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
// for more details.
// 
// You should have received a copy of the Affero General Public License along
// with this program. If not, see <http://www.gnu.org/licenses/>.
// 
// (C) 2020-2023 ERISCO, LLC
// 
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using System;
using System.Collections.Generic;
using System.Linq;
using Hybrasyl.Xml.Interfaces;

namespace Hybrasyl.Xml.Objects;

/// <remarks>
///     The element table defines the elements of a world. Content refers to an element by its key. Keys compare
///     without case.
/// </remarks>
public partial class ElementTable : ILoadOnStart<ElementTable>
{
    /// <summary>The prefix of a selector that picks a random member of a group, as in <c>Random:Cardinal</c>.</summary>
    public const string RandomPrefix = "Random:";

    /// <summary>Wire value for "no element" in a modern packet. No element can have this id.</summary>
    public const byte NoElementId = 255;

    /// <summary>The highest element id a retail client can display.</summary>
    public const byte MaxRetailId = 9;

    /// <summary>Selectors that need the state of a creature. The server resolves them, not the table.</summary>
    public static readonly IReadOnlyList<string> CreatureSelectors = ["Current", "Belt", "Necklace"];

    /// <summary>Old selector names, and the group that each one names now. Content can use them for one release.</summary>
    public static readonly IReadOnlyDictionary<string, string> LegacyRandomSelectors =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["RandomTemuair"] = "Cardinal",
            ["RandomExpanded"] = "Ordinal"
        };

    public override string PrimaryKey => Name;

    public new static void LoadAll(IWorldDataManager manager, string path = null) =>
        HybrasylEntity<ElementTable>.LoadAll(manager, path);

    /// <summary>The element with <c>Default="true"</c>. A creature without an element uses it.</summary>
    public ElementTableElement DefaultElement => Elements.FirstOrDefault(e => e.Default);

    public bool TryGetElement(string key, out ElementTableElement element)
    {
        element = string.IsNullOrEmpty(key) ? null : Elements.FirstOrDefault(e => KeyEquals(e.Key, key));
        return element != null;
    }

    public bool TryGetGroup(string key, out ElementTableGroup group)
    {
        group = string.IsNullOrEmpty(key) ? null : Groups.FirstOrDefault(g => KeyEquals(g.Key, key));
        return group != null;
    }

    /// <summary>
    ///     Get the damage multiplier for an attack of element <paramref name="source" /> against a defender of
    ///     element <paramref name="target" />.
    /// </summary>
    /// <remarks>
    ///     A table declares only the pairs it needs. A pair uses the Multiplier of its Target entry, then the
    ///     DefaultMultiplier of its Source row, then the <see cref="DefaultMultiplier" /> of the table.
    /// </remarks>
    public double GetMultiplier(string source, string target)
    {
        var row = Source.FirstOrDefault(s => KeyEquals(s.Element, source));
        var entry = row?.Target.FirstOrDefault(t => KeyEquals(t.Element, target));
        if (entry is { MultiplierSpecified: true }) return entry.Multiplier;
        return row is { DefaultMultiplierSpecified: true } ? row.DefaultMultiplier : DefaultMultiplier;
    }

    public static bool IsCreatureSelector(string selector) => CreatureSelectors.Any(s => KeyEquals(s, selector));

    /// <summary>
    ///     Resolve a selector from content to one element: an element key, <c>Random:&lt;Group&gt;</c>, or an
    ///     old name in <see cref="LegacyRandomSelectors" />.
    /// </summary>
    /// <returns>
    ///     False for an empty selector, an unknown key or group, an empty group, and a creature selector
    ///     (<see cref="CreatureSelectors" />), which the server resolves.
    /// </returns>
    public bool TryResolve(string selector, out ElementTableElement element)
    {
        element = null;
        if (string.IsNullOrEmpty(selector) || IsCreatureSelector(selector)) return false;

        string groupKey = null;
        if (selector.StartsWith(RandomPrefix, StringComparison.OrdinalIgnoreCase))
            groupKey = selector[RandomPrefix.Length..];
        else if (LegacyRandomSelectors.TryGetValue(selector, out var legacyGroup))
            groupKey = legacyGroup;

        if (groupKey == null) return TryGetElement(selector, out element);
        if (!TryGetGroup(groupKey, out var group) || group.Members.Count == 0) return false;
        return TryGetElement(group.Members.PickRandom(), out element);
    }

    /// <summary>
    ///     Check the rules that the schema cannot check, or that a table built in code can break.
    /// </summary>
    /// <returns>One message for each problem. An empty list means the table is correct.</returns>
    public List<string> Validate()
    {
        var errors = new List<string>();
        var reserved = CreatureSelectors.Concat(LegacyRandomSelectors.Keys).ToList();

        if (Elements.Count == 0)
            errors.Add("The table defines no elements.");

        foreach (var dup in Duplicates(Elements.Select(e => e.Key)))
            errors.Add($"Element key {dup} is not unique.");
        foreach (var dup in Elements.GroupBy(e => e.Id).Where(g => g.Count() > 1))
            errors.Add($"Element id {dup.Key} is not unique.");

        foreach (var element in Elements)
        {
            if (string.IsNullOrEmpty(element.Key))
                errors.Add($"Element id {element.Id} has no key.");
            else if (reserved.Any(r => KeyEquals(r, element.Key)))
                errors.Add($"Element key {element.Key} is reserved for a selector.");
            if (element.Id == NoElementId)
                errors.Add($"Element {element.Key} has id {NoElementId}, which means \"no element\".");
            if (element.RetailId > MaxRetailId)
                errors.Add($"Element {element.Key} has RetailId {element.RetailId}. The maximum is {MaxRetailId}.");
        }

        var defaults = Elements.Count(e => e.Default);
        if (defaults != 1)
            errors.Add($"Exactly one element must have Default=\"true\". This table has {defaults}.");

        foreach (var dup in Duplicates(Groups.Select(g => g.Key)))
            errors.Add($"Group key {dup} is not unique.");
        foreach (var group in Groups)
        {
            if (reserved.Any(r => KeyEquals(r, group.Key)))
                errors.Add($"Group key {group.Key} is reserved for a selector.");
            if (group.Members.Count == 0)
                errors.Add($"Group {group.Key} has no members.");
            foreach (var member in group.Members.Where(m => !TryGetElement(m, out _)))
                errors.Add($"Group {group.Key} names element {member}, which the table does not define.");
        }

        foreach (var dup in Duplicates(Source.Select(s => s.Element)))
            errors.Add($"Source element {dup} has more than one row.");
        foreach (var row in Source)
        {
            if (!TryGetElement(row.Element, out _))
                errors.Add($"Source row {row.Element} names an element that the table does not define.");
            foreach (var dup in Duplicates(row.Target.Select(t => t.Element)))
                errors.Add($"Source row {row.Element} has more than one Target for {dup}.");
            foreach (var target in row.Target.Where(t => !TryGetElement(t.Element, out _)))
                errors.Add($"Source row {row.Element} has a Target for {target.Element}, which the table does not define.");
        }

        return errors;
    }

    private static bool KeyEquals(string a, string b) => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);

    private static IEnumerable<string> Duplicates(IEnumerable<string> keys) =>
        keys.Where(k => !string.IsNullOrEmpty(k))
            .GroupBy(k => k, StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);
}