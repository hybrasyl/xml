﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;

namespace Hybrasyl.Xml.Interfaces
{
    public interface IWorldDataStore<T> where T : HybrasylEntity<T>
    {
        public IEnumerable<T> Errors { get; }
        public int ErrorCount => Errors.Count();
        public int ItemCount => Items.Count();

        public IAdditionalValidationResult ValidationResult { get; }
        public ILoadResult LoadResult { get; }
        public IProcessResult ProcessResult { get; }
        public IEnumerable<T> Items { get; }
        public IReadOnlyCollection<StoreKey> Keys { get; }
        public int Count { get; }

        public T GetByGuid(Guid guid);
        public T GetByIndex(dynamic index);
        public void Add(T entity, dynamic key);
        public void AddWithIndex(T entity, dynamic key, params dynamic[] indexes);
        public bool TryGetValue(dynamic key, out T result);
        public bool TryGetValueByIndex(dynamic key, out T result);
        public bool ContainsKey(dynamic key);
        public bool ContainsIndex(dynamic index);
        public bool Remove(dynamic key);
        public bool RemoveByIndex(dynamic index);
        public void Clear();
        public bool Contains(T entity);
        public IEnumerable<T> Find(Func<T, bool> condition);
        public IEnumerable<KeyValuePair<HashSet<StoreKey>, T>> GetEnumerator();
        public void AddCategory(T entity, params string[] categories);
        public void RemoveCategory(T entity, params string[] categories);

        public T this[dynamic key] { get; }
        public void FlagAsError(Guid guid, XmlError type, string error);
    }
}
