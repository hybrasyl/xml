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

/// <summary>Small schema changes: each test proves that the schema accepts the new shape.</summary>
public class SchemaAdditionTests
{
    private const string Ns = SchemaValidation.Ns;

    private static string ItemWith(string vendor) =>
        $"""
         <Item xmlns="{Ns}">
           <Name>Test Item</Name>
           <Properties>
             <Appearance Sprite="1" />
             <Stackable Max="1" />
             <Physical />
             {vendor}
           </Properties>
         </Item>
         """;

    [Theory]
    [InlineData("""<Vendor ShopTab="Misc"><Description>A test item.</Description></Vendor>""")]
    [InlineData("""<Vendor ShopTab="Misc" />""")]
    public void VendorDescriptionIsOptional(string vendor)
    {
        Assert.Empty(SchemaValidation.Validate(ItemWith(vendor)));
        Assert.Equal("Misc", Item.Deserialize(ItemWith(vendor)).Properties.Vendor.ShopTab);
    }

    private static string MapWith(string attributes = "", string children = "") =>
        $"""<Map xmlns="{Ns}" Id="500" X="10" Y="10" {attributes}><Name>Test Map</Name>{children}</Map>""";

    [Fact]
    public void MapAmbientSoundDefaultsToNone()
    {
        Assert.Empty(SchemaValidation.Validate(MapWith()));
        Assert.Equal(0, Map.Deserialize(MapWith()).AmbientSound);
    }

    [Fact]
    public void MapReadsAmbientSound()
    {
        var xml = MapWith("""Music="3" AmbientSound="12" """);

        Assert.Empty(SchemaValidation.Validate(xml));
        Assert.Equal(12, Map.Deserialize(xml).AmbientSound);
    }

    [Fact]
    public void SchemaRejectsAnAmbientSoundAbove255()
    {
        Assert.NotEmpty(SchemaValidation.Validate(MapWith("""AmbientSound="256" """)));
    }
}
