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
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

/// <remarks>
///     The element table defines the elements of a world. Content refers to an element by its key. At run time,
///     keys compare without case (<see cref="KeyComparer" />). The schema compares them with case, so content must
///     use the case of the key, and <see cref="Validate" /> rejects two keys that differ only in case.
/// </remarks>
public partial class ElementTable : ILoadOnStart<ElementTable>, IAdditionalValidation<ElementTable>
{
    /// <summary>The prefix of a selector that picks a random member of a group, as in <c>Random:Cardinal</c>.</summary>
    public const string RandomPrefix = "Random:";

    /// <summary>Wire value for "no element" in a modern packet. No element can have this id.</summary>
    public const byte NoElementId = 255;

    // The highest element id a retail client can display (the schema's maxInclusive for RetailId).
    private const byte MaxRetailId = 9;

    /// <summary>Compares element keys, group keys and selectors.</summary>
    public static readonly StringComparer KeyComparer = StringComparer.OrdinalIgnoreCase;

    private static readonly string[] CreatureSelectorKeys = ["Current", "Belt", "Necklace"];

    /// <summary>Selectors that need the state of a creature. The server resolves them, not the table.</summary>
    public static readonly IReadOnlyList<string> CreatureSelectors = CreatureSelectorKeys;

    /// <summary>Old selector names, and the group that each one names now. Content can use them for one release.</summary>
    /// <remarks>
    ///     The world must define these groups. Before the element table defined the elements, RandomTemuair picked
    ///     by enum position and so also picked Light and Dark. It now picks only from the Cardinal group.
    /// </remarks>
    public static readonly IReadOnlyDictionary<string, string> LegacyRandomSelectors =
        new Dictionary<string, string>(KeyComparer)
        {
            ["RandomTemuair"] = "Cardinal",
            ["RandomExpanded"] = "Ordinal"
        };

    public override string PrimaryKey => Name;

    public new static void LoadAll(IWorldDataManager manager, string path = null) =>
        HybrasylEntity<ElementTable>.LoadAll(manager, path);

    /// <summary>Run <see cref="Validate" /> on each loaded table, and store the errors as the validation result.</summary>
    public static void ValidateAll(IWorldDataManager manager)
    {
        var result = new XmlValidationResult();
        foreach (var table in manager.Values<ElementTable>())
        {
            result.TotalProcessed++;
            var errors = table.Validate();
            if (errors.Count > 0)
                result.Errors.Add(table.Guid, $"Element table {table.Name}: {string.Join(" ", errors)}");
        }

        manager.UpdateResult<ElementTable>(result);
    }

    /// <summary>The element with <c>Default="true"</c>. A creature without an element uses it.</summary>
    public ElementTableElement DefaultElement => Elements?.FirstOrDefault(e => e.Default);

    public bool TryGetElement(string key, out ElementTableElement element)
    {
        element = null;
        if (string.IsNullOrEmpty(key) || Elements == null) return false;
        foreach (var candidate in Elements)
        {
            if (!KeyComparer.Equals(candidate.Key, key)) continue;
            element = candidate;
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Get the damage multiplier for an attack of element <paramref name="source" /> against a defender of
    ///     element <paramref name="target" />.
    /// </summary>
    /// <remarks>
    ///     A table declares only the pairs it needs. A pair uses the Multiplier of its Target entry, then the
    ///     DefaultMultiplier of its Source row, then the <see cref="DefaultMultiplier" /> of the table.
    /// </remarks>
    public double GetMultiplier(ElementTableElement source, ElementTableElement target) =>
        GetMultiplier(source?.Key, target?.Key);

    /// <summary>
    ///     Get the multiplier for two element keys. A selector is not a key: it matches no row and so gets the
    ///     default. Resolve a selector with <see cref="TryResolve" />, or use the overload for elements.
    /// </summary>
    public double GetMultiplier(string source, string target)
    {
        var row = Source?.FirstOrDefault(s => KeyComparer.Equals(s.Element, source));
        var entry = row?.Target?.FirstOrDefault(t => KeyComparer.Equals(t.Element, target));
        if (entry is { MultiplierSpecified: true }) return entry.Multiplier;
        return row is { DefaultMultiplierSpecified: true } ? row.DefaultMultiplier : DefaultMultiplier;
    }

    public static bool IsCreatureSelector(string selector)
    {
        foreach (var key in CreatureSelectorKeys)
            if (KeyComparer.Equals(key, selector))
                return true;
        return false;
    }

    /// <summary>
    ///     Resolve a selector from content to one element: an element key, <c>Random:&lt;Group&gt;</c>, or an
    ///     old name in <see cref="LegacyRandomSelectors" />. A group member that the table does not define is
    ///     never picked. Each entry in <c>Members</c> is one chance, so a member listed twice is picked twice as
    ///     often. Content uses this to weight a group.
    /// </summary>
    /// <returns>
    ///     False for an empty selector, an unknown key or group, a group with no defined member, and a creature
    ///     selector (<see cref="CreatureSelectors" />), which the server resolves.
    /// </returns>
    public bool TryResolve(string selector, out ElementTableElement element)
    {
        element = null;
        if (string.IsNullOrEmpty(selector) || IsCreatureSelector(selector)) return false;
        if (!TryGetGroupKey(selector, out var groupKey)) return TryGetElement(selector, out element);
        if (!TryGetGroup(groupKey, out var group) || group.Members == null) return false;

        // Reservoir sampling: the nth defined entry is kept with probability 1/n, so the pick is uniform over the
        // defined entries, in one pass and without a list. A repeated member has more entries, and more weight.
        var defined = 0;
        foreach (var key in group.Members)
            if (TryGetElement(key, out var candidate) && Random.Shared.Next(++defined) == 0)
                element = candidate;
        return element != null;
    }

    /// <summary>
    ///     Check that a selector from content can resolve: a creature selector, a group with at least one defined
    ///     member, or an element key. The server can check content with it at load.
    /// </summary>
    public bool CanResolve(string selector) => IsCreatureSelector(selector) || TryResolve(selector, out _);

    /// <summary>
    ///     Check the rules that the schema cannot check, or that a table built in code can break.
    /// </summary>
    /// <returns>One message for each problem. An empty list means the table is correct.</returns>
    public List<string> Validate()
    {
        var errors = new List<string>();
        var elements = Elements ?? [];
        var groups = Groups ?? [];
        var rows = Source ?? [];
        var reserved = CreatureSelectors.Concat(LegacyRandomSelectors.Keys).ToList();

        if (elements.Count == 0)
            errors.Add("The table defines no elements.");

        foreach (var dup in elements.Select(e => e.Key).Duplicates(KeyComparer))
            errors.Add($"Element key {dup} is not unique.");
        foreach (var dup in elements.Select(e => e.Id).Duplicates(EqualityComparer<byte>.Default))
            errors.Add($"Element id {dup} is not unique.");

        foreach (var element in elements)
        {
            if (string.IsNullOrEmpty(element.Key))
                errors.Add($"Element id {element.Id} has no key.");
            else if (reserved.Contains(element.Key, KeyComparer))
                errors.Add($"Element key {element.Key} is reserved for a selector.");
            if (element.Id == NoElementId)
                errors.Add($"Element {element.Key} has id {NoElementId}, which means \"no element\".");
            if (element.RetailId > MaxRetailId)
                errors.Add($"Element {element.Key} has RetailId {element.RetailId}. The maximum is {MaxRetailId}.");
        }

        var defaults = elements.Count(e => e.Default);
        if (defaults != 1)
            errors.Add($"Exactly one element must have Default=\"true\". This table has {defaults}.");

        foreach (var dup in groups.Select(g => g.Key).Duplicates(KeyComparer))
            errors.Add($"Group key {dup} is not unique.");
        foreach (var group in groups)
        {
            var members = group.Members ?? [];
            if (string.IsNullOrEmpty(group.Key))
                errors.Add("A group has no key.");
            else if (reserved.Contains(group.Key, KeyComparer))
                errors.Add($"Group key {group.Key} is reserved for a selector.");
            if (members.Count == 0)
                errors.Add($"Group {group.Key} has no members.");
            foreach (var member in members.Where(m => !TryGetElement(m, out _)))
                errors.Add($"Group {group.Key} names element {member}, which the table does not define.");
        }

        if (rows.Count == 0)
            errors.Add("The table has no Source rows.");
        foreach (var dup in rows.Select(s => s.Element).Duplicates(KeyComparer))
            errors.Add($"Source element {dup} has more than one row.");
        foreach (var row in rows)
        {
            var targets = row.Target ?? [];
            if (!TryGetElement(row.Element, out _))
                errors.Add($"Source row {row.Element} names an element that the table does not define.");
            foreach (var dup in targets.Select(t => t.Element).Duplicates(KeyComparer))
                errors.Add($"Source row {row.Element} has more than one Target for {dup}.");
            foreach (var target in targets.Where(t => !TryGetElement(t.Element, out _)))
                errors.Add($"Source row {row.Element} has a Target for {target.Element}, which the table does not define.");
        }

        return errors;
    }

    // A Random:<Group> selector, or an old name for one, gives the key of its group.
    private static bool TryGetGroupKey(string selector, out string groupKey)
    {
        if (selector.StartsWith(RandomPrefix, StringComparison.OrdinalIgnoreCase))
            groupKey = selector[RandomPrefix.Length..];
        else
            LegacyRandomSelectors.TryGetValue(selector, out groupKey);
        return groupKey != null;
    }

    private bool TryGetGroup(string key, out ElementTableGroup group)
    {
        group = string.IsNullOrEmpty(key) ? null : Groups?.FirstOrDefault(g => KeyComparer.Equals(g.Key, key));
        return group != null;
    }

}