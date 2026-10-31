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

using System.Xml.Linq;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

public class SpawnGroups
{
    private const string Ns = SchemaValidation.Ns;

    [Fact]
    public void SpawnGroupIsARootElement()
    {
        var group = SpawnGroup.Deserialize($"""<SpawnGroup xmlns="{Ns}" Name="Crypt" BaseLevel="10" />""");

        Assert.Equal("Crypt", group.Name);
        Assert.Equal("10", group.BaseLevel);

        group.MapId = 500;
        var root = XDocument.Parse(group.Serialize()).Root!;
        Assert.Equal(XName.Get("SpawnGroup", Ns), root.Name);
        // The server sets MapId at load. It is not in the schema, so it is not written.
        Assert.Empty(root.Descendants(XName.Get("MapId", Ns)));
    }
}
