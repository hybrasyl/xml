using System;
using Hybrasyl.Xml.Objects;

namespace Hybrasyl.Xml.Interfaces;

public interface ICategoricalWorldDataStore<T> : IWorldDataStore<T> where T : HybrasylEntity<T>, ICategorizable<T>
{
    public void AddCategory(dynamic name, params string[] categories);
    public void RemoveCategory(dynamic name, params string[] categories);
}

