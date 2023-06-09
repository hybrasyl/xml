using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hybrasyl.Xml.Objects
{
    public class SpawnGroupStatus
    {
        public double LastSpawnSeconds => (DateTime.Now - LastSpawnTime).TotalSeconds;
        public DateTime LastSpawnTime { get; set; }
        public DateTime LastErrorTime { get; set; }
        public Exception LastException { get; set; }
        public int ErrorCount { get; set; } = 0;
        public bool Disabled { get; set; } = false;
    }
}
