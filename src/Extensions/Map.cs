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
    public new static XmlLoadResult<Map> LoadAll(string path) => HybrasylEntity<Map>.LoadAll(path);

}