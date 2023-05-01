using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hybrasyl.Xml.Interfaces
{
    public interface IIndexable
    {
        public string PrimaryKey { get; }
        public List<string> SecondaryKeys { get; }
    }
}
