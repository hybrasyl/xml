using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;
using Pluralize.NET;

namespace Hybrasyl.Xml.Objects;

public partial class HybrasylEntity<T> : IIndexable
{
    private static readonly Pluralizer Pluralizer = new Pluralizer();
    public Guid Guid { get; set; } = new Guid();
    public string Filename => string.IsNullOrWhiteSpace(LoadPath) ? null : Path.GetFileName(LoadPath);
    public string LoadPath { get; set; }
    public virtual string PrimaryKey => $"{typeof(T).Name}-{Filename}";
    public virtual List<string> SecondaryKeys => new();

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

    // C#11 was supposed to support virtual statics; eventually this can be redone with that support
    public static XmlLoadResult<T> LoadAll(string rootPath)
    {
        var ret = new XmlLoadResult<T>();
        foreach (var xmlFile in GetXmlFiles(Path.Join(rootPath, Pluralizer.Pluralize(typeof(T).Name.ToLower()))))
        {
            try
            {
                var entity = LoadFromFile(xmlFile);
                if (entity is not HybrasylEntity<T> hybrasylEntity) throw new InvalidOperationException("Unsupported type {typeof(T).Name}");
                hybrasylEntity.LoadPath = xmlFile;
                ret.Results.Add(entity);
                ret.TotalProcessed++;
            }
            catch (Exception ex)
            {
                ret.Errors.Add(xmlFile, ex.ToString());
            }
        }
        return ret;
    }

}