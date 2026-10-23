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

public class VariantResolution
{
    private const string Ns = "http://www.hybrasyl.com/XML/Hybrasyl/2020-02";

    private static Item MakeItem(string properties, string tags = "",
        string appearance = """<Appearance Sprite="1" EquipSprite="1" />""") =>
        Item.Deserialize($"""
        <Item xmlns="{Ns}">
          <Name>Base</Name>
          <Properties{TagsAttribute(tags)}>
            {appearance}
            <Stackable Max="1" />
            {properties}
          </Properties>
        </Item>
        """);

    private static Item Resolve(Item item, string variantProperties, string tags = "") =>
        VariantGroup.Deserialize($"""
            <VariantGroup xmlns="{Ns}">
              <Name>group</Name>
              <Variant>
                <Name>variant</Name>
                <Modifier>Fine</Modifier>
                <Properties{TagsAttribute(tags)}>{variantProperties}</Properties>
              </Variant>
            </VariantGroup>
            """).Variant.Single().ResolveVariant(item);

    private static string TagsAttribute(string tags) => tags == "" ? "" : $" Tags=\"{tags}\"";

    private const string Physical = """<Physical Value="100" Weight="3" Durability="10" />""";
    private const string Belt = $"""{Physical}<Equipment Slot="Waist" />""";
    private const string Weapon = $"""{Physical}<Equipment Slot="Weapon" />""";

    [Theory]
    [InlineData("""<StatModifiers BonusHp="5" />""", "Fire", "Water")]
    [InlineData("", "Fire", "Water")]
    [InlineData("""<StatModifiers BaseDefensiveElement="Earth" />""", "Earth", "Water")]
    public void ResolveVariant_KeepsBaseElements_UnlessTheVariantSetsItsOwn(string variantProperties,
        string defensive, string offensive)
    {
        var belt = MakeItem($"""{Belt}<StatModifiers BaseDefensiveElement="Fire" BaseOffensiveElement="Water" />""");

        var resolved = Resolve(belt, variantProperties);

        Assert.Equal(defensive, resolved.Properties.StatModifiers.BaseDefensiveElement);
        Assert.Equal(offensive, resolved.Properties.StatModifiers.BaseOffensiveElement);
    }

    [Fact]
    public void ResolveVariant_KeepsBasePhysical_WhenTheVariantHasNoPhysical()
    {
        var resolved = Resolve(MakeItem(Belt), "");

        Assert.Equal(100f, resolved.Properties.Physical.Value);
        Assert.Equal(3f, resolved.Properties.Physical.Weight);
        Assert.Equal(10f, resolved.Properties.Physical.Durability);
    }

    [Fact]
    public void ResolveVariant_ScalesPhysical_ByTheVariantMultipliers()
    {
        var resolved = Resolve(MakeItem(Belt), """<Physical Value="2" Durability="1.5" />""");

        Assert.Equal(200f, resolved.Properties.Physical.Value);
        Assert.Equal(3f, resolved.Properties.Physical.Weight);
        Assert.Equal(15f, resolved.Properties.Physical.Durability);
    }

    [Fact]
    public void ResolveVariant_ScalesAllFourDamageValues()
    {
        var sword = MakeItem($"""{Weapon}<Damage LargeMin="10" LargeMax="20" SmallMin="30" SmallMax="40" />""");

        var resolved = Resolve(sword, """<Damage LargeMin="2" LargeMax="2" SmallMin="2" SmallMax="2" />""");

        Assert.Equal(20f, resolved.Properties.Damage.LargeMin);
        Assert.Equal(40f, resolved.Properties.Damage.LargeMax);
        Assert.Equal(60f, resolved.Properties.Damage.SmallMin);
        Assert.Equal(80f, resolved.Properties.Damage.SmallMax);
    }

    [Fact]
    public void ResolveVariant_ClampsScaledDamage_ToUshortMax()
    {
        var sword = MakeItem($"""{Weapon}<Damage LargeMin="40000" LargeMax="40000" SmallMin="1" SmallMax="1" />""");

        var resolved = Resolve(sword, """<Damage LargeMin="2" LargeMax="2" SmallMin="1" SmallMax="1" />""");

        Assert.Equal(ushort.MaxValue, resolved.Properties.Damage.LargeMax);
    }

    [Fact]
    public void ResolveVariant_SkipsDamage_WhenTheBaseWeaponHasNone()
    {
        var resolved = Resolve(MakeItem(Weapon), """<Damage LargeMin="2" LargeMax="2" SmallMin="2" SmallMax="2" />""");

        Assert.Null(resolved.Properties.Damage);
    }

    [Fact]
    public void ResolveVariant_ResolvesAnItemWithNoEquipment()
    {
        var resolved = Resolve(MakeItem(Physical), """<Physical Value="2" />""");

        Assert.Equal(200f, resolved.Properties.Physical.Value);
    }

    [Fact]
    public void ResolveVariant_KeepsBaseFlags_AndAddsTheVariants()
    {
        var armor = MakeItem($"{Belt}<Flags>Tailorable</Flags>");

        var resolved = Resolve(armor, "<Flags>Depositable Vendorable</Flags>");

        Assert.Equal(ItemFlags.Tailorable | ItemFlags.Depositable | ItemFlags.Vendorable,
            resolved.Properties.Flags);
    }

    [Fact]
    public void ResolveVariant_TakesTheVariantsFlags_WhenTheBaseHasNone()
    {
        var resolved = Resolve(MakeItem(Belt), "<Flags>Depositable</Flags>");

        Assert.Equal(ItemFlags.Depositable, resolved.Properties.Flags);
    }

    [Fact]
    public void ResolveVariant_AddsTheVariantsTags_WhenTheBaseHasNone()
    {
        var resolved = Resolve(MakeItem(Belt), "", tags: "Magic");

        Assert.Equal(new[] { ItemTag.Magic }, resolved.Properties.Tags);
    }

    [Fact]
    public void ResolveVariant_AddsOnlyTheTagsTheBaseLacks()
    {
        var resolved = Resolve(MakeItem(Belt, tags: "Rare Magic"), "", tags: "Magic Legendary");

        Assert.Equal(new[] { ItemTag.Rare, ItemTag.Magic, ItemTag.Legendary }, resolved.Properties.Tags);
    }

    [Fact]
    public void ResolveVariant_RaisesLevelOne_WhenTheBaseHasNoRestrictions()
    {
        var resolved = Resolve(MakeItem(Belt), """<Restrictions><Level Min="2" /></Restrictions>""");

        Assert.Equal(3, resolved.Properties.Restrictions.Level.Min);
    }

    [Fact]
    public void ResolveVariant_RaisesLevelOne_WhenTheBaseLevelHasNoMin()
    {
        var resolved = Resolve(MakeItem($"""{Belt}<Restrictions><Level Max="99" /></Restrictions>"""),
            """<Restrictions><Level Min="2" /></Restrictions>""");

        Assert.Equal(3, resolved.Properties.Restrictions.Level.Min);
    }

    [Fact]
    public void ResolveVariant_RaisesTheBaseLevel_ByTheVariantsLevel()
    {
        var resolved = Resolve(MakeItem($"""{Belt}<Restrictions><Level Min="10" Max="99" /></Restrictions>"""),
            """<Restrictions><Level Min="2" /></Restrictions>""");

        Assert.Equal(12, resolved.Properties.Restrictions.Level.Min);
        Assert.Equal(99, resolved.Properties.Restrictions.Level.Max);
    }

    [Fact]
    public void ResolveVariant_KeepsTheBasesHideBoots()
    {
        var item = MakeItem(Belt, appearance: """<Appearance Sprite="1" EquipSprite="1" HideBoots="true" />""");

        Assert.True(Resolve(item, "").Properties.Appearance.HideBoots);
    }
}
