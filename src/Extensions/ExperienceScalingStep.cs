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

namespace Hybrasyl.Xml.Objects;

public partial class ExperienceScalingStep
{
    /// <summary>
    ///     Build a step in code. The generated properties only count when their Specified flag is
    ///     set, which deserialization does and an object initializer does not.
    /// </summary>
    /// <param name="min">The lowest level difference covered, or null for no lower bound.</param>
    /// <param name="max">The highest level difference covered, or null for no upper bound.</param>
    /// <param name="percent">The experience given, as a percentage of the reward.</param>
    /// <param name="fixedAmount">A fixed amount of experience, in place of percent.</param>
    public static ExperienceScalingStep Create(int? min, int? max, decimal percent = 100,
        uint? fixedAmount = null) => new()
    {
        Min = min ?? 0,
        MinSpecified = min.HasValue,
        Max = max ?? 0,
        MaxSpecified = max.HasValue,
        Percent = percent,
        Fixed = fixedAmount ?? 0,
        FixedSpecified = fixedAmount.HasValue
    };

    public bool Covers(int levelDifference) =>
        (!MinSpecified || levelDifference >= Min) && (!MaxSpecified || levelDifference <= Max);

    public uint Apply(uint experience)
    {
        if (FixedSpecified) return Fixed;
        var scaled = Math.Ceiling(experience * Percent / 100m);
        return (uint) Math.Clamp(scaled, 0m, uint.MaxValue);
    }
}
