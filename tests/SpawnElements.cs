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
    private static Spawn MakeSpawn(string damage, string defense)
    {
        var spawn = new Spawn();
        if (damage != null)
        {
            spawn.Damage = new SpawnDamage();
            spawn.Damage.Elements.Add(damage);
        }

        if (defense != null)
        {
            spawn.Defense = new SpawnDefense();
            spawn.Defense.Elements.Add(defense);
        }

        return spawn;
    }

    [Fact]
    public void DefensiveElementReadsDefenseList()
    {
        var spawn = MakeSpawn("Fire", "Water");
        Assert.Equal("Fire", spawn.OffensiveElement);
        Assert.Equal("Water", spawn.DefensiveElement);
    }

    [Fact]
    public void DefenseWithoutDamageDoesNotThrow()
    {
        var spawn = MakeSpawn(null, "Earth");
        Assert.Equal("Earth", spawn.DefensiveElement);
        Assert.Null(spawn.OffensiveElement);
    }

    [Fact]
    public void DamageWithoutDefenseDoesNotThrow()
    {
        var spawn = MakeSpawn("Wind", null);
        Assert.Equal("Wind", spawn.OffensiveElement);
        Assert.Null(spawn.DefensiveElement);
    }

    [Fact]
    public void EmptyElementListsGiveNull()
    {
        var spawn = new Spawn { Damage = new SpawnDamage(), Defense = new SpawnDefense() };
        Assert.Null(spawn.OffensiveElement);
        Assert.Null(spawn.DefensiveElement);
    }

    [Fact]
    public void SelectorIsReturnedUnresolved()
    {
        // The package does not know the active element table, so the server resolves a selector.
        var spawn = MakeSpawn("Random:Cardinal", "Current");
        Assert.Equal("Random:Cardinal", spawn.OffensiveElement);
        Assert.Equal("Current", spawn.DefensiveElement);
    }
}
