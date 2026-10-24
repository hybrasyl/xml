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
// (C) 2020-2026 ERISCO, LLC
//
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using System;
using System.Collections.Generic;
using System.Linq;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

/// <summary>
///     A species in the world data. Saves store <see cref="Key" /> and the key of a body, so neither changes. Clients
///     receive <see cref="Id" /> and the body <see cref="SpeciesBody.Id" />.
/// </summary>
public partial class Species : ILoadOnStart<Species>, IAdditionalValidation<Species>
{
    /// <summary>Compares species keys and body keys. Case does not matter.</summary>
    public static readonly StringComparer KeyComparer = StringComparer.OrdinalIgnoreCase;

    public override string PrimaryKey => Key;

    public override List<string> SecondaryKeys => Key == null ? [] : [Key.ToLowerInvariant()];

    public bool IsBranch => !string.IsNullOrEmpty(BranchOf);

    /// <summary>The body with <c>Default="true"</c>, or null for a species with no bodies.</summary>
    public SpeciesBody DefaultBody => Bodies?.FirstOrDefault(b => b.Default);

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<Species>.LoadAll(manager, path);

    public bool TryGetBody(string key, out SpeciesBody body)
    {
        body = null;
        if (string.IsNullOrEmpty(key) || Bodies == null) return false;
        foreach (var candidate in Bodies)
        {
            if (!KeyComparer.Equals(candidate.Key, key)) continue;
            body = candidate;
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Find a loaded species by key. Case does not matter, as for body keys. Use this, not the store key, because
    ///     the store compares primary keys with case.
    /// </summary>
    public static bool TryGet(IWorldDataManager manager, string key, out Species species)
    {
        species = null;
        return !string.IsNullOrEmpty(key) && manager.TryGetValueByIndex(key.ToLowerInvariant(), out species);
    }

    /// <summary>
    ///     The parent of a branch, or null when the species is not a branch or the parent is not loaded. A branch takes
    ///     only <see cref="Affinity" /> and <see cref="Resistance" /> from its parent.
    /// </summary>
    public Species GetParent(IWorldDataManager manager) => IsBranch && TryGet(manager, BranchOf, out var parent) ? parent : null;

    /// <summary>The affinity element key. A branch without its own affinity takes its parent's.</summary>
    public string GetAffinity(IWorldDataManager manager) =>
        string.IsNullOrEmpty(Affinity) ? GetParent(manager)?.Affinity : Affinity;

    /// <summary>The resistance element key. A branch without its own resistance takes its parent's.</summary>
    public string GetResistance(IWorldDataManager manager) =>
        string.IsNullOrEmpty(Resistance) ? GetParent(manager)?.Resistance : Resistance;

    /// <summary>
    ///     Run <see cref="Validate(IWorldDataManager)" /> on each species, and check the rules across species: keys that
    ///     differ only in case, unique species ids, and body ids that are unique across all species. The manager runs
    ///     it at load.
    /// </summary>
    public static void ValidateAll(IWorldDataManager manager)
    {
        var result = new XmlValidationResult();
        var all = manager.Values<Species>().ToList();
        var errors = all.ToDictionary(s => s.Guid, s => s.Validate(manager));

        // Add a message to each species that shares a value with another species.
        void Shared<T>(Func<Species, IEnumerable<T>> values, IEqualityComparer<T> comparer, Func<T, string> message)
        {
            var pairs = all.SelectMany(s => values(s).Where(v => v is not (null or "")).Distinct(comparer)
                .Select(v => (Species: s, Value: v)));
            foreach (var group in pairs.GroupBy(p => p.Value, comparer).Where(g => g.Count() > 1))
            foreach (var (species, _) in group)
                errors[species.Guid].Add(message(group.Key));
        }

        // Two keys that differ only in case have one lower-case index key, so a lookup finds either species. This is a
        // stopgap: when the store reports a case-only collision for every type (HXML-34), remove it.
        Shared<string>(s => [s.Key], KeyComparer, key => $"Species key {key} is not unique. Keys compare without case.");
        Shared<byte>(s => [s.Id], null, id => $"Species id {id} is not unique.");
        Shared<ushort>(s => (s.Bodies ?? []).Select(b => b.Id), null, id => $"Body id {id} is used by more than one species.");

        foreach (var species in all)
        {
            result.TotalProcessed++;
            if (errors[species.Guid].Count > 0)
                result.Errors.Add(species.Guid, $"Species {species.Key}: {string.Join(" ", errors[species.Guid])}");
        }

        manager.UpdateResult<Species>(result);
    }

    /// <summary>
    ///     Check the rules for one species that the schema cannot check, or that the loader does not check because it
    ///     does not validate files against the schema (HXML-35).
    /// </summary>
    /// <returns>One message for each problem. An empty list means the species is correct.</returns>
    public List<string> Validate(IWorldDataManager manager)
    {
        var errors = new List<string>();
        var bodies = Bodies ?? [];

        if (IsBadKey(Key))
            errors.Add("Key is empty, or it has a space at the start or the end.");
        if (Names == null)
            errors.Add("The species has no Names.");
        if (bodies.Count == 0 && PlayerOption is SpeciesPlayerOption.Starter or SpeciesPlayerOption.Unlockable)
            errors.Add($"A {PlayerOption} species has no bodies.");
        var defaults = bodies.Count(b => b.Default);
        if (bodies.Count > 0 && defaults != 1)
            errors.Add($"Exactly one body must have Default=\"true\". This species has {defaults}.");
        foreach (var body in bodies.Where(b => IsBadKey(b.Key)))
            errors.Add($"Body id {body.Id} has no key, or its key has a space at the start or the end.");
        foreach (var key in bodies.Select(b => b.Key).Duplicates(KeyComparer))
            errors.Add($"Body key {key} is not unique.");
        foreach (var id in bodies.Select(b => b.Id).Duplicates())
            errors.Add($"Body id {id} is not unique.");
        foreach (var body in bodies.Where(b => b.RetailSkinColor > 9))
            errors.Add($"Body {body.Key} has RetailSkinColor {body.RetailSkinColor}. The maximum is 9.");

        if (IsBranch)
        {
            if (PlayerOption == SpeciesPlayerOption.ByBranch)
                errors.Add("A branch cannot be ByBranch, because a branch cannot have branches.");
            var parent = GetParent(manager);
            if (parent == null)
                errors.Add($"BranchOf names {BranchOf}, which is not a loaded species.");
            else if (parent.IsBranch)
                errors.Add($"BranchOf names {BranchOf}, which is a branch itself.");
            else if (parent.PlayerOption != SpeciesPlayerOption.ByBranch)
                errors.Add($"BranchOf names {BranchOf}, which is not ByBranch.");
        }
        else
        {
            if (string.IsNullOrEmpty(Affinity))
                errors.Add("The species has no Affinity, and it is not a branch.");
            if (string.IsNullOrEmpty(Resistance))
                errors.Add("The species has no Resistance, and it is not a branch.");
        }

        return errors;
    }

    // Keys are saved, and the loader does not trim them, so " Dwarf " is a different key from "Dwarf". Validate
    // rejects such a key instead of trimming it, because a change to a saved key must be explicit.
    private static bool IsBadKey(string key) => string.IsNullOrWhiteSpace(key) || key.Trim() != key;

    /// <summary>
    ///     Check that <see cref="Affinity" /> and <see cref="Resistance" /> name elements of <paramref name="table" />.
    ///     The package cannot know the active element table, so the server calls this with it.
    /// </summary>
    /// <returns>One message for each problem. An empty list means the species is correct.</returns>
    public List<string> Validate(ElementTable table)
    {
        ArgumentNullException.ThrowIfNull(table);
        var errors = new List<string>();
        if (!string.IsNullOrEmpty(Affinity) && !table.TryGetElement(Affinity, out _))
            errors.Add($"Affinity {Affinity} is not an element of table {table.Name}.");
        if (!string.IsNullOrEmpty(Resistance) && !table.TryGetElement(Resistance, out _))
            errors.Add($"Resistance {Resistance} is not an element of table {table.Name}.");
        return errors;
    }
}
