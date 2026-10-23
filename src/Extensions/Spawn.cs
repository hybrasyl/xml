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
using System.Xml.Serialization;

namespace Hybrasyl.Xml.Objects;

public partial class Spawn
{
    [XmlIgnore] public SpawnStatus Status { get; set; }

    public ElementType OffensiveElement => PickElement(Damage?.Elements);

    public ElementType DefensiveElement => PickElement(Defense?.Elements);

    // A spawn can have <Defense> without <Damage>, or the reverse, so each list can be null.
    private static ElementType PickElement(List<ElementType> elements)
    {
        var ele = elements?.Count switch
        {
            1 => elements.First(),
            > 1 => elements.PickRandom(),
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