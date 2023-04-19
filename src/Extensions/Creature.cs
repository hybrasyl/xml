using System;
using System.IO;

namespace Hybrasyl.Xml.Objects;

public partial class Creature
{
    public static Creature operator &(Creature c1, Creature c2)
    {
        var creatureMerge = c1.Clone<Creature>();
        creatureMerge.BehaviorSet = string.IsNullOrEmpty(c2.BehaviorSet) ? c1.BehaviorSet : c2.BehaviorSet;
        creatureMerge.Description = string.IsNullOrEmpty(c2.Description) ? c1.Description : c2.Description;
        creatureMerge.Name = c2.Name;
        creatureMerge.Hostility = c2.Hostility ?? c1.Hostility;
        creatureMerge.Loot = c2.Loot + c1.Loot;
        return c1;
    }
}