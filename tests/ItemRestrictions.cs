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

public class ItemRestrictions
{
    private const string Ns = SchemaValidation.Ns;

    private static Item MakeItem(string restrictions) =>
        Item.Deserialize($"""
        <Item xmlns="{Ns}">
          <Name>Ring</Name>
          <Properties>
            <Appearance Sprite="1" />
            <Stackable Max="1" />
            {restrictions}
          </Properties>
        </Item>
        """);

    [Theory]
    [InlineData("", 1, 255, 0, 255)]
    [InlineData("<Restrictions />", 1, 255, 0, 255)]
    [InlineData("""<Restrictions><Level Min="10" Max="99" /></Restrictions>""", 10, 99, 0, 255)]
    [InlineData("""<Restrictions><Ab Min="5" Max="40" /></Restrictions>""", 1, 255, 5, 40)]
    [InlineData("""<Restrictions><Level Min="10" Max="99" /><Ab Min="5" Max="40" /></Restrictions>""", 10, 99, 5, 40)]
    public void LevelAndAbilityLimitsReadTheirOwnElements(string restrictions, byte minLevel, byte maxLevel,
        byte minAbility, byte maxAbility)
    {
        var item = MakeItem(restrictions);

        Assert.Equal(minLevel, item.MinLevel);
        Assert.Equal(maxLevel, item.MaxLevel);
        Assert.Equal(minAbility, item.MinAbility);
        Assert.Equal(maxAbility, item.MaxAbility);
    }
}
