using System;
using System.Collections.Generic;
using Hybrasyl.Xml.Objects;

namespace Hybrasyl.Xml.Interfaces;

public interface IWorldDataManager
{
    public string RootPath { get; set; }
    public T Get<T>(dynamic name) where T : HybrasylEntity<T>;
    public T GetByIndex<T>(dynamic index) where T : HybrasylEntity<T>;
    public T GetByGuid<T>(Guid guid) where T : HybrasylEntity<T>;
    public void Add<T>(T entity, dynamic name) where T : HybrasylEntity<T>;
    public void AddWithIndex<T>(T entity, dynamic name, params dynamic[] indexes) where T : HybrasylEntity<T>;
    public bool TryGetValue<T>(dynamic key, out T result) where T : HybrasylEntity<T>;
    public bool TryGetValueByIndex<T>(dynamic key, out T result) where T : HybrasylEntity<T>;
    public IEnumerable<T> Values<T>() where T : HybrasylEntity<T>;
    public bool ContainsKey<T>(dynamic name) where T : HybrasylEntity<T>;
    public bool ContainsIndex<T>(dynamic index) where T : HybrasylEntity<T>;
    public int Count<T>() where T : HybrasylEntity<T>;
    public bool Remove<T>(dynamic name) where T : HybrasylEntity<T>;
    public bool RemoveByIndex<T>(dynamic index) where T : HybrasylEntity<T>;
    public IEnumerable<T> Find<T>(Func<T, bool> condition);
    public void LoadAll<T>() where T : HybrasylEntity<T>, ILoadOnStart<T>;
    public void LoadAll();
    public void ProcessAll<T>() where T : HybrasylEntity<T>, IPostProcessable<T>;
    public void ValidateAll<T>() where T : HybrasylEntity<T>, IAdditionalValidationRequired<T>;

    public IEnumerable<Castable> FindSkills(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null);

    public IEnumerable<Castable> FindSpells(long str = 0, long @int = 0, long wis = 0, long con = 0, long dex = 0,
        string category = null);

}
