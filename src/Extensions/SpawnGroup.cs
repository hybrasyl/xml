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

/* Unmerged change from project 'Hybrasyl.Xml (net7.0)'
Before:
using System;
using System.Xml.Serialization;
After:
using Hybrasyl.Xml.Interfaces;
using System;
*/

using System.Xml.Serialization;
using Hybrasyl.Xml.Interfaces;

namespace Hybrasyl.Xml.Objects;

// For some reason xsd2code doesn't add this and it breaks spawngroup parsing
[XmlRoot(Namespace = "http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class SpawnGroup : ILoadOnStart<SpawnGroup>
{
    public override string PrimaryKey => Name;
    public ushort MapId { get; set; }

    [XmlIgnore] public SpawnStatus Status { get; set; }

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<SpawnGroup>.LoadAll(manager, path);

    public static SpawnGroup operator +(SpawnGroup sg1, SpawnGroup sg2)
    {
        var merged = sg1.Clone<SpawnGroup>();
        merged.Spawns.AddRange(sg2.Spawns);
        return merged;
    }
}