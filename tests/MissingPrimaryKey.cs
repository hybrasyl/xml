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
        var root = Path.Combine(Path.GetTempPath(), $"hybxml-{Guid.NewGuid()}");
        var dir = Path.Combine(root, WorldDataDirectory.NameFor<Species>());
        Directory.CreateDirectory(dir);
        try
        {
            var file = Path.Combine(dir, "nokey.xml");
            File.WriteAllText(file, $"""<Species xmlns="{SchemaValidation.Ns}" Id="4" />""");
            var manager = new XmlDataManager(root);

            Species.LoadAll(manager, root);

            var result = manager.GetStore<Species>().LoadResult;
            Assert.Equal(0, result.SuccessCount);
            Assert.Contains("Species has no primary key", Assert.Single(result.Errors).Value);
        }
        finally
        {
            Directory.Delete(root, true);
        }
    }
}
