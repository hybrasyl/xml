using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

// For some reason xsd2code doesn't add this and it breaks spawngroup parsing
[XmlRoot(Namespace = "http://www.hybrasyl.com/XML/Hybrasyl/2020-02")]
public partial class SpawnGroup : ILoadOnStart<SpawnGroup>
{
    public override string PrimaryKey => Name;
    public ushort MapId { get; set; }

    public static SpawnGroup operator +(SpawnGroup sg1, SpawnGroup sg2)
    {
        var merged = sg1.Clone<SpawnGroup>();
        merged.Spawns.AddRange(sg2.Spawns);
        return merged;
    }
    public new static void LoadAll(IWorldDataManager manager, string path) => HybrasylEntity<SpawnGroup>.LoadAll(manager, path);

}