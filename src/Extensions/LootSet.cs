using System;

namespace Hybrasyl.Xml.Objects;

public partial class LootSet
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
}