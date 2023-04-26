using System;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Xml.Serialization;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class Creature : ILoadOnStart<Creature>, IPostProcessable<Creature>
{
    [XmlIgnore] public Guid ParentGuid { get; set; }

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

    public new static void LoadAll(IWorldDataManager manager, string path) => HybrasylEntity<Creature>.LoadAll(manager, path);

    public static void ProcessAll(IWorldDataManager manager)
    {
        var ret = new XmlProcessResult();
        foreach (var template in manager.Values<Creature>().ToList())
        {
            foreach (var subtemplate in template.Types)
            {
                subtemplate.ParentGuid = template.Guid;
                manager.Add(subtemplate, subtemplate.Name);
                ret.AdditionalCount++;
            }

            ret.TotalProcessed++;
        }

        manager.UpdateStatus<Creature>(ret);
    }

}