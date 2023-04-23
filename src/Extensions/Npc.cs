
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class Npc : ILoadOnStart<Npc>
{
    public new static XmlLoadResult<Npc> LoadAll(string path) => HybrasylEntity<Npc>.LoadAll(path);

}