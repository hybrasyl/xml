using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hybrasyl.Xml.src.Interfaces
{
    public interface IWorldDataRecord<T>
    {
        public T Entity { get; set; }
        public HashSet<string> Keys { get; set; }
    }
}
