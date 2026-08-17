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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;
using Xunit;

namespace Hybrasyl.XmlTests;

/// <summary>
///     Pins the world-data subdirectory each loadable type reads from.
/// </summary>
/// <remarks>
///     A wrong name here loads nothing and reports nothing: the loader treats a missing
///     directory as an empty file list. The expected names below are written by hand rather
///     than derived from the code under test, so a change to the naming rule fails here
///     instead of quietly emptying a store.
/// </remarks>
public class WorldDataDirectoryTests
{
    /// <summary>
    ///     Every type that loads on start, and the directory it must read. Hand-maintained: a
    ///     new loadable type belongs here, and the coverage test below refuses to pass until
    ///     it is added.
    /// </summary>
    private static readonly Dictionary<string, string> Expected = new()
    {
        ["Castable"] = "castables",
        ["Creature"] = "creatures",
        ["CreatureBehaviorSet"] = "creaturebehaviorsets",
        ["ElementTable"] = "elementtables",
        ["Item"] = "items",
        ["Localization"] = "localizations",
        ["LootSet"] = "lootsets",
        ["Map"] = "maps",
        ["Nation"] = "nations",
        ["Npc"] = "npcs",
        ["ServerConfig"] = "serverconfigs",
        ["SpawnGroup"] = "spawngroups",
        ["Status"] = "statuses",
        ["VariantGroup"] = "variantgroups",
        ["WorldMap"] = "worldmaps"
    };

    private static IEnumerable<Type> LoadOnStartTypes =>
        typeof(IWorldDataManager).Assembly.GetTypes()
            .Where(predicate: t => t.GetInterfaces().Any(predicate: i =>
                       i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ILoadOnStart<>))
                   && t.GetMethod("LoadAll", BindingFlags.Static | BindingFlags.Public) != null);

    [Fact]
    public void EveryLoadableTypeResolvesToItsExpectedDirectory()
    {
        foreach (var type in LoadOnStartTypes)
        {
            Assert.True(Expected.ContainsKey(type.Name),
                $"{type.Name} loads on start but has no expected directory. Add it to " +
                "Expected -- a name the rule gets wrong loads zero files silently.");
            Assert.Equal(Expected[type.Name], WorldDataDirectory.NameFor(type.Name));
        }
    }

    [Fact]
    public void ExpectedTableNamesNoTypeThatNoLongerLoads()
    {
        var loadable = LoadOnStartTypes.Select(selector: t => t.Name).ToHashSet();
        var stale = Expected.Keys.Where(predicate: n => !loadable.Contains(n)).ToList();

        Assert.True(stale.Count == 0,
            $"Expected names {string.Join(", ", stale)}, which no longer load on start.");
    }

    [Theory]
    [InlineData("Status", "statuses")] // sibilant: -s takes -es, and this one is live
    [InlineData("Box", "boxes")]
    [InlineData("Buzz", "buzzes")]
    [InlineData("Branch", "branches")]
    [InlineData("Dish", "dishes")]
    [InlineData("Category", "categories")] // consonant + y
    [InlineData("Day", "days")] // vowel + y stays regular
    [InlineData("Item", "items")]
    public void NameFor_AppliesTheEnglishRulesTheModelExercises(string typeName, string expected)
    {
        Assert.Equal(expected, WorldDataDirectory.NameFor(typeName));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NameFor_RejectsAnEmptyTypeName(string typeName)
    {
        Assert.Throws<ArgumentException>(() => WorldDataDirectory.NameFor(typeName));
    }
}
