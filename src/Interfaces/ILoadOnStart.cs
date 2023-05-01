using System.Text;
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;

namespace Hybrasyl.Xml.Interfaces;

public interface ILoadOnStart<T> where T : HybrasylEntity<T>
{
    public static abstract void LoadAll(IWorldDataManager manager, string path=null);
}

public interface IPostProcessable<T> where T : HybrasylEntity<T>
{
    public static abstract void ProcessAll(IWorldDataManager manager);
}

public interface IAdditionalValidation<T> where T : HybrasylEntity<T>
{
    public static abstract void ValidateAll(IWorldDataManager manager);
}

