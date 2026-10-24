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

public class LootLists
{
    private static LootList WithGold(uint min, uint max, uint xp = 0) =>
        new() { Gold = new LootGold { Min = min, Max = max }, Xp = xp };

    [Fact]
    public void SumAddsGoldAndXp()
    {
        var sum = WithGold(10, 20, 100) + WithGold(5, 7, 50);

        Assert.Equal(15u, sum.Gold.Min);
        Assert.Equal(27u, sum.Gold.Max);
        Assert.Equal(150u, sum.Xp);
    }

    [Fact]
    public void SumKeepsGoldFromTheListThatHasIt()
    {
        var left = WithGold(10, 20) + new LootList();
        var right = new LootList() + WithGold(5, 7);
        var leftOnly = WithGold(10, 20) + null;
        var rightOnly = null + WithGold(5, 7);

        Assert.Equal((10u, 20u), (left.Gold.Min, left.Gold.Max));
        Assert.Equal((5u, 7u), (right.Gold.Min, right.Gold.Max));
        Assert.Equal((10u, 20u), (leftOnly.Gold.Min, leftOnly.Gold.Max));
        Assert.Equal((5u, 7u), (rightOnly.Gold.Min, rightOnly.Gold.Max));
    }

    [Fact]
    public void SumKeepsXpFromEachSide()
    {
        Assert.Equal(50u, (new LootList() + new LootList { Xp = 50 }).Xp);
        Assert.Equal(50u, (new LootList { Xp = 50 } + null).Xp);
        Assert.Equal(50u, (null + new LootList { Xp = 50 }).Xp);
    }

    [Fact]
    public void SumOfEmptyListsIsEmpty()
    {
        Assert.True((new LootList() + new LootList()).IsEmpty);
        Assert.True((new LootList() + null).IsEmpty);
        Assert.True(((LootList?) null + null).IsEmpty);
    }
}
