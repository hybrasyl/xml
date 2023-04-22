using System;

namespace Hybrasyl.Xml.Objects;

public partial class Nation
{
    public SpawnPoint RandomSpawnPoint =>
        SpawnPoints.Count > 0 ? SpawnPoints[Random.Shared.Next(0, SpawnPoints.Count)] : default;
}
