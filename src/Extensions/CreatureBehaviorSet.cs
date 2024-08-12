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

using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Hybrasyl.Xml.Objects;

public partial class CreatureBehaviorSet : IPostProcessable<CreatureBehaviorSet>, ILoadOnStart<CreatureBehaviorSet>, IAdditionalValidation<CreatureBehaviorSet>, IEquatable<CreatureBehaviorSet>
{
    [XmlIgnore]
    public List<string> LearnSkillCategories => string.IsNullOrEmpty(Castables?.SkillCategories)
        ? new List<string>()
        : Castables.SkillCategories.Trim().ToLower().Split(" ").ToList();

    [XmlIgnore]
    public List<string> LearnSpellCategories => string.IsNullOrEmpty(Castables?.SpellCategories)
        ? new List<string>()
        : Castables.SpellCategories.Trim().ToLower().Split(" ").ToList();

    public override string PrimaryKey => Name;

    [XmlIgnore] 
    public CreatureBehaviorSet BeforeImport { get; protected set;  } = null;

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<CreatureBehaviorSet>.LoadAll(manager, path);

    public static void ProcessAll(IWorldDataManager manager)
    {
        var ret = new XmlProcessResult();
        foreach (var import in manager.Find<CreatureBehaviorSet>(condition: x => !string.IsNullOrWhiteSpace(x.Import))
                     .ToList())
        {
            if (!manager.TryGetValue(import.Import, out CreatureBehaviorSet creatureBehaviorSet))
            {
                manager.FlagAsError(import, XmlError.ProcessingError,
                    $"{import.Filename}: Referenced import set {import.Import} not found");
                ret.Errors.Add(import.Guid, $"{import.Filename}: Referenced import set {import.Import} not found");
            }

            var newSet = import.Clone<CreatureBehaviorSet>(true);
            var resolved = newSet & creatureBehaviorSet;
            resolved.Name = import.Name;
            resolved.BeforeImport = import.Clone<CreatureBehaviorSet>();
            manager.Add(resolved, resolved.Name);
            ret.AdditionalCount++;
            ret.TotalProcessed++;
        }

        manager.UpdateResult<CreatureBehaviorSet>(ret);
    }

    public static void ValidateAll(IWorldDataManager manager)
    {
        var ret = new XmlProcessResult();
        foreach (var cbs in manager.Values<CreatureBehaviorSet>())
        {
            if (cbs.Castables?.Castable == null)
                ret.Errors.Add(cbs.Guid, "Creature behavior set has no castables");
        }
        manager.UpdateResult<CreatureBehaviorSet>(ret);
    }

    /// <summary>
    ///     Merge two behavior sets together
    /// </summary>
    /// <param name="cbs1">Target behavior set</param>
    /// <param name="cbs2">Source behavior set (import)</param>
    /// <returns></returns>
    public static CreatureBehaviorSet operator &(CreatureBehaviorSet cbs1, CreatureBehaviorSet cbs2)
    {
        // Usage: a & b
        // a is intended to be set with a defined import value (eg Import=)
        // b is the set referenced by the import value
        var newCbs = new CreatureBehaviorSet
        {
            Name = cbs1.Name,
            StatAlloc = string.IsNullOrEmpty(cbs1.StatAlloc) ? cbs2.StatAlloc : cbs1.StatAlloc,
            Behavior = new CreatureBehavior(),
            Castables = new CreatureCastables(),
            Import = cbs1.Import
        };

        newCbs.Behavior.CastingSets = new List<CreatureCastingSet>();
        newCbs.Castables.Castable = new List<string>();

        if (cbs1.Behavior != null)
        {
            newCbs.Behavior.CastingSets.AddRange(cbs1.Behavior.CastingSets);
            newCbs.Behavior.Hostility = cbs1.Behavior.Hostility;
            newCbs.Behavior.SetCookies = cbs1.Behavior.SetCookies;
        }

        if (cbs2.Behavior != null)
        {
            newCbs.Behavior.CastingSets.AddRange(cbs2.Behavior.CastingSets);
            newCbs.Behavior.Hostility = cbs2.Behavior.Hostility;
            newCbs.Behavior.SetCookies = cbs2.Behavior.SetCookies;
        }

        if (cbs1.Castables != null)
        {
            newCbs.Castables.Castable.AddRange(cbs1.Castables.Castable);
            if (!string.IsNullOrEmpty(cbs1.Castables.SkillCategories))
                newCbs.Castables.SkillCategories = cbs1.Castables.SkillCategories;
            if (!string.IsNullOrEmpty(cbs1.Castables.SpellCategories))
                newCbs.Castables.SpellCategories = cbs1.Castables.SpellCategories;
            newCbs.Castables.Auto = cbs1.Castables.Auto;
        }

        if (cbs2.Castables != null)
        {
            newCbs.Castables.Castable.AddRange(cbs2.Castables.Castable);
            if (!string.IsNullOrEmpty(cbs2.Castables.SkillCategories))
                newCbs.Castables.SkillCategories += $" {cbs2.Castables.SkillCategories}";
            if (!string.IsNullOrEmpty(cbs2.Castables.SpellCategories))
                newCbs.Castables.SpellCategories += $" {cbs2.Castables.SpellCategories}";
        }

        newCbs.Immunities = cbs1.Immunities == null ? cbs2.Immunities : cbs1.Immunities.Union(cbs2.Immunities).ToList();

        if (cbs1.StatModifiers == null) newCbs.StatModifiers = cbs2.StatModifiers;
        else if (cbs2.StatModifiers == null) newCbs.StatModifiers = cbs1.StatModifiers;
        else
        {
            // Both are null
            newCbs.StatModifiers = cbs1.StatModifiers + cbs2.StatModifiers;
        }
        return newCbs;
    }

    // Helper functions to determine immunities

    public bool ImmuneToElement(ElementType type, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.Element &&
            string.Equals(x.Value, type.ToString(), StringComparison.CurrentCultureIgnoreCase));
        return immunity != null;
    }

    public bool ImmuneToElement(string type, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.Element &&
            string.Equals(x.Value, type, StringComparison.CurrentCultureIgnoreCase));
        return immunity != null;
    }

    public bool ImmuneToCastableCategory(string category, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.CastableCategory &&
            string.Equals(x.Value, category, StringComparison.CurrentCultureIgnoreCase));
        return immunity != null;
    }

    public bool ImmuneToCastableCategories(IEnumerable<string> categories, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.CastableCategory &&
            categories.Contains(x.Value, StringComparer.OrdinalIgnoreCase));
        return immunity != null;
    }

    public bool ImmuneToStatusCategory(string category, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.StatusCategory &&
            string.Equals(x.Value, category, StringComparison.CurrentCultureIgnoreCase));
        return immunity != null;
    }

    public bool ImmuneToStatusCategories(IEnumerable<string> categories, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.StatusCategory &&
            categories.Contains(x.Value, StringComparer.OrdinalIgnoreCase));
        return immunity != null;
    }

    public bool ImmuneToStatus(string status, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.Status &&
            string.Equals(x.Value, status, StringComparison.CurrentCultureIgnoreCase));
        return immunity != null;
    }

    public bool ImmuneToStatus(Status status, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.Status &&
            string.Equals(x.Value, status.Name, StringComparison.CurrentCultureIgnoreCase));
        return immunity != null;
    }

    public bool ImmuneToCastable(string castable, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.Castable &&
            string.Equals(x.Value, castable, StringComparison.CurrentCultureIgnoreCase));
        return immunity != null;
    }

    public bool ImmuneToCastable(Castable castable, out CreatureImmunity immunity)
    {
        immunity = Immunities?.FirstOrDefault(predicate: x =>
            x.Type == CreatureImmunityType.Castable &&
            string.Equals(x.Value, castable.Name, StringComparison.CurrentCultureIgnoreCase));
        return immunity != null;
    }

    public bool Equals(CreatureBehaviorSet other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _statAlloc == other._statAlloc && Equals(_castables, other._castables) && 
               Equals(_behavior, other._behavior) && Equals(_immunities, other._immunities) && 
               Equals(_statModifiers, other._statModifiers) && _name == other._name;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((CreatureBehaviorSet) obj);
    }

    public static bool operator ==(CreatureBehaviorSet lhs, CreatureBehaviorSet rhs)
    {
        return lhs switch
        {
            null when rhs is null => true,
            null => false,
            _ => lhs.Equals(rhs)
        };
    }

    public static bool operator !=(CreatureBehaviorSet lhs, CreatureBehaviorSet rhs) => !(lhs == rhs);

    public override int GetHashCode() => HashCode.Combine(_statAlloc, _castables, _behavior, _immunities, _statModifiers, _name);


}