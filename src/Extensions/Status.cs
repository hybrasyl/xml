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

public partial class Status : ICategorizable, ILoadOnStart<Status>
{
    [Obsolete("This behavior is undesirable")]
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

    public override string PrimaryKey => Name;

    public override List<string> SecondaryKeys =>
        Name == null ? new List<string>() : new List<string> { Name.ToLower() };

    public List<string> CategoryList => Categories.Select(selector: x => x.Value).ToList();

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<Status>.LoadAll(manager, path);

    public bool IsCategory(string category) => CategoryList.Contains(category.Normalize().ToLower());
}

public partial class StatusHeal
{
    public bool IsSimple => string.IsNullOrEmpty(Formula);
    public bool IsEmpty => IsSimple && Simple.Value == 0 && Simple.Min == 0 && Simple.Max == 0;
}

public partial class StatusDamage
{
    public bool IsSimple => string.IsNullOrEmpty(Formula);
    public bool IsEmpty => IsSimple && Simple.Value == 0 && Simple.Min == 0 && Simple.Max == 0;
}