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

namespace Hybrasyl.Xml.Objects;

public partial class CreatureCastable : IEquatable<CreatureCastable>
{
    public CreatureCastable(int interval, CreatureTargetPriority priority, string value)
    {
        Interval = interval;
        TargetPriority = priority;
        Value = value;
    }

    public bool Equals(CreatureCastable other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TargetPriority == other.TargetPriority && HealthPercentage == other.HealthPercentage 
                                                        && UseOnce == other.UseOnce && Interval == other.Interval && Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((CreatureCastable) obj);
    }

    public static bool operator ==(CreatureCastable lhs, CreatureCastable rhs)
    {
        return lhs switch
        {
            null when rhs is null => true,
            null => false,
            _ => lhs.Equals(rhs)
        };
    }

    public static bool operator !=(CreatureCastable lhs, CreatureCastable rhs) => !(lhs == rhs);


    public override int GetHashCode() => HashCode.Combine((int) TargetPriority, HealthPercentage, UseOnce, Interval, Value);
}