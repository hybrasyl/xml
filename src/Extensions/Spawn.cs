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
using System.Linq;

namespace Hybrasyl.Xml.Objects;

public partial class Spawn
{
    public DateTime LastSpawn { get; set; } = default;
    public bool Disabled { get; set; } = false;
    public string ErrorMessage { get; set; } = string.Empty;

    public ElementType OffensiveElement
    {
        get
        {
            var ele = _damage.Elements.Count switch
            {
                1 => _damage.Elements.First(),
                > 1 => _damage.Elements.PickRandom(),
                _ => ElementType.None
            };
            return ele switch
            {
                ElementType.RandomExpanded => (ElementType) Random.Shared.Next(1, 10),
                ElementType.RandomTemuair => (ElementType) Random.Shared.Next(1, 7),
                _ => ele
            };
        }
    }

    public ElementType DefensiveElement
    {
        get
        {
            var ele = _defense.Elements.Count switch
            {
                1 => _damage.Elements.First(),
                > 1 => _damage.Elements.PickRandom(),
                _ => ElementType.None
            };
            return ele switch
            {
                ElementType.RandomExpanded => (ElementType) Random.Shared.Next(1, 10),
                ElementType.RandomTemuair => (ElementType) Random.Shared.Next(1, 7),
                _ => ele
            };
        }
    }
}