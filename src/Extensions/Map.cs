using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class Map : ILoadOnStart<Map>
{
    public override string PrimaryKey => Id.ToString();
    public override List<string> SecondaryKeys => new() { Name };
    public new static void LoadAll(IWorldDataManager manager, string path) => HybrasylEntity<Map>.LoadAll(manager, path);

}