using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Objects;
using System;
using System.Collections.Generic;

namespace Hybrasyl.Xml.Manager;

public class XmlCategoryStore<T> : XmlStore<T>, ICategoricalWorldDataStore<T> where T : HybrasylEntity<T>, ICategorizable<T>
{
    private Dictionary<string, HashSet<Guid>> _categories = new();

    private void AddCategoriesToIndex(Guid entityGuid, params string[] categories)
    {
        foreach (var category in categories)
        {
            if (!_categories.TryGetValue(category, out var guid))
                _categories[category] = new HashSet<Guid>();
            _categories[category].Add(entityGuid);
        }
    }

    private void RemoveCategoriesFromIndex(Guid entityGuid, params string[] categories)
    {
        foreach (var category in categories)
        {
            if (!_categories.TryGetValue(category, out var guid))
                return;
            _categories[category].Remove(entityGuid);
        }
    }

    public void AddCategory(dynamic name, params string[] categories)
    {
        if (TryGetValue(name, out T result)) return;
        result.AddCategories(categories);
        AddCategoriesToIndex(result.Guid, categories);
    }

    public void RemoveCategory(dynamic name, params string[] categories)
    {
        if (TryGetValue(name, out T result)) return;
        result.RemoveCategories(categories);
        RemoveCategoriesFromIndex(result.Guid, categories);
    }
}
