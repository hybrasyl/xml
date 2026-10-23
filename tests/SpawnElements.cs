// This file is part of Project Hybrasyl.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the Affero General Public License as published by
// the Free Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but
// without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
// for more details.
//
// You should have received a copy of the Affero General Public License along
// with this program. If not, see <http://www.gnu.org/licenses/>.
//
// (C) 2020-2026 ERISCO, LLC
//
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

public class SpawnElementTests
{
    private static Spawn MakeSpawn(ElementType? damage, ElementType? defense)
    {
        var spawn = new Spawn();
        if (damage is { } d)
        {
            spawn.Damage = new SpawnDamage();
            spawn.Damage.Elements.Add(d);
        }

        if (defense is { } f)
        {
            spawn.Defense = new SpawnDefense();
            spawn.Defense.Elements.Add(f);
        }

        return spawn;
    }

    [Fact]
    public void DefensiveElementReadsDefenseList()
    {
        var spawn = MakeSpawn(ElementType.Fire, ElementType.Water);
        Assert.Equal(ElementType.Fire, spawn.OffensiveElement);
        Assert.Equal(ElementType.Water, spawn.DefensiveElement);
    }

    [Fact]
    public void DefenseWithoutDamageDoesNotThrow()
    {
        var spawn = MakeSpawn(null, ElementType.Earth);
        Assert.Equal(ElementType.Earth, spawn.DefensiveElement);
        Assert.Equal(ElementType.None, spawn.OffensiveElement);
    }

    [Fact]
    public void DamageWithoutDefenseDoesNotThrow()
    {
        var spawn = MakeSpawn(ElementType.Wind, null);
        Assert.Equal(ElementType.Wind, spawn.OffensiveElement);
        Assert.Equal(ElementType.None, spawn.DefensiveElement);
    }

    [Fact]
    public void EmptyElementListsGiveNone()
    {
        var spawn = new Spawn { Damage = new SpawnDamage(), Defense = new SpawnDefense() };
        Assert.Equal(ElementType.None, spawn.OffensiveElement);
        Assert.Equal(ElementType.None, spawn.DefensiveElement);
    }
}
