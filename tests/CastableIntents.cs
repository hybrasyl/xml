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
using System.Collections.Generic;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

public class CastableIntentTests
{
    // One entry for each shape in the CastableIntent type of Castable.xsd.
    // Add an entry here when the schema gets a new shape.
    public static IEnumerable<object[]> Shapes() =>
    [
        ["Map", (Action<CastableIntent>) (i => i.Map = new object())],
        ["Line", (Action<CastableIntent>) (i => i.Line.Add(new CastableLineIntent()))],
        ["Cross", (Action<CastableIntent>) (i => i.Cross.Add(new CastableCrossIntent()))],
        ["Square", (Action<CastableIntent>) (i => i.Square.Add(new CastableSquareIntent()))],
        ["Tile", (Action<CastableIntent>) (i => i.Tile.Add(new CastableTileIntent()))],
        ["Cone", (Action<CastableIntent>) (i => i.Cone.Add(new CastableConeIntent()))]
    ];

    [Fact]
    public void IntentWithNoShapeIsShapeless()
    {
        Assert.True(new CastableIntent().IsShapeless);
    }

    [Theory]
    [MemberData(nameof(Shapes))]
    public void IntentWithOneShapeIsNotShapeless(string shape, Action<CastableIntent> addShape)
    {
        var intent = new CastableIntent();
        addShape(intent);
        Assert.False(intent.IsShapeless, $"an intent with only {shape} must not be shapeless");
    }

    [Fact]
    public void ShapeListCoversEverySchemaShape()
    {
        // If the schema gets a new shape element, this count changes and the
        // test fails until the shape is added to Shapes() and IsShapeless.
        var elements = 0;
        foreach (var property in typeof(CastableIntent).GetProperties())
        {
            if (property.GetCustomAttributes(typeof(System.Xml.Serialization.XmlElementAttribute), false).Length > 0)
                elements++;
        }

        Assert.Equal(elements, new List<object[]>(Shapes()).Count);
    }
}
