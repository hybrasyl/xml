using System;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class LootSet : ILoadOnStart<LootSet>
{
    public override string PrimaryKey => Name;

    [Obsolete("This behavior is undesirable")]
    public int Id
    {
        get
        {
            unchecked
            {
                return 31 * (Name.GetHashCode() + 1);
            }
        }
    }

    public new static void LoadAll(IWorldDataManager manager, string path) => HybrasylEntity<LootSet>.LoadAll(manager, path);

}