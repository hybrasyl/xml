using System;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;

namespace Hybrasyl.Xml.Objects;

public partial class Nation : ILoadOnStart<Nation>
{
    public SpawnPoint RandomSpawnPoint =>
        SpawnPoints.Count > 0 ? SpawnPoints[Random.Shared.Next(0, SpawnPoints.Count)] : default;
    public new static XmlLoadResult<Nation> LoadAll(string path) => HybrasylEntity<Nation>.LoadAll(path);

}
