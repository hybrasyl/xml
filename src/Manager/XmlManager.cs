using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Objects;

namespace Hybrasyl.Xml.Manager;

public class XmlDataManager : IWorldDataManager
{

    private Dictionary<Type, MethodInfo> _loadableTypes = new();
    private Dictionary<Type, MethodInfo> _processableTypes = new();
    private Dictionary<Type, MethodInfo> _validatableTypes = new();

    private Dictionary<Type, dynamic> _dataStore = new();

    public XmlDataManager()
    {
        var ourType = typeof(ILoadOnStart<>);
        // find all classes that implement ILoadOnStart<T>
        var assembly = Assembly.GetExecutingAssembly();
        var targetTypes = assembly.GetTypes().Where(type => type.IsAssignableFrom(ourType) && type.IsClass).ToList();
        var storeType = typeof(XmlStore<>);
        foreach (var targetType in targetTypes)
        {
            // Gather our methods to run later
            var loadMethod = targetType.GetMethod("LoadAll", BindingFlags.Static | BindingFlags.Public);
            if (loadMethod != null)
                _loadableTypes.Add(targetType, loadMethod.MakeGenericMethod(targetType));
            var processMethod = targetType.GetMethod("ProcessAll", BindingFlags.Static | BindingFlags.Public);
            if (loadMethod != null)
                _loadableTypes.Add(targetType, loadMethod.MakeGenericMethod(targetType));
            var validateMethod = targetType.GetMethod("ValidateAll", BindingFlags.Static | BindingFlags.Public);
            if (loadMethod != null)
                _loadableTypes.Add(targetType, loadMethod.MakeGenericMethod(targetType));
        }
    }

    private XmlStore<T> GetStore<T>() where T : HybrasylEntity<T>
    {
        if (_dataStore.TryGetValue(typeof(T), out var dataStore) && dataStore is XmlStore<T> cast)
            return cast;
        return null;
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

    public void LoadAll<T>() where T : HybrasylEntity<T>, ILoadOnStart<T> => throw new NotImplementedException();
    public void ProcessAll<T>() where T : HybrasylEntity<T>, IPostProcessable<T> => throw new NotImplementedException();
    public void ValidateAll<T>() where T : HybrasylEntity<T>, IAdditionalValidationRequired<T> => throw new NotImplementedException();

    public IEnumerable<Castable> FindSkills(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null) => throw new NotImplementedException();

    public IEnumerable<Castable> FindSpells(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null) => throw new NotImplementedException();


    public string RootPath { get; set; }
    // TODO: static virtuals were supposed to be in C# 11 but aren't. This can be improved significantly once they are

    public void LoadAll()
    {
        foreach (var kvp in _loadableTypes)
        {

        }
    }
}

