
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class Npc : ILoadOnStart<Npc>
{
    public new static void LoadAll(IWorldDataManager manager, string path) => HybrasylEntity<Npc>.LoadAll(manager, path);

}