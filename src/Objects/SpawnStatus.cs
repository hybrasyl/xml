using System;

namespace Hybrasyl.Xml.Objects
{
    public class SpawnStatus
    {
        public double LastSpawnSeconds => (DateTime.Now - LastSpawnTime).TotalSeconds;
        public DateTime LastSpawnTime { get; set; }
        public DateTime LastErrorTime { get; set; }
        public Exception LastException { get; set; }
        public int ErrorCount { get; set; } = 0;
        public bool Disabled { get; set; } = false;
    }
}
