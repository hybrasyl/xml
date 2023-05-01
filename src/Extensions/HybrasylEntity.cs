using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;
using Pluralize.NET;
using System.Threading.Tasks;

namespace Hybrasyl.Xml.Objects;

public partial class HybrasylEntity<T> : IIndexable where T : HybrasylEntity<T>
{
    private static readonly Pluralizer Pluralizer = new Pluralizer();
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string Filename => string.IsNullOrWhiteSpace(LoadPath) ? null : Path.GetFileName(LoadPath);
    public string LoadPath { get; set; }
    public virtual string PrimaryKey => $"{typeof(T).Name}-{Filename}";
    public virtual List<string> SecondaryKeys => new();

    public XmlError Error { get; set; } = XmlError.None;
    public string LoadErrorMessage { get; set; } = string.Empty;

    public T Clone<T>(bool newGuid = false) where T : HybrasylEntity<T>
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
        obj.Guid = newGuid ? Guid.NewGuid() : Guid;
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

    public static async Task<T> LoadFromFileAsync(string fileName)
    {
        await using var file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        using var sr = new StreamReader(file);
        var dataString = await sr.ReadToEndAsync();
        sr.Close();
        file.Close();
        return Deserialize(dataString);
    }

    public static async Task<XmlLoadResult> LoadAllAsync(IWorldDataManager manager, string rootPath)
    {
        var ret = new XmlLoadResult();
        foreach (var xmlFile in GetXmlFiles(rootPath ?? manager.RootPath))
        {
            try
            {
                var entity = await LoadFromFileAsync(xmlFile);
                if (entity is not HybrasylEntity<T> hybrasylEntity)
                    throw new InvalidOperationException("Unsupported type {typeof(T).Name}");
                hybrasylEntity.LoadPath = xmlFile;
                ret.SuccessCount++;
            }
            catch (Exception ex)
            {
                ret.Errors.Add(xmlFile, ex.ToString());
            }
            ret.TotalProcessed++;

        }

        return ret;
    }

    // C#11 was supposed to support virtual statics; eventually this can be redone with that support
    public static void LoadAll(IWorldDataManager manager, string rootPath)
    {
        var ret = new XmlLoadResult();
        var targetDir = rootPath ?? manager.RootPath;
        var subPath = Path.Join(targetDir, Pluralizer.Pluralize(typeof(T).Name)).ToLower();
        //throw new InvalidOperationException($"subdir is {subPath} targetdir {targetDir}");
        foreach (var xmlFile in GetXmlFiles(subPath))
        {
            try
            {
                var entity = LoadFromFile(xmlFile);
                if (entity is not HybrasylEntity<T> hybrasylEntity) throw new InvalidOperationException("Unsupported type {typeof(T).Name}");
                hybrasylEntity.LoadPath = xmlFile;
                if (entity is IIndexable indexable)
                {
                    manager.AddWithIndex(entity, indexable.PrimaryKey, indexable.SecondaryKeys.ToArray());
                }

                if (entity is ICategorizable<T> categorizable)
                {
                    manager.GetStore<T>().AddCategory(categorizable.Name, categorizable.CategoryList.ToArray());
                }
                ret.SuccessCount++;

            }
            catch (Exception ex)
            {
                ret.Errors.Add(xmlFile, ex.ToString());
            }
            ret.TotalProcessed++;
        }
        manager.UpdateStatus<T>(ret);
    }

}