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
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;

namespace Hybrasyl.Xml.Interfaces;

public interface IWorldDataManager
{
    public string RootPath { get; set; }
    public T Get<T>(dynamic name) where T : HybrasylEntity<T>;
    public T GetByIndex<T>(dynamic index) where T : HybrasylEntity<T>;
    public T GetByGuid<T>(Guid guid) where T : HybrasylEntity<T>;
    public XmlDataStore<T> GetStore<T>() where T : HybrasylEntity<T>;
    public void Add<T>(T entity, dynamic key) where T : HybrasylEntity<T>;
    public void Add<T>(T entity) where T : HybrasylEntity<T>;
    public void AddWithIndex<T>(T entity, dynamic key, params dynamic[] indexes) where T : HybrasylEntity<T>;
    public bool TryGetValue<T>(dynamic key, out T result) where T : HybrasylEntity<T>;
    public bool TryGetValueByIndex<T>(dynamic key, out T result) where T : HybrasylEntity<T>;
    public IEnumerable<T> Values<T>() where T : HybrasylEntity<T>;
    public bool ContainsKey<T>(dynamic key) where T : HybrasylEntity<T>;
    public bool ContainsIndex<T>(dynamic index) where T : HybrasylEntity<T>;
    public int Count<T>() where T : HybrasylEntity<T>;
    public bool Remove<T>(dynamic key) where T : HybrasylEntity<T>;
    public bool RemoveByIndex<T>(dynamic index) where T : HybrasylEntity<T>;
    public IEnumerable<T> Find<T>(Func<T, bool> condition) where T : HybrasylEntity<T>;
    public void LoadAll<T>() where T : HybrasylEntity<T>, ILoadOnStart<T>;
    public void LoadAllAsync<T>() where T : HybrasylEntity<T>, ILoadOnStart<T>;
    public void ProcessAll<T>() where T : HybrasylEntity<T>, IPostProcessable<T>;
    public void ValidateAll<T>() where T : HybrasylEntity<T>, IAdditionalValidation<T>;
    public void FlagAsError<T>(T entity, XmlError error, string message) where T : HybrasylEntity<T>;

    public void UpdateResult<T>(IProcessResult result) where T : HybrasylEntity<T>;
    public void UpdateResult<T>(ILoadResult result) where T : HybrasylEntity<T>;
    public void UpdateResult<T>(IValidationResult result) where T : HybrasylEntity<T>;

    public ILoadResult GetLoadResult<T>() where T : HybrasylEntity<T>;
    public IProcessResult GetProcessResult<T>() where T : HybrasylEntity<T>;
    public IValidationResult GetValidationResult<T>() where T : HybrasylEntity<T>;

    public void LoadData();

    public IEnumerable<Castable> FindSkills(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null);

    public IEnumerable<Castable> FindSpells(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null);

    public HashSet<Castable> FindCastables(long Str = 0, long Int = 0, long Wis = 0,
        long Con = 0, long Dex = 0, string category = null,
        CastableFilter filter = CastableFilter.SkillsAndSpells);

    public IEnumerable<Item> FindItem(string name);

    public IEnumerable<T> FindByCategory<T>(string category) where T : HybrasylEntity<T>, ICategorizable;
}