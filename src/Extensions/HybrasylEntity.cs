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

using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;
using Pluralize.NET;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Hybrasyl.Xml.Objects;

public partial class HybrasylEntity<T> : IIndexable where T : HybrasylEntity<T>
{
    private static readonly Pluralizer Pluralizer = new();

    // Options for the JSON round-trip used by Clone(). Relaxed escaping: this payload is an
    // internal, in-memory deep-copy buffer (never persisted or transmitted), so the default
    // HTML-safe escaping would only add work. Round-trip correctness is unaffected either way.
    private static readonly JsonSerializerOptions CloneOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public Guid Guid { get; set; } = Guid.NewGuid();
    public string Filename => string.IsNullOrWhiteSpace(LoadPath) ? null : Path.GetFileName(LoadPath);
    public string LoadPath { get; set; }

    public XmlError Error { get; set; } = XmlError.None;
    public string LoadErrorMessage { get; set; } = string.Empty;
    public virtual string PrimaryKey => $"{typeof(T).Name}-{Filename}";
    public virtual List<string> SecondaryKeys => new();

    public T Clone<T>(bool newGuid = false) where T : HybrasylEntity<T>
    {
        // Deep-copy via a JSON round-trip. Serialize with the runtime type (GetType()) so
        // derived members are captured -- System.Text.Json serializes by the *declared* type
        // otherwise, and the declared type of `this` is the HybrasylEntity<T> base.
        var bytes = JsonSerializer.SerializeToUtf8Bytes(this, GetType(), CloneOptions);
        if (JsonSerializer.Deserialize(bytes, typeof(T), CloneOptions) is not T obj)
            return null;
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

    private static T DeserializeFromFile(string fileName)
    {
        // don't load as string first; just load via the deserializer; use StreamReader for utf-8
        // lenience
        using var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read,
            bufferSize: 4096, FileOptions.SequentialScan);
        using var sr = new StreamReader(fs);
        using var xr = XmlReader.Create(sr);
        return (T) SerializerXml.Deserialize(xr);
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
        var targetDir = rootPath ?? manager.RootPath;
        var subPath = Path.Join(targetDir, Pluralizer.Pluralize(typeof(T).Name).ToLower());

        var files = GetXmlFiles(subPath) ?? new List<string>();
 
       _ = SerializerXml;

        var errors = new ConcurrentDictionary<string, string>();
        var successCount = 0;
        var totalProcessed = 0;

        Parallel.ForEach(files, xmlFile =>
        {
            try
            {
                var entity = DeserializeFromFile(xmlFile);
                if (entity is not HybrasylEntity<T> hybrasylEntity)
                    throw new InvalidOperationException($"Unsupported type {typeof(T).Name}");
                hybrasylEntity.LoadPath = xmlFile;
                manager.Add(entity);
                Interlocked.Increment(ref successCount);
            }
            catch (Exception ex)
            {
                errors[xmlFile] = $"{ex.Message} {ex.InnerException?.Message}";
            }

            Interlocked.Increment(ref totalProcessed);
        });

        var ret = new XmlLoadResult
        {
            SuccessCount = successCount,
            TotalProcessed = totalProcessed,
            Errors = new Dictionary<string, string>(errors)
        };
        manager.UpdateResult<T>(ret);
    }
}
