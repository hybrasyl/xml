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

using Hybrasyl.Xml.Interfaces;
using System.Xml.Serialization;

namespace Hybrasyl.Xml.Objects;

public partial class SpawnGroup : ILoadOnStart<SpawnGroup>
{
    public override string PrimaryKey => Name;
    // The server sets the map when it loads the spawn groups. Spawns.xsd has no MapId.
    [XmlIgnore] public ushort MapId { get; set; }

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