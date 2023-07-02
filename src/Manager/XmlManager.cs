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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Objects;
using Pluralize.NET;
using Serilog;

namespace Hybrasyl.Xml.Manager;

public class XmlDataManager : IWorldDataManager
{
    private static readonly Pluralizer Pluralizer = new();
    private static readonly SHA256 _sha256 = SHA256.Create();
    private readonly Dictionary<Type, dynamic> _dataStore = new();
    private readonly Dictionary<Type, MethodInfo> _loadableTypes = new();
    private readonly Dictionary<Type, MethodInfo> _processableTypes = new();
    private readonly Dictionary<Type, MethodInfo> _validatableTypes = new();

    public XmlDataManager(string rootPath)
    {
        RootPath = rootPath;
        var loadOnStartType = typeof(ILoadOnStart<>);
        // find all classes that implement ILoadOnStart<T>
        var assembly = Assembly.GetAssembly(loadOnStartType);

        var targetTypes = assembly.GetTypes()
            .Where(predicate: t => t.GetInterfaces()
                .Any(predicate: i => i.IsGenericType && i.GetGenericTypeDefinition() == loadOnStartType));

        var storeType = typeof(XmlDataStore<>);
        foreach (var targetType in targetTypes)
        {
            // Gather our methods to run later
            var loadMethod = targetType.GetMethod("LoadAll", BindingFlags.Static | BindingFlags.Public);
            if (loadMethod != null)
                _loadableTypes.Add(targetType, loadMethod);
            var processMethod = targetType.GetMethod("ProcessAll", BindingFlags.Static | BindingFlags.Public);
            if (processMethod != null)
                _processableTypes.Add(targetType, processMethod);
            var validateMethod = targetType.GetMethod("ValidateAll", BindingFlags.Static | BindingFlags.Public);
            if (validateMethod != null)
                _validatableTypes.Add(targetType, validateMethod);
        }
    }

    public XmlDataStore<T> GetStore<T>() where T : HybrasylEntity<T>
    {
        if (_dataStore.TryGetValue(typeof(T), out var dataStore) && dataStore is XmlDataStore<T> cast)
            return cast;
        _dataStore.Add(typeof(T), new XmlDataStore<T>());
        return _dataStore[typeof(T)] as XmlDataStore<T>;
    }

    public T Get<T>(dynamic name) where T : HybrasylEntity<T> => GetStore<T>().Get(name);
    public T GetByIndex<T>(dynamic index) where T : HybrasylEntity<T> => GetStore<T>().GetByIndex(index);
    public T GetByGuid<T>(Guid guid) where T : HybrasylEntity<T> => GetStore<T>().GetByGuid(guid);
    public void Add<T>(T entity, dynamic name) where T : HybrasylEntity<T> => GetStore<T>().Add(entity, name);
    public void Add<T>(T entity) where T : HybrasylEntity<T> => GetStore<T>().Add(entity);

    public void AddWithIndex<T>(T entity, dynamic name, params dynamic[] indexes) where T : HybrasylEntity<T> =>
        GetStore<T>().AddWithIndex(entity, name, indexes);

    public bool TryGetValue<T>(dynamic key, out T result) where T : HybrasylEntity<T> =>
        GetStore<T>().TryGetValue(key, out result);

    public bool TryGetValueByIndex<T>(dynamic index, out T result) where T : HybrasylEntity<T> =>
        GetStore<T>().TryGetValueByIndex(index, out result);

    public IEnumerable<T> Values<T>() where T : HybrasylEntity<T> => GetStore<T>().Values;
    public bool ContainsKey<T>(dynamic name) where T : HybrasylEntity<T> => GetStore<T>().ContainsKey(name);
    public bool ContainsIndex<T>(dynamic index) where T : HybrasylEntity<T> => GetStore<T>().ContainsIndex(index);
    public int Count<T>() where T : HybrasylEntity<T> => GetStore<T>().Count;
    public bool Remove<T>(T entity) where T : HybrasylEntity<T> => GetStore<T>().Remove(entity.PrimaryKey);
    public bool Remove<T>(dynamic name) where T : HybrasylEntity<T> => GetStore<T>().Remove(name);
    public bool RemoveByIndex<T>(dynamic index) where T : HybrasylEntity<T> => GetStore<T>().RemoveByIndex(index);
    public IEnumerable<T> Find<T>(Func<T, bool> condition) where T : HybrasylEntity<T> => GetStore<T>().Find(condition);

    public IEnumerable<Castable> FindSkills(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null) => FindCastables(str, @int, wis, con, dex, category, CastableFilter.SkillsOnly);

    public IEnumerable<Castable> FindSpells(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null) => FindCastables(str, @int, wis, con, dex, category, CastableFilter.SpellsOnly);

    public IEnumerable<T> FindByCategory<T>(string category) where T : HybrasylEntity<T>, ICategorizable =>
        GetStore<T>().FindByCategory(category);

    public IEnumerable<Item> FindItem(string name)
    {
        // Check for an exact result first
        var ret = new List<Item>();
        if (TryGetValue(name, out Item target) || TryGetValueByIndex(name, out target))
            ret.Add(target);
        else
            foreach (var gender in Enum.GetValues(typeof(Gender)))
            {
                var rawhash = $"{name.Normalize()}:{gender.ToString().Normalize()}";
                var hash = _sha256.ComputeHash(Encoding.ASCII.GetBytes(rawhash));
                if (TryGetValue(string.Concat(hash.Select(selector: b => b.ToString("x2"))).Substring(0, 8),
                        out Item result))
                    ret.Add(result);
            }

        return ret;
    }

    public string RootPath { get; set; }

    public bool IsReady { get; set; } 

    public async void LoadDataAsync()
    {
        if (!Directory.Exists(RootPath))
            throw new FileNotFoundException($"{RootPath} doesn't exist or is not readable");

        List<Task> tasks = new List<Task>();

        foreach (var kvp in _loadableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(predicate: x => x.Name == "LoadAll" && x.IsGenericMethod);
            if (method != null)
            {
                var genMethod = method.MakeGenericMethod(kvp.Key);
                tasks.Add(Task.Run(() => genMethod.Invoke(this, null)));
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        await Task.WhenAll(tasks);

        foreach (var kvp in _processableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(predicate: x => x.Name == "ProcessAll" && x.IsGenericMethod);
            if (method != null)
            {
                var genMethod = method.MakeGenericMethod(kvp.Key);
                genMethod.Invoke(this, null);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        foreach (var kvp in _validatableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(predicate: x => x.Name == "ValidateAll" && x.IsGenericMethod);
            if (method != null)
            {
                var genMethod = method.MakeGenericMethod(kvp.Key);
                genMethod.Invoke(this, null);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        IsReady = true;
    }

    public void LoadData()
    {
        if (!Directory.Exists(RootPath))
            throw new FileNotFoundException($"{RootPath} doesn't exist or is not readable");

        foreach (var kvp in _loadableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(predicate: x => x.Name == "LoadAll" && x.IsGenericMethod);
            if (method != null)
            {
                var genMethod = method.MakeGenericMethod(kvp.Key);
                genMethod.Invoke(this, null);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        foreach (var kvp in _processableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(predicate: x => x.Name == "ProcessAll" && x.IsGenericMethod);
            if (method != null)
            {
                var genMethod = method.MakeGenericMethod(kvp.Key);
                genMethod.Invoke(this, null);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        foreach (var kvp in _validatableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(predicate: x => x.Name == "ValidateAll" && x.IsGenericMethod);
            if (method != null)
            {
                var genMethod = method.MakeGenericMethod(kvp.Key);
                genMethod.Invoke(this, null);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        IsReady = true;
    }

    public void LogResult(ILogger log)
    {
        foreach (var kvp in _loadableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(predicate: x => x.Name == "GetLoadResult");
            if (method == null) continue;
            var genMethod = method.MakeGenericMethod(kvp.Key);
            var value = genMethod.Invoke(this, null);
            if (value is not ILoadResult loadResult) continue;
            log.Information("{Type}: Loaded {processed}, with {error} error(s) ({success} successfully loaded)",
                kvp.Key.Name, loadResult.TotalProcessed, loadResult.ErrorCount, loadResult.SuccessCount);
            if (loadResult.ErrorCount <= 0) continue;
            log.Error("{Type}: Load errors follow", kvp.Key.Name);
            foreach (var err in loadResult.Errors)
                log.Error("  {filename}: {error}", err.Key, err.Value);
        }

        foreach (var kvp in _processableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(predicate: x => x.Name == "GetProcessResult");
            if (method == null) continue;
            var genMethod = method.MakeGenericMethod(kvp.Key);
            var value = genMethod.Invoke(this, null);            
            if (value is not IProcessResult loadResult) continue;
            log.Information(
                "{Type}: Processing: {processed} processed with {error} error(s) ({Additional} additional {Type}(s) from processing)",
                kvp.Key.Name, loadResult.TotalProcessed, loadResult.ErrorCount, loadResult.AdditionalCount,
                kvp.Key.Name);
            if (loadResult.ErrorCount <= 0) continue;
            log.Error("{Type}: Processing errors follow", kvp.Key.Name);
            foreach (var err in loadResult.Errors)
                log.Error("  {guid}: {error}", GetFilenameByGuidDynamic(err.Key, kvp.Key), err.Value);
        }

        foreach (var kvp in _validatableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(predicate: x => x.Name == "GetProcessResult");
            if (method == null) continue;
            var genMethod = method.MakeGenericMethod(kvp.Key);
            var value = genMethod.Invoke(this, null);
            if (value is not IValidationResult loadResult) continue;
            log.Information("{Type}: Validation: {processed} validated with {error} error(s)",
                kvp.Key.Name, loadResult.TotalProcessed, loadResult.ErrorCount);
            if (loadResult.ErrorCount <= 0) continue;
            log.Error("{Type}: Validation errors follow", kvp.Key.Name);
            foreach (var err in loadResult.Errors)
                log.Error("  {guid}: {error}", GetFilenameByGuidDynamic(err.Key, kvp.Key), err.Value);
        }
    }

    private string GetFilenameByGuidDynamic(Guid guid, Type target)
    {
        var method = typeof(XmlDataManager).GetMethods()
            .FirstOrDefault(predicate: x => x.Name == "GetByGuid" && x.IsGenericMethod);
        if (method == null) return "err";
        var genMethod = method.MakeGenericMethod(target);
        var o = genMethod.Invoke(this, new object[] {guid});
        var oAsType = Convert.ChangeType(o, target);
        if (oAsType == null) return string.Empty;
        var property = target.GetProperties().FirstOrDefault(predicate: x => x.Name == "Filename");
        return property?.GetValue(oAsType)?.ToString() ?? "err";
    }


    public void LoadAll<T>() where T : HybrasylEntity<T>, ILoadOnStart<T> => T.LoadAll(this);

    public void ProcessAll<T>() where T : HybrasylEntity<T>, IPostProcessable<T> => T.ProcessAll(this);

    public void ValidateAll<T>() where T : HybrasylEntity<T>, IAdditionalValidation<T> => T.ValidateAll(this);

    public void FlagAsError<T>(T entity, XmlError error, string message) where T : HybrasylEntity<T> =>
        GetStore<T>().FlagAsError(entity.Guid, error, message);

    public async void LoadAllAsync<T>() where T : HybrasylEntity<T>, ILoadOnStart<T>
    {
        await Task.Run(() => T.LoadAll(this));
    }

    public void UpdateResult<T>(ILoadResult result) where T : HybrasylEntity<T> =>
        GetStore<T>().LoadResult = result;

    public void UpdateResult<T>(IProcessResult result) where T : HybrasylEntity<T> =>
        GetStore<T>().ProcessResult = result;

    public void UpdateResult<T>(IValidationResult result) where T : HybrasylEntity<T> =>
        GetStore<T>().ValidationResult = result;

    public IValidationResult GetValidationResult<T>() where T : HybrasylEntity<T> =>
        GetStore<T>().ValidationResult;

    public ILoadResult GetLoadResult<T>() where T : HybrasylEntity<T> => GetStore<T>().LoadResult;
    public IProcessResult GetProcessResult<T>() where T : HybrasylEntity<T> => GetStore<T>().ProcessResult;


    public HashSet<Castable> FindCastables(long Str = 0, long Int = 0, long Wis = 0,
        long Con = 0, long Dex = 0, string category = null,
        CastableFilter filter = CastableFilter.SkillsAndSpells)
    {
        HashSet<Castable> ret;
        if (!string.IsNullOrEmpty(category))
        {
            var sanitized = Sanitize(category);
            ret = GetStore<Castable>().FindByCategory(sanitized).ToHashSet(new CastableComparer());
        }
        else
        {
            ret = Values<Castable>().ToHashSet(new CastableComparer());
        }

        if (Str > 0 || Int > 0 || Wis > 0 || Con > 0 || Dex > 0)
            // TODO: perhaps cache this information
            foreach (var castable in ret)
            {
                if (castable.Requirements.Count == 0) continue;
                var physreq = castable.Requirements.Where(predicate: x => x.Physical != null);
                if (!physreq.Any()) continue;
                foreach (var req in physreq)
                    if (Str >= req.Physical.Str && Int >= req.Physical.Int && Wis >= req.Physical.Wis &&
                        Con >= req.Physical.Con &&
                        Dex >= req.Physical.Dex)
                        continue;
                    else
                        ret.Remove(castable);
            }

        if (filter == CastableFilter.SkillsOnly)
            ret = ret.Where(predicate: c => c.IsSkill).ToHashSet();
        if (filter == CastableFilter.SpellsOnly)
            ret = ret.Where(predicate: c => c.IsSpell).ToHashSet();
        return ret;
    }

    private static string Sanitize(dynamic key) => key.ToString().Normalize().ToLower();

    private void AddToStore<T>(T entity) where T : HybrasylEntity<T>
    {
        var store = GetStore<T>();

        if (entity.SecondaryKeys.Count > 0)
            AddWithIndex(entity, entity.PrimaryKey, entity.SecondaryKeys.ToArray());
        else
            Add(entity, entity.PrimaryKey);
    }
}