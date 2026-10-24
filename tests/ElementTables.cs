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
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Hybrasyl.Xml.Manager;
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

    private const string Elements =
        "<Elements>" +
        "<Element Key=\"Force\" Id=\"0\" Default=\"true\" />" +
        "<Element Key=\"Fire\" Id=\"1\" RetailId=\"1\" />" +
        "<Element Key=\"Water\" Id=\"2\" RetailId=\"2\" />" +
        "<Element Key=\"Wind\" Id=\"3\" RetailId=\"3\" />" +
        "<Element Key=\"Earth\" Id=\"4\" RetailId=\"4\" />" +
        "<Element Key=\"Dark\" Id=\"6\" RetailId=\"6\" />" +
        "<Element Key=\"Undead\" Id=\"9\" RetailId=\"9\" />" +
        "</Elements>";

    private const string Groups =
        "<Groups>" +
        "<Group Key=\"Cardinal\" Members=\"Fire Water Wind Earth\" />" +
        "<Group Key=\"Solo\" Members=\"Fire\" />" +
        "</Groups>";

    private const string FireRow =
        "<Source Element=\"Fire\"><Target Element=\"Wind\" Multiplier=\"1.5\" /><Target Element=\"Water\" /></Source>";

    private const string UndeadRow =
        "<Source Element=\"Undead\" DefaultMultiplier=\"1.2\"><Target Element=\"Undead\" Multiplier=\"0.5\" /></Source>";

    private static readonly string[] Cardinal = ["Fire", "Water", "Wind", "Earth"];

    private static string Table(string rows, string attributes = "", string elements = Elements, string groups = Groups) =>
        $"<ElementTable xmlns=\"{Ns}\" Name=\"Test\" {attributes}>{elements}{groups}{rows}</ElementTable>";

    private static ElementTable Deserialize(string xml)
    {
        using var reader = new StringReader(xml);
        return (ElementTable) new XmlSerializer(typeof(ElementTable)).Deserialize(reader)!;
    }

    private static ElementTable Sample(string attributes = "") => Deserialize(Table(FireRow + UndeadRow, attributes));

    private static List<string> Validate(string xml)
    {
        var errors = new List<string>();
        var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema, Schemas = Schemas.Value };
        settings.ValidationEventHandler += (_, e) => errors.Add(e.Message);
        using var reader = XmlReader.Create(new StringReader(xml), settings);
        while (reader.Read()) { }
        return errors;
    }

    private static string AddElement(string element) => Elements.Replace("</Elements>", element + "</Elements>");

    // Schema

    [Fact]
    public void SampleTableIsValid()
    {
        Assert.Empty(Validate(Table(FireRow + UndeadRow)));
    }

    [Fact]
    public void TableWithoutGroupsIsValid()
    {
        Assert.Empty(Validate(Table(FireRow, groups: "")));
    }

    [Fact]
    public void TableWithoutElementsIsInvalid()
    {
        Assert.NotEmpty(Validate(Table(FireRow, elements: "")));
    }

    [Fact]
    public void SourceRowWithoutTargetsIsValid()
    {
        Assert.Empty(Validate(Table("<Source Element=\"Dark\" DefaultMultiplier=\"0.9\" />")));
    }

    [Fact]
    public void SourceForUndefinedElementIsInvalid()
    {
        Assert.NotEmpty(Validate(Table("<Source Element=\"Wood\"><Target Element=\"Fire\" Multiplier=\"1\" /></Source>")));
    }

    [Fact]
    public void TargetForUndefinedElementIsInvalid()
    {
        Assert.NotEmpty(Validate(Table("<Source Element=\"Fire\"><Target Element=\"Wood\" Multiplier=\"1\" /></Source>")));
    }

    [Fact]
    public void DuplicateElementKeyIsInvalid()
    {
        Assert.NotEmpty(Validate(Table(FireRow, elements: AddElement("<Element Key=\"Fire\" Id=\"20\" />"))));
    }

    [Fact]
    public void DuplicateElementIdIsInvalid()
    {
        Assert.NotEmpty(Validate(Table(FireRow, elements: AddElement("<Element Key=\"Void\" Id=\"1\" />"))));
    }

    [Fact]
    public void ElementId255IsInvalid()
    {
        Assert.NotEmpty(Validate(Table(FireRow, elements: AddElement("<Element Key=\"Void\" Id=\"255\" />"))));
    }

    [Fact]
    public void RetailIdAbove9IsInvalid()
    {
        Assert.NotEmpty(Validate(Table(FireRow, elements: AddElement("<Element Key=\"Void\" Id=\"22\" RetailId=\"10\" />"))));
    }

    [Fact]
    public void DuplicateGroupKeyIsInvalid()
    {
        var groups = Groups.Replace("</Groups>", "<Group Key=\"Solo\" Members=\"Water\" /></Groups>");
        Assert.NotEmpty(Validate(Table(FireRow, groups: groups)));
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
    public void SourceWithoutElementIsInvalid()
    {
        // Without a value, the unique check skips the row, so two unnamed rows would both pass.
        var unnamed = "<Source><Target Element=\"Wind\" Multiplier=\"1.5\" /></Source>";
        Assert.NotEmpty(Validate(Table(unnamed)));
        Assert.NotEmpty(Validate(Table(unnamed + unnamed)));
    }

    [Fact]
    public void SameTargetInTwoSourcesIsValid()
    {
        Assert.Empty(Validate(Table(FireRow +
            "<Source Element=\"Water\"><Target Element=\"Wind\" Multiplier=\"0.8\" /></Source>")));
    }

    // Multipliers

    [Fact]
    public void DefaultMultiplierIsOneWhenUnset()
    {
        Assert.Equal(1.0, Sample().DefaultMultiplier);
    }

    [Fact]
    public void GetMultiplierReturnsDeclaredPair()
    {
        Assert.Equal(1.5, Sample().GetMultiplier("Fire", "Wind"));
    }

    [Fact]
    public void GetMultiplierUsesRowDefaultForMissingTarget()
    {
        var table = Sample("DefaultMultiplier=\"0.9\"");
        Assert.Equal(1.2, table.GetMultiplier("Undead", "Fire"));
        Assert.Equal(0.5, table.GetMultiplier("Undead", "Undead"));
    }

    [Fact]
    public void GetMultiplierAcceptsResolvedElements()
    {
        var table = Sample();
        Assert.True(table.TryResolve("Fire", out var fire));
        Assert.True(table.TryResolve("Wind", out var wind));
        Assert.Equal(1.5, table.GetMultiplier(fire, wind));
        Assert.Equal(1.0, table.GetMultiplier(null, wind));
    }

    [Fact]
    public void GetMultiplierUsesTableDefaultForMissingTarget()
    {
        Assert.Equal(0.9, Sample("DefaultMultiplier=\"0.9\"").GetMultiplier("Fire", "Earth"));
    }

    [Fact]
    public void GetMultiplierUsesTableDefaultForMissingSource()
    {
        Assert.Equal(0.9, Sample("DefaultMultiplier=\"0.9\"").GetMultiplier("Dark", "Wind"));
    }

    [Fact]
    public void GetMultiplierUsesTableDefaultForTargetWithoutMultiplier()
    {
        Assert.Equal(0.9, Sample("DefaultMultiplier=\"0.9\"").GetMultiplier("Fire", "Water"));
    }

    [Fact]
    public void GetMultiplierComparesKeysWithoutCase()
    {
        Assert.Equal(1.5, Sample().GetMultiplier("fire", "WIND"));
    }

    // Elements and selectors

    [Fact]
    public void DefaultElementIsTheOneMarkedDefault()
    {
        Assert.Equal("Force", Sample().DefaultElement?.Key);
    }

    [Fact]
    public void RetailIdIsZeroWhenUnset()
    {
        Assert.True(Sample().TryGetElement("Force", out var force));
        Assert.Equal(0, force.RetailId);
    }

    [Fact]
    public void TryGetElementComparesKeysWithoutCase()
    {
        var table = Sample();
        Assert.True(table.TryGetElement("earth", out var earth));
        Assert.Equal(4, earth.Id);
        Assert.False(table.TryGetElement("Wood", out _));
        Assert.False(table.TryGetElement(null, out _));
    }

    [Fact]
    public void TryResolveReturnsAnElementKey()
    {
        Assert.True(Sample().TryResolve("Wind", out var wind));
        Assert.Equal("Wind", wind.Key);
    }

    [Fact]
    public void TryResolvePicksFromAGroup()
    {
        var table = Sample();
        Assert.True(table.TryResolve("Random:Solo", out var solo));
        Assert.Equal("Fire", solo.Key);
        Assert.True(table.TryResolve("Random:Cardinal", out var cardinal));
        Assert.Contains(cardinal.Key, Cardinal);
    }

    [Fact]
    public void TryResolveAcceptsLegacySelectors()
    {
        Assert.True(Sample().TryResolve("RandomTemuair", out var element));
        Assert.Contains(element.Key, Cardinal);
    }

    [Theory]
    [InlineData("Current")]
    [InlineData("Belt")]
    [InlineData("Necklace")]
    [InlineData("Random:Missing")]
    [InlineData("Wood")]
    [InlineData("")]
    [InlineData(null)]
    public void TryResolveRejectsWhatTheTableCannotResolve(string? selector)
    {
        Assert.False(Sample().TryResolve(selector, out var element));
        Assert.Null(element);
    }

    [Fact]
    public void TryResolveNeverPicksAnUndefinedMember()
    {
        var table = Sample();
        table.Groups.Add(new ElementTableGroup { Key = "Mixed", Members = ["Wood", "Fire", "Lava"] });
        table.Groups.Add(new ElementTableGroup { Key = "Gone", Members = ["Wood"] });
        for (var i = 0; i < 50; i++)
        {
            Assert.True(table.TryResolve("Random:Mixed", out var element));
            Assert.Equal("Fire", element.Key);
        }

        Assert.False(table.TryResolve("Random:Gone", out _));
    }

    [Fact]
    public void TryResolveWeightsARepeatedMember()
    {
        // A member listed twice is picked twice as often. Content uses this to weight a group.
        var table = Sample();
        table.Groups.Add(new ElementTableGroup { Key = "Weighted", Members = ["Fire", "Fire", "Water"] });
        Assert.Empty(table.Validate());

        const int picks = 6000;
        var fire = 0;
        for (var i = 0; i < picks; i++)
        {
            Assert.True(table.TryResolve("Random:Weighted", out var element));
            if (element.Key == "Fire") fire++;
        }

        // The expected share is 2/3. The bounds are more than eight standard deviations from it.
        Assert.InRange(fire / (double) picks, 0.61, 0.72);
    }

    [Theory]
    [InlineData("Wind", true)]
    [InlineData("Random:Cardinal", true)]
    [InlineData("RandomTemuair", true)]
    [InlineData("Current", true)]
    [InlineData("RandomExpanded", false)]
    [InlineData("Random:Missing", false)]
    [InlineData("Wood", false)]
    [InlineData("", false)]
    public void CanResolveChecksWithoutPicking(string selector, bool expected)
    {
        // The sample table has no Ordinal group, so the old name RandomExpanded cannot resolve.
        Assert.Equal(expected, Sample().CanResolve(selector));
    }

    // Validate

    [Fact]
    public void ValidateAcceptsTheSampleTable()
    {
        Assert.Empty(Sample().Validate());
    }

    [Fact]
    public void ValidateRejectsTableWithoutOneDefault()
    {
        var none = Sample();
        none.Elements.ForEach(e => e.Default = false);
        Assert.NotEmpty(none.Validate());

        var two = Sample();
        two.Elements.ForEach(e => e.Default = true);
        Assert.NotEmpty(two.Validate());
    }

    [Fact]
    public void ValidateRejectsReservedKeys()
    {
        var table = Sample();
        table.Elements.Add(new ElementTableElement { Key = "Current", Id = 30 });
        table.Groups.Add(new ElementTableGroup { Key = "RandomTemuair", Members = ["Fire"] });
        Assert.Equal(2, table.Validate().Count);
    }

    [Fact]
    public void ValidateRejectsUndefinedGroupMember()
    {
        var table = Sample();
        table.Groups.Add(new ElementTableGroup { Key = "Old", Members = ["Wood"] });
        Assert.Single(table.Validate());
    }

    [Fact]
    public void ValidateRejectsKeysThatDifferOnlyInCase()
    {
        var table = Sample();
        table.Elements.Add(new ElementTableElement { Key = "fire", Id = 31 });
        Assert.Single(table.Validate());
    }

    [Fact]
    public void ValidateRejectsDuplicateElementIds()
    {
        var table = Sample();
        table.Elements.Add(new ElementTableElement { Key = "Void", Id = 1 });
        Assert.Single(table.Validate());
    }

    [Fact]
    public void ValidateRejectsElementWithoutKey()
    {
        var table = Sample();
        table.Elements.Add(new ElementTableElement { Key = null, Id = 33 });
        Assert.Single(table.Validate());
    }

    [Fact]
    public void ValidateRejectsDuplicateGroupKeys()
    {
        var table = Sample();
        table.Groups.Add(new ElementTableGroup { Key = "cardinal", Members = ["Fire"] });
        Assert.Single(table.Validate());
    }

    [Fact]
    public void ValidateRejectsDuplicateSourceRows()
    {
        var table = Sample();
        table.Source.Add(new ElementTableSourceElement { Element = "fire" });
        Assert.Single(table.Validate());
    }

    [Fact]
    public void ValidateRejectsDuplicateTargetsInOneRow()
    {
        var table = Sample();
        table.Source[0].Target.Add(new ElementTableTargetElement { Element = "wind" });
        Assert.Single(table.Validate());
    }

    [Fact]
    public void ValidateRejectsGroupWithoutKey()
    {
        var table = Sample();
        table.Groups.Add(new ElementTableGroup { Key = null, Members = ["Fire"] });
        Assert.Single(table.Validate());
    }

    [Fact]
    public void ValidateRejectsTableWithoutSourceRows()
    {
        var table = Sample();
        table.Source.Clear();
        Assert.Single(table.Validate());
    }

    [Fact]
    public void ValidateRejectsWireValuesOutOfRange()
    {
        var table = Sample();
        table.Elements.Add(new ElementTableElement { Key = "Void", Id = ElementTable.NoElementId });
        table.Elements.Add(new ElementTableElement { Key = "Time", Id = 32, RetailId = 10 });
        Assert.Equal(2, table.Validate().Count);
    }

    [Fact]
    public void ValidateReportsNullCollectionsAsErrors()
    {
        var table = Sample();
        table.Groups.Add(new ElementTableGroup { Key = "Empty", Members = null });
        table.Source.Add(new ElementTableSourceElement { Element = "Dark", Target = null });
        Assert.Single(table.Validate());

        table.Elements = null;
        table.Groups = null;
        table.Source = null;
        Assert.NotEmpty(table.Validate());
        Assert.False(table.TryResolve("Random:Cardinal", out _));
        Assert.Equal(1.0, table.GetMultiplier("Fire", "Wind"));
    }
}

[Collection("Xml")]
public class ElementTableValidateAllTests : IClassFixture<XmlManagerFixture>
{
    private readonly XmlManagerFixture fixture;

    public ElementTableValidateAllTests(XmlManagerFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void ValidateAllStoresOneErrorForEachInvalidTable()
    {
        var manager = fixture.SyncManager;
        manager.ValidateAll<ElementTable>();
        var result = manager.GetValidationResult<ElementTable>();
        var tables = manager.Values<ElementTable>().ToList();
        Assert.Equal(tables.Count, result.TotalProcessed);
        Assert.Equal(tables.Count(t => t.Validate().Count > 0), result.ErrorCount);
    }

    [Fact]
    public void ResultSummariesIncludeElementTableValidation()
    {
        var summary = Assert.Single(fixture.SyncManager.GetResultSummaries(),
            s => s.Stage == XmlResultStage.Validation && s.TypeName == nameof(ElementTable));
        Assert.Equal(fixture.SyncManager.Values<ElementTable>().Count(), summary.TotalProcessed);
    }
}
