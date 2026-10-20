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
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

public class ElementTableTests
{
    private const string Ns = "http://www.hybrasyl.com/XML/Hybrasyl/2020-02";

    private static readonly Lazy<XmlSchemaSet> Schemas = new(() =>
    {
        var set = new XmlSchemaSet();
        foreach (var file in Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "XSD"), "*.xsd"))
            set.Add(Ns, file);
        set.Compile();
        return set;
    });

    private static string Table(string body, string attributes = "") =>
        $"<ElementTable xmlns=\"{Ns}\" Name=\"Test\" {attributes}>{body}</ElementTable>";

    private static ElementTable Deserialize(string xml)
    {
        using var reader = new StringReader(xml);
        return (ElementTable) new XmlSerializer(typeof(ElementTable)).Deserialize(reader)!;
    }

    private static List<string> Validate(string xml)
    {
        var errors = new List<string>();
        var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema, Schemas = Schemas.Value };
        settings.ValidationEventHandler += (_, e) => errors.Add(e.Message);
        using var reader = XmlReader.Create(new StringReader(xml), settings);
        while (reader.Read()) { }
        return errors;
    }

    private const string FireRow =
        "<Source Element=\"Fire\"><Target Element=\"Wind\" Multiplier=\"1.5\" /><Target Element=\"Water\" /></Source>";

    [Fact]
    public void SparseTableIsValid()
    {
        // The schema required exactly 9 Source rows of exactly 9 Targets until HXML-24 (HXML-21 #4).
        Assert.Empty(Validate(Table(FireRow)));
    }

    [Fact]
    public void TenElementTableIsValid()
    {
        var elements = new[] { "None", "Fire", "Water", "Wind", "Earth", "Light", "Dark", "Wood", "Metal", "Undead" };
        var rows = string.Concat(Array.ConvertAll(elements, s =>
            $"<Source Element=\"{s}\">" +
            string.Concat(Array.ConvertAll(elements, t => $"<Target Element=\"{t}\" Multiplier=\"1\" />")) +
            "</Source>"));
        Assert.Empty(Validate(Table(rows)));
    }

    [Fact]
    public void DuplicateSourceIsInvalid()
    {
        Assert.NotEmpty(Validate(Table(FireRow + FireRow)));
    }

    [Fact]
    public void DuplicateTargetInOneSourceIsInvalid()
    {
        Assert.NotEmpty(Validate(Table(
            "<Source Element=\"Fire\"><Target Element=\"Wind\" Multiplier=\"1.5\" />" +
            "<Target Element=\"Wind\" Multiplier=\"0.5\" /></Source>")));
    }

    [Fact]
    public void SameTargetInTwoSourcesIsValid()
    {
        Assert.Empty(Validate(Table(FireRow +
            "<Source Element=\"Water\"><Target Element=\"Wind\" Multiplier=\"0.8\" /></Source>")));
    }

    [Fact]
    public void DefaultMultiplierIsOneWhenUnset()
    {
        Assert.Equal(1.0, Deserialize(Table(FireRow)).DefaultMultiplier);
    }

    [Fact]
    public void GetMultiplierReturnsDeclaredPair()
    {
        Assert.Equal(1.5, Deserialize(Table(FireRow)).GetMultiplier(ElementType.Fire, ElementType.Wind));
    }

    [Fact]
    public void GetMultiplierUsesDefaultForMissingTarget()
    {
        var table = Deserialize(Table(FireRow, "DefaultMultiplier=\"0.9\""));
        Assert.Equal(0.9, table.GetMultiplier(ElementType.Fire, ElementType.Earth));
    }

    [Fact]
    public void GetMultiplierUsesDefaultForMissingSource()
    {
        var table = Deserialize(Table(FireRow, "DefaultMultiplier=\"0.9\""));
        Assert.Equal(0.9, table.GetMultiplier(ElementType.Dark, ElementType.Wind));
    }

    [Fact]
    public void GetMultiplierUsesDefaultForTargetWithoutMultiplier()
    {
        var table = Deserialize(Table(FireRow, "DefaultMultiplier=\"0.9\""));
        Assert.Equal(0.9, table.GetMultiplier(ElementType.Fire, ElementType.Water));
    }
}
