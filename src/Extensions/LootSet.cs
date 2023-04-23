using System;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class LootSet : ILoadOnStart<LootSet>
{
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

    public new static XmlLoadResult<LootSet> LoadAll(string path) => HybrasylEntity<LootSet>.LoadAll(path);

}