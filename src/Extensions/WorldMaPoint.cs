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
using System.Text;

namespace Hybrasyl.Xml.Objects;

public partial class WorldMapPoint
{
    public WorldMap Parent { get; set; }
    public int XOffset => X % 255;
    public int YOffset => Y % 255;
    public int XQuadrant => (X - XOffset) / 255;
    public int YQuadrant => (Y - YOffset) / 255;

    public string DestinationMap => Target.Value;
    public byte DestinationX => Target.X;
    public byte DestinationY => Target.Y;

    public long Id => HashCode.Combine(Name, X, Y, Parent);

    public byte[] GetBytes()
    {
        var buffer = Encoding.ASCII.GetBytes(Name);
        // X quadrant, offset, Y quadrant, offset, length of the name, the name, plus a 64-bit(?!) ID
        var bytes = new List<byte>
        {
            (byte) XQuadrant,
            (byte) XOffset,
            (byte) YQuadrant,
            (byte) YOffset,
            (byte) Name.Length
        };
        bytes.AddRange(buffer);
        bytes.AddRange(BitConverter.GetBytes(Id));

        return bytes.ToArray();
    }
}