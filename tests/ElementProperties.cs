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

public class StatModifiersElementTests
{
    private static StatModifiers Modifiers() => new()
    {
        ElementalModifiers =
        [
            new ElementalModifier { Type = ElementalModifierType.Augment, Element = "Fire", Modifier = 1.2 },
            new ElementalModifier { Type = ElementalModifierType.Resistance, Element = "Water", Modifier = 0.8 }
        ]
    };

    [Fact]
    public void GetElementalAugmentReadsAugments()
    {
        Assert.Equal(1.2, Modifiers().GetElementalAugment("Fire"));
    }

    [Fact]
    public void GetElementalResistanceReadsResistances()
    {
        Assert.Equal(0.8, Modifiers().GetElementalResistance("Water"));
    }

    [Fact]
    public void ElementalModifierComparesKeysWithoutCase()
    {
        Assert.Equal(1.2, Modifiers().GetElementalAugment("fire"));
    }

    [Fact]
    public void ElementalModifierIsOneWhenNoEntryMatches()
    {
        var modifiers = Modifiers();
        // The type must match too: Fire has an augment but no resistance.
        Assert.Equal(1.0, modifiers.GetElementalResistance("Fire"));
        Assert.Equal(1.0, modifiers.GetElementalAugment("Earth"));
        Assert.Equal(1.0, new StatModifiers { ElementalModifiers = null }.GetElementalAugment("Fire"));
    }

    [Fact]
    public void SumKeepsTheElementalModifiersOfBothSides()
    {
        var fireResistance = new ElementalModifier
            { Type = ElementalModifierType.Resistance, Element = "Fire", Modifier = 0.03 };

        var sum = Modifiers() + new StatModifiers { ElementalModifiers = [fireResistance] };

        Assert.Equal(3, sum.ElementalModifiers.Count);
        Assert.Contains(fireResistance, sum.ElementalModifiers);
        Assert.Equal(1.2, sum.GetElementalAugment("Fire"));
    }

    [Fact]
    public void SumMakesTheListWhenOnlyTheRightSideHasElementalModifiers()
    {
        var sum = new StatModifiers { ElementalModifiers = null } + Modifiers();

        Assert.Equal(2, sum.ElementalModifiers.Count);
    }

    [Fact]
    public void SumKeepsTheLeftListWhenTheRightSideHasNoElementalModifiers()
    {
        Assert.Equal(2, (Modifiers() + new StatModifiers { ElementalModifiers = null }).ElementalModifiers.Count);
        Assert.Equal(2, (Modifiers() + new StatModifiers()).ElementalModifiers.Count);
    }
}

public class CastableElementTests
{
    [Fact]
    public void ElementIsNullWithoutElements()
    {
        Assert.Null(new Castable().Element);
    }

    [Fact]
    public void ElementIsTheOnlySelector()
    {
        Assert.Equal("Random:Cardinal", new Castable { Elements = ["Random:Cardinal"] }.Element);
    }

    [Fact]
    public void ElementPicksOneOfSeveralSelectors()
    {
        string[] elements = ["Fire", "Water", "Wind"];
        Assert.Contains(new Castable { Elements = [..elements] }.Element, elements);
    }
}
