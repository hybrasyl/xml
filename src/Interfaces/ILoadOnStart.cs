using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;

namespace Hybrasyl.Xml.Interfaces;

public interface ILoadOnStart<T> where T : HybrasylEntity<T>
{
    public static abstract XmlLoadResult<T> LoadAll(string path);
}

public interface IPostProcessable<T> where T : HybrasylEntity<T>
{
    public static abstract XmlProcessResult<T> Process(IWorldDataManager manager);
}

public interface IAdditionalValidation<T> where T : HybrasylEntity<T>
{
    public static abstract XmlProcessResult<T> Validate(IWorldDataManager manager);
}

