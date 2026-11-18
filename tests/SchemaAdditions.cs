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

using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

/// <summary>Small schema changes: each test proves that the schema accepts the new shape.</summary>
public class SchemaAdditionTests
{
    private const string Ns = SchemaValidation.Ns;

    private static void AssertValid(string xml)
    {
        var errors = SchemaValidation.Validate(xml);
        Assert.True(errors.Count == 0, string.Join("; ", errors));
    }

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
        AssertValid(ItemWith(vendor));
        Assert.Equal("Misc", Item.Deserialize(ItemWith(vendor)).Properties.Vendor.ShopTab);
    }

    private static string MapWith(string attributes = "", string children = "") =>
        $"""<Map xmlns="{Ns}" Id="500" X="10" Y="10" {attributes}><Name>Test Map</Name>{children}</Map>""";

    [Fact]
    public void MapAmbientSoundDefaultsToNone()
    {
        AssertValid(MapWith());
        Assert.Equal(0, Map.Deserialize(MapWith()).AmbientSound);
    }

    [Fact]
    public void MapReadsAmbientSound()
    {
        var xml = MapWith("""Music="3" AmbientSound="12" """);

        AssertValid(xml);
        Assert.Equal(12, Map.Deserialize(xml).AmbientSound);
    }

    [Fact]
    public void SchemaRejectsAnAmbientSoundAbove255()
    {
        Assert.NotEmpty(SchemaValidation.Validate(MapWith("""AmbientSound="256" """)));
    }

    [Fact]
    public void MapReadsItsScriptAfterSigns()
    {
        var xml = MapWith(children:
            """<Signs><Sign Type="Sign" X="1" Y="1"><Name>A sign</Name></Sign></Signs><Script>mileth_map</Script>""");

        AssertValid(xml);
        Assert.Equal("mileth_map", Map.Deserialize(xml).Script);
    }

    [Fact]
    public void AReactorScriptIsNotTheMapScript()
    {
        var xml = MapWith(children: """<Reactors><Reactor X="1" Y="1"><Script>trap</Script></Reactor></Reactors>""");

        AssertValid(xml);
        Assert.Null(Map.Deserialize(xml).Script);
    }

    private static string CastableWith(string intent, string icon = "1", string effects = "") =>
        $"""
         <Castable xmlns="{Ns}" Icon="{icon}" Book="PrimarySpell">
           <Name>Test Castable</Name>
           <Intents>{intent}</Intents>
           {effects}
           <Mastery />
         </Castable>
         """;

    [Fact]
    public void BeamReadsLengthAndWidth()
    {
        var xml = CastableWith("""<Intent><Beam Length="4" Width="3" Direction="Front" /></Intent>""");

        AssertValid(xml);
        var beam = Assert.Single(Castable.Deserialize(xml).Intents[0].Beam);
        Assert.Equal(4, beam.Length);
        Assert.Equal(3, beam.Width);
        Assert.Equal(IntentDirection.Front, beam.Direction);
    }

    [Fact]
    public void BeamWithNoAttributesIsOneTileInFront()
    {
        var xml = CastableWith("<Intent><Beam /></Intent>");

        AssertValid(xml);
        var beam = Assert.Single(Castable.Deserialize(xml).Intents[0].Beam);
        Assert.Equal(1, beam.Length);
        Assert.Equal(1, beam.Width);
        Assert.Equal(IntentDirection.None, beam.Direction);
        Assert.Equal(VisualEffectType.Targets, beam.VisualEffect);
    }

    [Fact]
    public void SchemaRejectsABeamWithAnEvenWidth()
    {
        Assert.NotEmpty(SchemaValidation.Validate(CastableWith("""<Intent><Beam Width="2" /></Intent>""")));
    }

    [Fact]
    public void IntentReadsPromptText()
    {
        var xml = CastableWith("""<Intent UseType="Prompt" Prompt="Speak the word:" />""");

        AssertValid(xml);
        Assert.Equal("Speak the word:", Castable.Deserialize(xml).Intents[0].Prompt);
    }

    [Fact]
    public void IntentPromptIsOptional()
    {
        Assert.Null(Castable.Deserialize(CastableWith("""<Intent UseType="Prompt" />""")).Intents[0].Prompt);
    }

    [Fact]
    public void DamageReadsTheNoPositionalFlag()
    {
        var xml = CastableWith("<Intent />", effects:
            """<Effects><Damage Type="Direct"><Flags>NoCrit NoPositional</Flags><Simple>5</Simple><Formula>1</Formula></Damage></Effects>""");

        AssertValid(xml);
        Assert.Equal(DamageFlags.NoCrit | DamageFlags.NoPositional, Castable.Deserialize(xml).Effects.Damage.Flags);
    }
}
