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

public class VariantElements
{
    private const string Belt = """
        <Item xmlns="http://www.hybrasyl.com/XML/Hybrasyl/2020-02">
          <Name>Element Belt</Name>
          <Properties>
            <Appearance Sprite="1" EquipSprite="1" />
            <Stackable Max="1" />
            <Physical Value="100" Weight="1" Durability="10" />
            <Equipment Slot="Waist" />
            <StatModifiers BaseDefensiveElement="Fire" BaseOffensiveElement="Water" />
            <Restrictions>
              <Level Min="1" Max="99" />
            </Restrictions>
          </Properties>
        </Item>
        """;

    private const string Group = """
        <VariantGroup xmlns="http://www.hybrasyl.com/XML/Hybrasyl/2020-02">
          <Name>elements</Name>
          <Variant>
            <Name>NoElements</Name>
            <Modifier>Sturdy</Modifier>
            <Properties>
              <StatModifiers BonusHp="5" />
            </Properties>
          </Variant>
          <Variant>
            <Name>NoStatModifiers</Name>
            <Modifier>Polished</Modifier>
            <Properties />
          </Variant>
          <Variant>
            <Name>OwnDefensive</Name>
            <Modifier>Earthen</Modifier>
            <Properties>
              <StatModifiers BaseDefensiveElement="Earth" />
            </Properties>
          </Variant>
        </VariantGroup>
        """;

    [Theory]
    [InlineData("NoElements", ElementType.Fire, ElementType.Water)]
    [InlineData("NoStatModifiers", ElementType.Fire, ElementType.Water)]
    [InlineData("OwnDefensive", ElementType.Earth, ElementType.Water)]
    public void ResolveVariant_KeepsBaseElements_UnlessTheVariantSetsItsOwn(string variantName,
        ElementType defensive, ElementType offensive)
    {
        var belt = Item.Deserialize(Belt);
        var variant = VariantGroup.Deserialize(Group).Variant.Single(predicate: v => v.Name == variantName);

        var resolved = variant.ResolveVariant(belt);

        Assert.Equal(defensive, resolved.Properties.StatModifiers.BaseDefensiveElement);
        Assert.Equal(offensive, resolved.Properties.StatModifiers.BaseOffensiveElement);
    }
}
