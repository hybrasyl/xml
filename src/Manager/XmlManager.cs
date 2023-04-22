using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Objects;
using Pluralize.NET;

namespace Hybrasyl.Xml.Manager;

public class XmlDataManager : IWorldDataManager
{
    private Dictionary<Type, MethodInfo> _loadableTypes = new();
    private Dictionary<Type, MethodInfo> _processableTypes = new();
    private Dictionary<Type, MethodInfo> _validatableTypes = new();

    private Dictionary<Type, dynamic> _dataStore = new();

    private static readonly Pluralizer Pluralizer = new();

    public XmlDataManager(string rootPath)
    {
        RootPath = rootPath;
        var loadOnStartType = typeof(ILoadOnStart<>);
        // find all classes that implement ILoadOnStart<T>
        var assembly = Assembly.GetAssembly(loadOnStartType);
        var asdf = assembly.GetTypes();

        var targetTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == loadOnStartType));

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

    private XmlDataStore<T> GetStore<T>() where T : HybrasylEntity<T>
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
    public bool Remove<T>(dynamic name) where T : HybrasylEntity<T> => GetStore<T>().Remove(name);
    public bool RemoveByIndex<T>(dynamic index) where T : HybrasylEntity<T> => GetStore<T>().RemoveByIndex(index);
    public IEnumerable<T> Find<T>(Func<T, bool> condition) where T : HybrasylEntity<T> => GetStore<T>().Find(condition);

    public void ProcessAll<T>() where T : HybrasylEntity<T>, IPostProcessable<T> => throw new NotImplementedException();
    public void ValidateAll<T>() where T : HybrasylEntity<T>, IAdditionalValidationRequired<T> => throw new NotImplementedException();

    public IEnumerable<Castable> FindSkills(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null) => throw new NotImplementedException();

    public IEnumerable<Castable> FindSpells(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null) => throw new NotImplementedException();


    public string RootPath { get; set; }

    public void LoadAll()
    {
        // Makes heavy usage of reflection, which is a one-time performance hit (which is negligible
        // compared to the overhead of reading XML from disk). If you care about this, you can
        // use LoadAll<T> as needed.
        // TODO: Consider rewriting to use LoadAll<T> via reflection / async?
        //foreach (var kvp in _loadableTypes)
        //{
        //    var storeType = typeof(XmlDataStore<>);
        //    var categoryStoreType = typeof(XmlCategoryStore<>);
        //    var categorizingType = typeof(ICategorizable<>);
        //    if (kvp.Key.IsGenericType && kvp.Key.GetGenericTypeDefinition() == categorizingType)
        //    {
        //        var type = categoryStoreType.MakeGenericType(kvp.Key);
        //        _dataStore[type] = Activator.CreateInstance(type);
        //    }
        //    else
        //    {
        //        var type = storeType.MakeGenericType(kvp.Key);
        //        _dataStore[type] = Activator.CreateInstance(type);
        //    }

        //    var loadMethod = GetMethod("LoadAll", kvp.Key);
        //    if (loadMethod == null)
        //        return;
        //    dynamic result = loadMethod.Invoke(null, new object[] { this });


        //}

        // Find all ILoadOnStart types, and invoke LoadAll<T> for the types in question
        foreach (var kvp in _loadableTypes)
        {
            var method = typeof(XmlDataManager).GetMethods()
                .FirstOrDefault(x => x.Name == "LoadAll" && x.IsGenericMethod);
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
    }

    public void LoadAll<T>() where T : HybrasylEntity<T>, ILoadOnStart<T>
    {
        if (!_dataStore.TryGetValue(typeof(T), out _)) 
            _dataStore[typeof(T)] = new XmlDataStore<T>();

        if (_dataStore[typeof(T)] is not XmlDataStore<T> store) return;
        var result = T.LoadAll(Path.Join(RootPath, Pluralizer.Pluralize(typeof(T).Name)));

        foreach (var entity in result.Results)
        {
            if (entity.SecondaryKeys.Count > 0)
                AddWithIndex(entity, entity.PrimaryKey, entity.SecondaryKeys.ToArray());
            else
                Add(entity, entity.PrimaryKey);
    
            if (entity is ICategorizable<T> categoryEntity)
            {
                store.AddCategory(entity.PrimaryKey, categoryEntity.CategoryList.ToArray());
            }
        }


    }


}

