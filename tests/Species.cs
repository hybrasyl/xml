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

    private static string Make(string key = "Dwarf", byte id = 4, string attributes = "Origin=\"Aosda\" PlayerOption=\"Starter\" Affinity=\"Wind\" Resistance=\"Wind\"",
        string body = Names + Bodies) =>
        $"<Species xmlns=\"{Ns}\" Key=\"{key}\" Id=\"{id}\" {attributes}>{body}</Species>";

    private const string Kobold =
        $"<Species xmlns=\"{Ns}\" Key=\"Kobold\" Id=\"5\" Origin=\"Fanaidhe\" PlayerOption=\"ByBranch\" Affinity=\"Nature\" Resistance=\"Wind\"><Names Singular=\"Kobold\" Plural=\"Kobolds\" Adjective=\"Koboldine\" /></Species>";

    private const string Vosk =
        $"<Species xmlns=\"{Ns}\" Key=\"Vosk\" Id=\"6\" Origin=\"Fanaidhe\" PlayerOption=\"Starter\" BranchOf=\"Kobold\"><Names Singular=\"Vosk\" Plural=\"Vosken\" Adjective=\"Voskar\" /><Bodies><Body Key=\"Moss\" Id=\"60\" Default=\"true\" /></Bodies></Species>";

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
    [InlineData("Origin=\"Aosdha\" PlayerOption=\"Starter\"", Names + Bodies)]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Hidden\"", Names + Bodies)]
    [InlineData("PlayerOption=\"Starter\"", Names + Bodies)]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\" Affinity=\"Storm Wind\"", Names + Bodies)]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\"", Bodies)]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\"", Names + "<Bodies />")]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\"", Names + "<Bodies><Body Key=\"Ruddy\" Id=\"40\" RetailSkinColor=\"10\" /></Bodies>")]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\"", Names + "<Bodies><Body Key=\"Ruddy\" Id=\"40\" /><Body Key=\"Ruddy\" Id=\"41\" /></Bodies>")]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\"", Names + "<Bodies><Body Key=\"Ruddy\" Id=\"40\" /><Body Key=\"Ashen\" Id=\"40\" /></Bodies>")]
    public void SchemaRejectsBadSpecies(string attributes, string body)
    {
        // An unknown origin or option, a missing origin, an element key with a space, no names, an empty body
        // list, a retail value above 9, and a body key or body id twice.
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

    private const string Playable = "Origin=\"Aosda\" PlayerOption=\"Starter\" Affinity=\"Wind\" Resistance=\"Wind\"";

    [Theory]
    [InlineData(Playable, Names)]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Unlockable\" Affinity=\"Wind\" Resistance=\"Wind\"", Names)]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\" Resistance=\"Wind\"", Names + GnollBodies)]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\" Affinity=\"Wind\"", Names + GnollBodies)]
    [InlineData(Playable, Names + "<Bodies><Body Key=\"Grey\" Id=\"90\" /></Bodies>")]
    [InlineData(Playable, Names + "<Bodies><Body Key=\"Grey\" Id=\"90\" Default=\"true\" /><Body Key=\"Brown\" Id=\"91\" Default=\"true\" /></Bodies>")]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\" BranchOf=\"Gnome\"", Names + GnollBodies)]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\" BranchOf=\"Vosk\"", Names + GnollBodies)]
    [InlineData("Origin=\"Aosda\" PlayerOption=\"Starter\" BranchOf=\"Dwarf\"", Names + GnollBodies)]
    public void ValidateRejectsOneProblem(string attributes, string body)
    {
        // A playable species with no bodies (two options), no affinity, no resistance, no default body, two default
        // bodies, and a parent that is not loaded, that is a branch, or that is not ByBranch.
        var manager = Catalog(Dwarf, Kobold, Vosk, Make("Gnoll", 9, attributes, body));
        manager.TryGetValue("Gnoll", out Species gnoll);

        Assert.Single(gnoll.Validate(manager));
        Assert.Equal(1, ValidateAll(manager));
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
