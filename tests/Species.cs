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
using System.IO;
using System.Linq;
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

public class SpeciesTests
{
    private const string Ns = SchemaValidation.Ns;

    private const string Names = "<Names Singular=\"Dwarf\" Plural=\"Dwarves\" Adjective=\"Dwarven\" />";

    private const string Bodies =
        "<Bodies><Body Key=\"Ruddy\" Id=\"40\" Name=\"Ruddy\" Default=\"true\" /><Body Key=\"Ashen\" Id=\"41\" /></Bodies>";

    private const string Dwarf =
        $"""
         <Species xmlns="{Ns}" Key="Dwarf" Id="4" Origin="Aosda" PlayerOption="Starter" Affinity="Wind" Resistance="Wind">
           {Names}
           <Description>Storm-and-stone people descended from the Gleodhar.</Description>
           {Bodies}
         </Species>
         """;

    // A starter species without elements, and with them.
    private const string Starter = "Origin=\"Aosda\" PlayerOption=\"Starter\"";
    private const string Playable = Starter + " Affinity=\"Wind\" Resistance=\"Wind\"";

    private static string Make(string key = "Dwarf", byte id = 4, string attributes = Playable, string body = Names + Bodies) =>
        $"<Species xmlns=\"{Ns}\" Key=\"{key}\" Id=\"{id}\" {attributes}>{body}</Species>";

    private static readonly string Kobold = Make("Kobold", 5,
        "Origin=\"Fanaidhe\" PlayerOption=\"ByBranch\" Affinity=\"Nature\" Resistance=\"Wind\"",
        "<Names Singular=\"Kobold\" Plural=\"Kobolds\" Adjective=\"Koboldine\" />");

    private static readonly string Vosk = Make("Vosk", 6, "Origin=\"Fanaidhe\" PlayerOption=\"Starter\" BranchOf=\"Kobold\"",
        "<Names Singular=\"Vosk\" Plural=\"Vosken\" Adjective=\"Voskar\" /><Bodies><Body Key=\"Moss\" Id=\"60\" Default=\"true\" /></Bodies>");

    // Schema

    [Fact]
    public void SampleSpeciesIsValid()
    {
        Assert.Empty(SchemaValidation.Validate(Dwarf));
    }

    [Fact]
    public void ParentWithoutBodiesAndBranchWithoutElementsAreValid()
    {
        Assert.Empty(SchemaValidation.Validate(Kobold));
        Assert.Empty(SchemaValidation.Validate(Vosk));
    }

    [Theory]
    [InlineData("Origin=\"Aosdha\" PlayerOption=\"Starter\"", Names + Bodies)] // unknown origin
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Hidden\"", Names + Bodies)] // unknown option
    [InlineData("PlayerOption=\"Starter\"", Names + Bodies)] // no origin
    [InlineData(Starter + " Affinity=\"Storm Wind\"", Names + Bodies)] // an element key with a space
    [InlineData(Starter, Bodies)] // no names
    [InlineData(Starter, Names + "<Bodies />")] // an empty body list
    [InlineData(Starter, Names + "<Bodies><Body Key=\"Ruddy\" Id=\"40\" RetailSkinColor=\"10\" /></Bodies>")] // retail value above 9
    [InlineData(Starter, Names + "<Bodies><Body Key=\"Ruddy\" Id=\"40\" /><Body Key=\"Ruddy\" Id=\"41\" /></Bodies>")] // body key twice
    [InlineData(Starter, Names + "<Bodies><Body Key=\"Ruddy\" Id=\"40\" /><Body Key=\"Ashen\" Id=\"40\" /></Bodies>")] // body id twice
    public void SchemaRejectsBadSpecies(string attributes, string body)
    {
        Assert.NotEmpty(SchemaValidation.Validate(Make(attributes: attributes, body: body)));
    }

    [Fact]
    public void SchemaRejectsAKeyThatIsNotAnIdentifier()
    {
        Assert.NotEmpty(SchemaValidation.Validate(Make(key: "Sea Folk")));
    }

    // Deserialization

    [Fact]
    public void SampleSpeciesDeserializes()
    {
        var dwarf = Species.Deserialize(Dwarf);

        Assert.Equal("Dwarf", dwarf.PrimaryKey);
        Assert.Equal(4, dwarf.Id);
        Assert.Equal(SpeciesOrigin.Aosda, dwarf.Origin);
        Assert.Equal(SpeciesPlayerOption.Starter, dwarf.PlayerOption);
        Assert.Equal("Dwarves", dwarf.Names.Plural);
        Assert.False(dwarf.IsBranch);
        Assert.Equal("Ruddy", dwarf.DefaultBody.Key);

        Assert.True(dwarf.TryGetBody("ashen", out var ashen));
        Assert.Equal(41, ashen.Id);
        Assert.Null(ashen.Name);
        Assert.False(ashen.Default);
        Assert.Equal(0, ashen.RetailSkinColor);
        Assert.False(dwarf.TryGetBody("Pale", out _));
    }

    // Validation

    private static XmlDataManager Catalog(params string[] species)
    {
        var manager = new XmlDataManager();
        foreach (var xml in species)
            manager.Add(Species.Deserialize(xml));
        return manager;
    }

    private static int ValidateAll(XmlDataManager manager)
    {
        manager.ValidateAll<Species>();
        return manager.GetValidationResult<Species>().ErrorCount;
    }

    [Fact]
    public void StarterCatalogIsValid()
    {
        var manager = Catalog(Dwarf, Kobold, Vosk);

        Assert.Equal(0, ValidateAll(manager));
        Assert.Equal(3, manager.GetValidationResult<Species>().TotalProcessed);
    }

    [Fact]
    public void BranchTakesElementsFromItsParent()
    {
        var manager = Catalog(Kobold, Vosk);
        manager.TryGetValue("Vosk", out Species vosk);

        Assert.True(vosk.IsBranch);
        Assert.Equal("Kobold", vosk.GetParent(manager).Key);
        Assert.Equal("Nature", vosk.GetAffinity(manager));
        Assert.Equal("Wind", vosk.GetResistance(manager));
    }

    // Gnoll has its own body ids, so the only problem in each case is the one under test.
    private const string GnollBodies =
        "<Bodies><Body Key=\"Grey\" Id=\"90\" Default=\"true\" /><Body Key=\"Brown\" Id=\"91\" /></Bodies>";

    [Theory]
    [InlineData(Playable, Names)] // a Starter species with no bodies
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Unlockable\" Affinity=\"Wind\" Resistance=\"Wind\"", Names)] // an Unlockable species with no bodies
    [InlineData(Starter + " Resistance=\"Wind\"", Names + GnollBodies)] // no affinity
    [InlineData(Starter + " Affinity=\"Wind\"", Names + GnollBodies)] // no resistance
    [InlineData(Playable, Names + "<Bodies><Body Key=\"Grey\" Id=\"90\" /></Bodies>")] // no default body
    [InlineData(Playable, Names + "<Bodies><Body Key=\"Grey\" Id=\"90\" Default=\"true\" /><Body Key=\"Brown\" Id=\"91\" Default=\"true\" /></Bodies>")] // two default bodies
    [InlineData(Starter + " BranchOf=\"Gnome\"", Names + GnollBodies)] // a parent that is not loaded
    [InlineData(Starter + " BranchOf=\"Vosk\"", Names + GnollBodies)] // a parent that is a branch
    [InlineData(Starter + " BranchOf=\"Dwarf\"", Names + GnollBodies)] // a parent that is not ByBranch
    [InlineData("Origin=\"Aosda\" PlayerOption=\"ByBranch\" BranchOf=\"Kobold\"", Names)] // a branch that is ByBranch
    [InlineData(Playable, GnollBodies)] // no names
    public void ValidateRejectsOneProblem(string attributes, string body)
    {
        var manager = Catalog(Dwarf, Kobold, Vosk, Make("Gnoll", 9, attributes, body));
        manager.TryGetValue("Gnoll", out Species gnoll);

        Assert.Single(gnoll.Validate(manager));
        Assert.Equal(1, ValidateAll(manager));
    }

    [Theory]
    [InlineData(" Gnoll", "Grey")] // a species key with a space at the start
    [InlineData("Gnoll", "Grey ")] // a body key with a space at the end
    [InlineData("Gnoll", " ")] // a blank body key
    public void ValidateRejectsAPaddedKey(string key, string bodyKey)
    {
        // The schema pattern rejects these in a file, but the loader does not check files against the schema (HXML-35).
        var gnoll = Species.Deserialize(Make(key, 9, body: Names + $"<Bodies><Body Key=\"{bodyKey}\" Id=\"90\" Default=\"true\" /></Bodies>"));
        Assert.Single(gnoll.Validate(Catalog()));
    }

    [Fact]
    public void ValidateAllRejectsKeysThatDifferOnlyInCase()
    {
        var manager = Catalog(Dwarf, Make("DWARF", 9, body: Names + GnollBodies));
        Assert.Equal(2, ValidateAll(manager));
    }

    [Fact]
    public void SpeciesWithoutPlayerOptionIsLocked()
    {
        // The loader does not check files against the schema (HXML-35), so a missing option must not be playable.
        var species = Species.Deserialize(Make(attributes: "Origin=\"Aosda\" Affinity=\"Wind\" Resistance=\"Wind\""));
        Assert.Equal(SpeciesPlayerOption.Locked, species.PlayerOption);
    }

    [Fact]
    public void ValidateAllRejectsARepeatedSpeciesId()
    {
        var manager = Catalog(Dwarf, Make("Gnoll", 4, body: Names + GnollBodies));
        Assert.Equal(2, ValidateAll(manager));
    }

    [Fact]
    public void ValidateAllRejectsABodyIdInTwoSpecies()
    {
        var manager = Catalog(Dwarf, Make("Gnoll", 9));
        Assert.Equal(2, ValidateAll(manager));
    }

    [Fact]
    public void ValidateChecksBodiesThatTheSchemaRejects()
    {
        // A species built in code, or loaded without the schema (HXML-35).
        var dwarf = Species.Deserialize(Dwarf);
        dwarf.Bodies.Add(new SpeciesBody { Key = "ruddy", Id = 40, RetailSkinColor = 10 });
        dwarf.Bodies.Add(new SpeciesBody { Key = null, Id = 42 });

        Assert.Equal(4, dwarf.Validate(Catalog()).Count);
    }

    [Fact]
    public void ValidateChecksElementsAgainstATable()
    {
        var table = new ElementTable
        {
            Name = "Test",
            Elements = [new ElementTableElement { Key = "Wind", Id = 3 }, new ElementTableElement { Key = "Nature", Id = 5 }]
        };

        Assert.Empty(Species.Deserialize(Dwarf).Validate(table));
        Assert.Empty(Species.Deserialize(Vosk).Validate(table));
        Assert.Equal(2, Species.Deserialize(Make(attributes: "Origin=\"Aosda\" PlayerOption=\"Starter\" Affinity=\"Storm\" Resistance=\"Wood\"")).Validate(table).Count);
        Assert.Throws<ArgumentNullException>(() => Species.Deserialize(Dwarf).Validate((ElementTable) null));
    }

    // Loading

    [Fact]
    public void SpeciesLoadFromTheSpeciesDirectory()
    {
        var root = Directory.CreateTempSubdirectory("hybrasyl-species-");
        try
        {
            var species = root.CreateSubdirectory("species");
            File.WriteAllText(Path.Combine(species.FullName, "dwarf.xml"), Dwarf);
            File.WriteAllText(Path.Combine(species.FullName, "kobold.xml"), Kobold);
            File.WriteAllText(Path.Combine(species.FullName, "vosk.xml"), Vosk);

            var manager = new XmlDataManager(root.FullName);
            manager.LoadAll<Species>();

            Assert.Equal(3, manager.Count<Species>());
            Assert.True(manager.TryGetValue("Dwarf", out Species _));
            Assert.True(manager.TryGetValueByIndex("vosk", out Species _));
            Assert.Equal(0, ValidateAll(manager));
        }
        finally
        {
            root.Delete(true);
        }
    }
}
