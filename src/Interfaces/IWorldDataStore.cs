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
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hybrasyl.Xml.Interfaces;

public interface IWorldDataStore<T> where T : HybrasylEntity<T>
{
    public IEnumerable<T> Errors { get; }
    public int ErrorCount => Errors.Count();
    public int ItemCount => Values.Count();

    public IValidationResult ValidationResult { get; }
    public ILoadResult LoadResult { get; }
    public IProcessResult ProcessResult { get; }
    public IEnumerable<T> Values { get; }
    public IReadOnlyCollection<StoreKey> Keys { get; }
    public int Count { get; }

    public T this[dynamic key] { get; }

    public T GetByGuid(Guid guid);
    public T GetByIndex(dynamic index);
    public void Add(T entity, dynamic key);
    public void AddWithIndex(T entity, dynamic key, params dynamic[] indexes);
    public bool TryGetValue(dynamic key, out T result);
    public bool TryGetValueByIndex(dynamic key, out T result);
    public bool ContainsKey(dynamic key);
    public bool ContainsIndex(dynamic index);
    public bool ContainsCategory(string category);
    public bool Remove(dynamic key);
    public bool RemoveByIndex(dynamic index);
    public void Clear();
    public bool Contains(T entity);
    public IEnumerable<T> Find(Func<T, bool> condition);
    public IEnumerable<KeyValuePair<HashSet<StoreKey>, T>> GetEnumerator();
    public void AddCategory(T entity, params string[] categories);
    public void RemoveCategory(T entity, params string[] categories);
    public IEnumerable<T> FindByCategory(string category);
    public void FlagAsError(Guid guid, XmlError type, string error);
}