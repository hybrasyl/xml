﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Objects;
using static System.Formats.Asn1.AsnWriter;
using Xunit;

namespace Hybrasyl.Xml.Manager;

public class XmlDataStore<T> : IWorldDataStore<T> where T : HybrasylEntity<T>
{
    private Dictionary<Guid, T> _store { get; set; } = new();
    private Dictionary<StoreKey, Guid> _index { get; set; } = new();
    private Dictionary<Guid, HashSet<StoreKey>> _reverseIndex { get; set; } = new();

    private HashSet<Guid> _errors { get; set; } = new HashSet<Guid>();

    public IEnumerable<T> Errors => _errors.Select(item => _store[item]).ToList();

    private object _lock { get; } = new();

    private Dictionary<string, HashSet<Guid>> _categories = new();

    public ILoadResult LoadResult { get; set; }
    public IProcessResult ProcessResult { get; set; }
    public IAdditionalValidationResult ValidationResult { get; set; }

    public void FlagAsError(Guid guid, XmlError type, string error)
    {
        if (!_store.TryGetValue(guid, out var value))
            throw new ArgumentException(
                "Guid {guid} does not exist and cannot be flagged as containing an error");
        lock (_lock)
        {
            _errors.Add(guid);
            value.Error = type;
            value.LoadErrorMessage = error;
        }
    }

    private void AddCategoriesToIndex(Guid entityGuid, params string[] categories)
    {
        foreach (var category in categories)
        {
            lock (_lock)
            {
                if (!_categories.TryGetValue(category, out var guid))
                    _categories[category] = new HashSet<Guid>();
                _categories[category].Add(entityGuid);
            }
        }
    }

    private void RemoveCategoriesFromIndex(Guid entityGuid, params string[] categories)
    {
        foreach (var category in categories)
        {
            lock (_lock)
            {
                if (!_categories.TryGetValue(category, out var guid))
                    return;
                _categories[category].Remove(entityGuid);
            }
        }
    }

    public void AddCategory(T entity, params string[] categories)
    {
        throw new NotImplementedException();
    }

    public void RemoveCategory(T entity, params string[] categories) {}

    public IEnumerable<T> GetByCategory(string category)
    {
        if (!_categories.TryGetValue(category, out var guids)) return new List<T>();
        var ret = guids.Select(guid => _store[guid]).ToList();
        return ret;
    }

    private void StoreItem(T entity, dynamic key, params dynamic[] indexes)
    {
        lock (_lock)
        {
            // If this is an overwrite, clean old entries up first & make sure the indexes are cleaned up
            if (_index.TryGetValue(GetStoreKey(key, true), out Guid existing))
            {
                foreach (var entry in _reverseIndex[existing])
                    _index.Remove(entry);
                _reverseIndex.Remove(existing);
                _store.Remove(existing);
            }

            _store[entity.Guid] = entity;
            _reverseIndex[entity.Guid] = new HashSet<StoreKey>();
            _index[GetStoreKey(key, true)] = entity.Guid;
            _reverseIndex[entity.Guid].Add(GetStoreKey(key, true));

            if (!string.IsNullOrWhiteSpace(entity.Filename))
            {
                _index[new StoreKey(entity.Filename, false)] = entity.Guid;
                _reverseIndex[entity.Guid].Add(new StoreKey(entity.Filename, false));
            }

            if (entity is ICategorizable categorizable)
                AddCategoriesToIndex(entity.Guid, categorizable.CategoryList.ToArray());

            foreach (var index in indexes)
            {
                var storeKey = new StoreKey(index, false);
                _index[storeKey] = entity.Guid;
                _reverseIndex[entity.Guid].Add(storeKey);
            }
        }
    }

    private bool DeleteItem(Guid guid, dynamic key)
    {
        lock (_lock)
        {
            if (!_store.TryGetValue(guid, out var entity)) return false;
            var ret = _store.Remove(guid) && _index.Remove(GetStoreKey(key, true));
            foreach (var index in _reverseIndex[guid])
            {
                _index.Remove(index);
            }
            ret = ret && _reverseIndex.Remove(guid);
            if (entity is ICategorizable categorizable)
                RemoveCategoriesFromIndex(guid, categorizable.CategoryList.ToArray());
            return ret;
        }
    }

    public T this[dynamic name]
    {
        get
        {
            if (name is Guid guid)
                return GetByGuid(guid);
            return Get(name);
        }
    }

    public IEnumerable<T> Find(Func<T, bool> condition) => _store.Values.Where(condition);

    private StoreKey GetStoreKey(dynamic key, bool isPrimaryKey = false) =>
        new StoreKey(key.ToString(), isPrimaryKey);

    public T Get(dynamic name) => _index.TryGetValue(new StoreKey(name, true), out Guid guid) ? _store[guid] : null;

    public T GetByGuid(Guid guid) => _store.TryGetValue(guid, out var ret) ? ret : null;

    public T GetByIndex(dynamic index) =>
        _index.TryGetValue(new StoreKey(index, false), out var guid) ? _store[guid] : null;

    public T GetByFilename(string filename) => GetByIndex(filename);

    public void Add(T entity)
    {
        if (entity is IIndexable i)
            AddWithIndex(entity, i.PrimaryKey, i.SecondaryKeys.ToArray());
    }

    public void Add(T entity, dynamic name) => StoreItem(entity, name);

    public void AddWithIndex(T entity, dynamic name, params dynamic[] indexes) => StoreItem(entity, name, indexes);

    public bool TryGetValue(dynamic key, out T result)
    {
        result = null;
        if (!_index.TryGetValue(GetStoreKey(key, true), out Guid guid)) return false;
        result = _store[guid];
        return true;
    }

    public bool TryGetValueByIndex(dynamic index, out T result)
    {
        result = null;
        if (!_index.TryGetValue(GetStoreKey(index, false), out Guid guid)) return false;
        result = _store[guid];
        return true;
    }

    public bool TryGetValueByFilename(string filename, out T result)
    {
        result = TryGetValueByIndex(filename, out var ret) ? ret : null;
        return result != null;
    }

    public IEnumerable<T> Items => _store.Values;
    public IReadOnlyCollection<StoreKey> Keys => _index.Keys;

    // TODO: make this less expensive
    public IEnumerable<KeyValuePair<HashSet<StoreKey>, T>> GetEnumerator() => _store.Select(kvp =>
        new KeyValuePair<HashSet<StoreKey>, T>(_reverseIndex[kvp.Key], kvp.Value));

    public int Count => _store.Count;

    public bool ContainsKey(dynamic name) => _index.ContainsKey(GetStoreKey(name, true));
    public bool ContainsIndex(dynamic index) => _index.ContainsKey(GetStoreKey(index));

    private void UpdateIndexOnRemove(Guid guid)
    {
        if (!_reverseIndex.TryGetValue(guid, out var indexes)) return;
        lock (_lock)
        {
            foreach (var index in indexes)
            {
                _reverseIndex[guid].Remove(GetStoreKey(index));
            }
        }
    }

    public bool Remove(dynamic name) =>
        _index.TryGetValue(GetStoreKey(name, true), out Guid guid) && DeleteItem(guid, name);

    public bool RemoveByIndex(dynamic index) => _index.TryGetValue(GetStoreKey(index, false), out Guid guid) &&
                                                DeleteItem(guid, index);

    public void Clear()
    {
        lock (_lock)
        {
            _reverseIndex = new Dictionary<Guid, HashSet<StoreKey>>();
            _store = new Dictionary<Guid, T>();
            _index = new Dictionary<StoreKey, Guid>();
        }
    }

    public bool Contains(T entity) => _store.ContainsKey(entity.Guid);

}
