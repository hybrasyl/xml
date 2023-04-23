using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class WorldMap : ILoadOnStart<WorldMap>
{
    public new static XmlLoadResult<WorldMap> LoadAll(string path) => HybrasylEntity<WorldMap>.LoadAll(path);

}