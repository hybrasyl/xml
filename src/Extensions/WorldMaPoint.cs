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
            (byte)XQuadrant,
            (byte)XOffset,
            (byte)YQuadrant,
            (byte)YOffset,
            (byte)Name.Length
        };
        bytes.AddRange(buffer);
        bytes.AddRange(BitConverter.GetBytes(Id));

        return bytes.ToArray();
    }
}