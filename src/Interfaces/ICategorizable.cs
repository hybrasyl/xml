using Hybrasyl.Xml.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Hybrasyl.Xml.Interfaces;

public interface ICategorizable<T>
{
    public List<string> CategoryList { get; }
    public List<Category> Categories { get; set; }
    public string Name { get; set; }

    public void AddCategories(params string[] categories)
    {
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
        Categories = Categories.Where(c => !categories.Contains(c.Value)).ToList();
    }
}
