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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hybrasyl.Xml.Enums;
After:
using Hybrasyl.Xml.Enums;
using Hybrasyl.Collections.Interfaces;
using System.Xml.Manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Hybrasyl.NET;
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Interfaces;

namespace Hybrasyl.Xml.Objects;

public partial class HybrasylEntity<T> : IIndexable where T : HybrasylEntity<T>
{
    private static readonly Pluralizer Pluralizer = new();
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string Filename => string.IsNullOrWhiteSpace(LoadPath) ? null : Path.GetFileName(LoadPath);
    public string LoadPath { get; set; }

    public XmlError Error { get; set; } = XmlError.None;
    public string LoadErrorMessage { get; set; } = string.Empty;
    public virtual string PrimaryKey => $"{typeof(T).Name}-{Filename}";
    public virtual List<string> SecondaryKeys => new();

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
        var subPath = Path.Join(targetDir, Pluralizer.Pluralize(typeof(T).Name).ToLower());

        foreach (var xmlFile in GetXmlFiles(subPath))
        {
            try
            {
                var entity = LoadFromFile(xmlFile);
                if (entity is not HybrasylEntity<T> hybrasylEntity)
                    throw new InvalidOperationException("Unsupported type {typeof(T).Name}");
                hybrasylEntity.LoadPath = xmlFile;
                manager.Add(entity);

                ret.SuccessCount++;
            }
            catch (Exception ex)
            {
                ret.Errors.Add(xmlFile, $"{ex.Message} {ex.InnerException?.Message}");
            }

            ret.TotalProcessed++;
        }

        manager.UpdateResult<T>(ret);
    }
}