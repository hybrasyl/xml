using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class HybrasylEntity<T>
{
    public Guid Guid { get; set; } = new Guid();
    public string Filename => string.IsNullOrWhiteSpace(LoadPath) ? null : Path.GetFileName(LoadPath);
    public string LoadPath { get; set; }

    public T Clone<T>() where T : HybrasylEntity<T>
    {
        var ms = new MemoryStream();
        var writer = new BsonDataWriter(ms);
        var reader = new BsonDataReader(ms);
        var serializer = new JsonSerializer();
        serializer.Serialize(writer, this);
        ms.Position = 0;
        var obj = serializer.Deserialize<T>(reader);
        ms.Close();
        if (obj == null) return null;
        obj.Guid = Guid;
        obj.LoadPath = LoadPath;
        return obj;
    }

    public static List<string> GetXmlFiles(string Path)
    {
        try
        {
            if (Directory.Exists(Path))
                return Directory.GetFiles(Path, "*.xml", SearchOption.AllDirectories)
                    .Where(predicate: x => !x.Contains(".ignore") || x.StartsWith("\\_")).ToList();
        }
        catch (Exception)
        {
            return null;
        }

        return new List<string>();
    }
    public static XmlLoadResult<T> LoadAll(IWorldDataManager manager) => throw new NotImplementedException();

}