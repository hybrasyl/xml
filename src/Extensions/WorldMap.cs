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
using System.Collections.Generic;
using System.Text;

namespace Hybrasyl.Xml.Objects;

public partial class WorldMap : ILoadOnStart<WorldMap>
{
    public override string PrimaryKey => Name;

    public new static void LoadAll(IWorldDataManager manager, string path)
    {
        HybrasylEntity<WorldMap>.LoadAll(manager, path);
        foreach (var wmap in manager.Values<WorldMap>())
            foreach (var point in wmap.Points)
                manager.Add(point, point.Id);
    }

    public byte[] GetBytes()
    {
        // Returns the representation of the worldmap as an array of bytes, 
        // suitable to passing to a map packet.

        var buffer = Encoding.ASCII.GetBytes(ClientMap);
        var bytes = new List<byte> { (byte)ClientMap.Length };

        bytes.AddRange(buffer);
        bytes.Add((byte)Points.Count);
        bytes.Add(0x00);

        foreach (var mappoint in Points) bytes.AddRange(mappoint.GetBytes());

        return bytes.ToArray();
    }
}