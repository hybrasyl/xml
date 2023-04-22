using System.Linq;
using System;

namespace Hybrasyl.Xml.Objects;

public partial class Spawn
{
    public DateTime LastSpawn { get; set; } = default;
    public bool Disabled { get; set; } = false;
    public string ErrorMessage { get; set; } = string.Empty;

    public ElementType OffensiveElement
    {
        get
        {
            var ele = _damage.Elements.Count switch
            {
                1 => _damage.Elements.First(),
                > 1 => _damage.Elements.PickRandom(),
                _ => ElementType.None
            };
            return ele switch
            {
                ElementType.RandomExpanded => (ElementType) Random.Shared.Next(1, 10),
                ElementType.RandomTemuair => (ElementType) Random.Shared.Next(1, 7),
                _ => ele
            };
        }
    }

    public ElementType DefensiveElement
    {
        get
        {
            var ele = _defense.Elements.Count switch
            {
                1 => _damage.Elements.First(),
                > 1 => _damage.Elements.PickRandom(),
                _ => ElementType.None
            };
            return ele switch
            {
                ElementType.RandomExpanded => (ElementType) Random.Shared.Next(1, 10),
                ElementType.RandomTemuair => (ElementType) Random.Shared.Next(1, 7),
                _ => ele
            };
        }
    }



}
