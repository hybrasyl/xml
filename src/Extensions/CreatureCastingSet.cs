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

namespace Hybrasyl.Xml.Objects;

public partial class CreatureCastingSet : IEquatable<CreatureCastingSet>
{
    public List<string> CategoryList => string.IsNullOrEmpty(Categories)
        ? new List<string>()
        : Categories.Trim().Split(" ").Select(selector: x => x.ToLower()).ToList();

    public bool Equals(CreatureCastingSet set)
    {
        if (set is null) return false;
        if (ReferenceEquals(this, set)) return true;
        if (GetType() != set.GetType()) return false;
        return set.Guid == Guid;
    }

    public override bool Equals(object obj) => Equals(obj as CreatureCastingSet);

    public static bool operator ==(CreatureCastingSet lhs, CreatureCastingSet rhs)
    {
        return lhs switch
        {
            null when rhs is null => true,
            null => false,
            _ => lhs.Equals(rhs)
        };
    }

    public static bool operator !=(CreatureCastingSet lhs, CreatureCastingSet rhs) => !(lhs == rhs);

    public override int GetHashCode()
    {
        if (Guid == Guid.Empty)
            Guid = Guid.NewGuid();
        return Guid.GetHashCode();
    }
}