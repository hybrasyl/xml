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
using System.IO;
using Hybrasyl.Xml.Interfaces;
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

public class MissingPrimaryKeyTests
{
    [Fact]
    public void StoreRejectsAMissingPrimaryKeyWithAClearMessage()
    {
        var manager = new XmlDataManager();

        var error = Assert.Throws<ArgumentException>(() => manager.Add(new Species { Key = null }));

        Assert.Contains("Species has no primary key", error.Message);
    }

    [Fact]
    public void LoadRecordsTheMissingPrimaryKeyForTheFile()
    {
        var result = LoadOne<Species>($"""<Species xmlns="{SchemaValidation.Ns}" Id="4" />""", Species.LoadAll);

        Assert.Equal(0, result.SuccessCount);
        Assert.Contains("Species has no primary key", Assert.Single(result.Errors).Value);
    }

    // Item and Castable compute the key from Name, so a file with no Name has no key.
    [Fact]
    public void LoadRecordsTheMissingNameOfAnItem()
    {
        var result = LoadOne<Item>($"""<Item xmlns="{SchemaValidation.Ns}" />""", Item.LoadAll);

        Assert.Equal(0, result.SuccessCount);
        Assert.Contains("Item has no primary key", Assert.Single(result.Errors).Value);
    }

    [Fact]
    public void LoadRecordsTheMissingNameOfACastable()
    {
        var result = LoadOne<Castable>($"""<Castable xmlns="{SchemaValidation.Ns}" />""", Castable.LoadAll);

        Assert.Equal(0, result.SuccessCount);
        Assert.Contains("Castable has no primary key", Assert.Single(result.Errors).Value);
    }

    // The key of a map is its Id. Its Name is a secondary key.
    [Fact]
    public void LoadRecordsTheMissingNameOfAMap()
    {
        var result = LoadOne<Map>($"""<Map xmlns="{SchemaValidation.Ns}" Id="500" X="10" Y="10" />""", Map.LoadAll);

        Assert.Equal(0, result.SuccessCount);
        Assert.Contains("Map has a null secondary key", Assert.Single(result.Errors).Value);
    }

    [Fact]
    public void StoreRejectsANullSecondaryKeyWithAClearMessage()
    {
        var manager = new XmlDataManager();

        var error = Assert.Throws<ArgumentException>(() =>
            manager.AddWithIndex(new Species { Key = "elf" }, "elf", (string?)null));

        Assert.Contains("Species has a null secondary key", error.Message);
    }

    // The store also reads the entity's own secondary keys, which can differ from the indexes passed in.
    [Fact]
    public void StoreRejectsANullSecondaryKeyOfTheEntity()
    {
        var manager = new XmlDataManager();

        var error = Assert.Throws<ArgumentException>(() =>
            manager.AddWithIndex(new Map { Id = 500, Name = null }, "500", "alias"));

        Assert.Contains("Map has a null secondary key", error.Message);
    }

    private static ILoadResult LoadOne<T>(string xml, Action<IWorldDataManager, string> loadAll)
        where T : HybrasylEntity<T>
    {
        var root = Path.Combine(Path.GetTempPath(), $"hybxml-{Guid.NewGuid()}");
        var dir = Path.Combine(root, WorldDataDirectory.NameFor<T>());
        Directory.CreateDirectory(dir);
        try
        {
            File.WriteAllText(Path.Combine(dir, "nokey.xml"), xml);
            var manager = new XmlDataManager(root);
            loadAll(manager, root);
            return manager.GetStore<T>().LoadResult;
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }
}
