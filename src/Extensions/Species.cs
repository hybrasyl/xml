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

    public override List<string> SecondaryKeys => Key == null ? [] : [Key.ToLower()];

    public bool IsBranch => !string.IsNullOrEmpty(BranchOf);

    /// <summary>The body with <c>Default="true"</c>, or null for a species with no bodies.</summary>
    public SpeciesBody DefaultBody => Bodies?.FirstOrDefault(b => b.Default);

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<Species>.LoadAll(manager, path);

    public bool TryGetBody(string key, out SpeciesBody body)
    {
        body = string.IsNullOrEmpty(key) ? null : Bodies?.FirstOrDefault(b => KeyComparer.Equals(b.Key, key));
        return body != null;
    }

    /// <summary>The parent of a branch, or null when the species is not a branch or the parent is not loaded.</summary>
    public Species GetParent(IWorldDataManager manager) =>
        IsBranch && manager.TryGetValueByIndex(BranchOf.ToLower(), out Species parent) ? parent : null;

    /// <summary>The affinity element key. A branch without its own affinity takes its parent's.</summary>
    public string GetAffinity(IWorldDataManager manager) =>
        string.IsNullOrEmpty(Affinity) ? GetParent(manager)?.Affinity : Affinity;

    /// <summary>The resistance element key. A branch without its own resistance takes its parent's.</summary>
    public string GetResistance(IWorldDataManager manager) =>
        string.IsNullOrEmpty(Resistance) ? GetParent(manager)?.Resistance : Resistance;

    /// <summary>
    ///     Run <see cref="Validate(IWorldDataManager)" /> on each species, and check the rules across species: unique
    ///     species ids, and body ids that are unique across all species. The manager runs it at load.
    /// </summary>
    public static void ValidateAll(IWorldDataManager manager)
    {
        var result = new XmlValidationResult();
        var all = manager.Values<Species>().ToList();
        var errors = all.ToDictionary(s => s.Guid, s => s.Validate(manager));

        foreach (var group in all.GroupBy(s => s.Id).Where(g => g.Count() > 1))
        foreach (var species in group)
            errors[species.Guid].Add($"Species id {group.Key} is not unique.");

        foreach (var group in all.SelectMany(s => (s.Bodies ?? []).Select(b => (Species: s, Body: b)))
                     .GroupBy(x => x.Body.Id).Where(g => g.Select(x => x.Species).Distinct().Count() > 1))
        foreach (var species in group.Select(x => x.Species).Distinct())
            errors[species.Guid].Add($"Body id {group.Key} is used by more than one species.");

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

        if (bodies.Count == 0 && PlayerOption is SpeciesPlayerOption.Starter or SpeciesPlayerOption.Unlockable)
            errors.Add($"A {PlayerOption} species has no bodies.");
        if (bodies.Count > 0 && bodies.Count(b => b.Default) != 1)
            errors.Add($"Exactly one body must have Default=\"true\". This species has {bodies.Count(b => b.Default)}.");
        foreach (var body in bodies.Where(b => string.IsNullOrEmpty(b.Key)))
            errors.Add($"Body id {body.Id} has no key.");
        foreach (var group in bodies.Where(b => !string.IsNullOrEmpty(b.Key)).GroupBy(b => b.Key, KeyComparer)
                     .Where(g => g.Count() > 1))
            errors.Add($"Body key {group.Key} is not unique.");
        foreach (var group in bodies.GroupBy(b => b.Id).Where(g => g.Count() > 1))
            errors.Add($"Body id {group.Key} is not unique.");
        foreach (var body in bodies.Where(b => b.RetailSkinColor > 9))
            errors.Add($"Body {body.Key} has RetailSkinColor {body.RetailSkinColor}. The maximum is 9.");

        if (IsBranch)
        {
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

    /// <summary>
    ///     Check that <see cref="Affinity" /> and <see cref="Resistance" /> name elements of <paramref name="table" />.
    ///     The package cannot know the active element table, so the server calls this with it.
    /// </summary>
    /// <returns>One message for each problem. An empty list means the species is correct.</returns>
    public List<string> Validate(ElementTable table)
    {
        var errors = new List<string>();
        if (!string.IsNullOrEmpty(Affinity) && !table.TryGetElement(Affinity, out _))
            errors.Add($"Affinity {Affinity} is not an element of table {table.Name}.");
        if (!string.IsNullOrEmpty(Resistance) && !table.TryGetElement(Resistance, out _))
            errors.Add($"Resistance {Resistance} is not an element of table {table.Name}.");
        return errors;
    }
}
