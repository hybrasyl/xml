using System.Collections.Generic;
using System;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class WorldMap : ILoadOnStart<WorldMap>
{
    public override string PrimaryKey => Name;

    public new static void LoadAll(IWorldDataManager manager, string path)
    {
        var ret = HybrasylEntity<WorldMap>.LoadAll(manager, path);
        foreach (var wmap in manager.Values<WorldMap>())
        {
            foreach (var point in wmap.Points)
            {
                manager.Add<WorldMapPoint>(point, point.Id);
            }
        }
        manager.UpdateStatus<WorldMap>(ret);
    }

    public byte[] GetBytes()
    {
        // Returns the representation of the worldmap as an array of bytes, 
        // suitable to passing to a map packet.

        var buffer = Encoding.ASCII.GetBytes(ClientMap);
        var bytes = new List<byte> { (byte) ClientMap.Length };

        bytes.AddRange(buffer);
        bytes.Add((byte) Points.Count);
        bytes.Add(0x00);

        foreach (var mappoint in Points) bytes.AddRange(mappoint.GetBytes());

        return bytes.ToArray();
    }

}