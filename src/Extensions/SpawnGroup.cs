using System;
using System.IO;

namespace Hybrasyl.Xml.Objects;

public partial class SpawnGroup
{
    public static SpawnGroup operator +(SpawnGroup sg1, SpawnGroup sg2)
    {
        var merged = sg1.Clone<SpawnGroup>();
        merged.Spawns.AddRange(sg2.Spawns);
        return merged;
    }
}