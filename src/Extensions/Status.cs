using System;
using System.Collections.Generic;
using System.Linq;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class Status : ICategorizable<Status>, ILoadOnStart<Status>
{
    public override string PrimaryKey => Name;
    public List<string> CategoryList => Categories.Select(x => x.Value).ToList();

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
    public new static void LoadAll(IWorldDataManager manager, string path) => HybrasylEntity<Status>.LoadAll(manager, path);
    
}

public partial class StatusHeal
{
    public bool IsSimple => string.IsNullOrEmpty(Formula);
    public bool IsEmpty => IsSimple && Simple.Value == 0 && Simple.Min == 0 && Simple.Max == 0;
}

public partial class StatusDamage
{
    public bool IsSimple => string.IsNullOrEmpty(Formula);
    public bool IsEmpty => IsSimple && Simple.Value == 0 && Simple.Min == 0 && Simple.Max == 0;
}

