using Hybrasyl.Xml.Objects;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hybrasyl.Xml.src.Interfaces;
using System.Linq;

namespace Hybrasyl.Xml.Interfaces;

public interface ICategorizable<T> where T : HybrasylEntity<T>
{
    public List<string> CategoryList { get; }
    public List<Category> Categories { get; set; }
    public string Name { get; set; }

    public void AddCategories(params string[] categories)
    {
        if (this is not T obj) return;
        foreach (var category in categories)
        {
            if (!CategoryList.Contains(category))
            {
                Categories.Add(new Category { Value=category});
            }
        }
    }

    public void RemoveCategories(params string[] categories)
    {
        if (this is not T obj) return;
        Categories = Categories.Where(c => !categories.Contains(c.Value)).ToList();
    }
}
