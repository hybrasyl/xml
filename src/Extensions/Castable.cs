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

public partial class Castable : ILoadOnStart<Castable>, ICategorizable
{
    public int Id
    {
        get
        {
            unchecked
            {
                return 31 * (Name.GetHashCode() + 1);
            }
        }
    }

    // Helper functions to deal with xml vagaries
    public List<AddStatus> AddStatuses => Effects.Statuses?.Add ?? new List<AddStatus>();
    public List<RemoveStatus> RemoveStatuses => Effects.Statuses?.Remove ?? new List<RemoveStatus>();
    public List<CastableReactor> Reactors => Effects?.Reactors ?? new List<CastableReactor>();
    public byte CastableLevel { get; set; }

    public ElementType Element
    {
        get
        {
            return Elements.Count switch
            {
                1 => Elements.First(),
                > 1 => Elements.PickRandom(),
                _ => ElementType.None
            };
        }
    }

    public DateTime LastCast { get; set; }

    public bool IsSkill => Book is Book.PrimarySkill or Book.SecondarySkill or Book.UtilitySkill;
    public bool IsSpell => Book is Book.PrimarySpell or Book.SecondarySpell or Book.UtilitySpell;

    public bool OnCooldown => Cooldown > 0 && (DateTime.Now - LastCast).TotalSeconds < Cooldown;

    public override string PrimaryKey => Id.ToString();

    public override List<string> SecondaryKeys =>
        Name == null ? new List<string>() : new List<string> { Name, Name.ToLower() };

    public List<string> CategoryList
    {
        get
        {
            return Categories.Count > 0
                ? Categories.Select(selector: x => x.Value.ToLower()).ToList()
                : new List<string>();
        }
    }

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<Castable>.LoadAll(manager, path);

    public byte GetMaxLevelByClass(Class castableClass)
    {
        var maxLevelProperty = MaxLevel.GetType().GetProperty(castableClass.ToString());
        return (byte) (maxLevelProperty != null ? maxLevelProperty.GetValue(MaxLevel, null) : 0);
    }

    public bool TryGetMotion(Class castClass, out CastableMotion motion)
    {
        motion = null;

        if (Effects?.Animations?.OnCast?.Player == null) return false;

        var m = Effects.Animations.OnCast.Player.Where(predicate: e => e.Class.Contains(castClass));
        if (!m.Any())
            m = Effects.Animations.OnCast.Player.Where(predicate: e => e.Class.Count == 0);

        if (!m.Any())
            return false;
        motion = m.First();

        return true;
    }

    public bool IsCategory(string category) => CategoryList.Contains(category.Normalize().ToLower());
}

public class CastableComparer : IEqualityComparer<Castable>
{
    public bool Equals(Castable c1, Castable c2)
    {
        if (c1 == null && c2 == null) return true;
        if (c1 == null || c2 == null) return false;

        return string.Equals(c1.Name.Trim(), c2.Name.Trim(), StringComparison.CurrentCultureIgnoreCase) &&
               c1.Book == c2.Book;
    }

    public int GetHashCode(Castable c)
    {
        var hCode = $"{c.Name.Trim().ToLower()}-{c.Book}";
        return hCode.GetHashCode();
    }
}