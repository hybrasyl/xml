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
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

public class TraitTests
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

    private const string Dockworker =
        $"""
         <Trait xmlns="{Ns}" Name="Dockworker" Icon="112" IconSet="Skill" Category="Background">
           <Description>Years on the docks have made you unusually strong.</Description>
           <ExclusiveGroup>Background</ExclusiveGroup>
           <ExclusivePolicy>Replace</ExclusivePolicy>
           <ConflictsWith>
             <Trait>Scribe</Trait>
             <Trait>Courtier</Trait>
           </ConflictsWith>
           <StatModifiers BonusStr="3" />
         </Trait>
         """;

    private static string Minimal(string attributes = "Icon=\"40\" IconSet=\"Spell\"", string body = "") =>
        $"<Trait xmlns=\"{Ns}\" Name=\"Tempered\" {attributes}>{body}</Trait>";

    private static List<string> Validate(string xml)
    {
        var errors = new List<string>();
        var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema, Schemas = Schemas.Value };
        settings.ValidationEventHandler += (_, e) => errors.Add(e.Message);
        using var reader = XmlReader.Create(new StringReader(xml), settings);
        while (reader.Read()) { }
        return errors;
    }

    // Schema

    [Fact]
    public void SampleTraitIsValid()
    {
        Assert.Empty(Validate(Dockworker));
    }

    [Fact]
    public void TraitWithoutOptionalBlocksIsValid()
    {
        Assert.Empty(Validate(Minimal()));
    }

    [Theory]
    [InlineData("IconSet=\"Spell\"")]
    [InlineData("Icon=\"40\"")]
    public void IconAndIconSetAreRequired(string attributes)
    {
        Assert.NotEmpty(Validate(Minimal(attributes)));
    }

    [Theory]
    [InlineData("Icon=\"65535\" IconSet=\"Skill\"", true)]
    [InlineData("Icon=\"65536\" IconSet=\"Skill\"", false)]
    [InlineData("Icon=\"40\" IconSet=\"Utility\"", false)]
    public void IconIsAnUnsignedShortInASkillOrSpellSheet(string attributes, bool valid)
    {
        Assert.Equal(valid, Validate(Minimal(attributes)).Count == 0);
    }

    [Theory]
    [InlineData("<ExclusivePolicy>Reject</ExclusivePolicy>", true)]
    [InlineData("<ExclusivePolicy>Replace</ExclusivePolicy>", true)]
    [InlineData("<ExclusivePolicy>Stack</ExclusivePolicy>", false)]
    public void ExclusivePolicyIsRejectOrReplace(string body, bool valid)
    {
        Assert.Equal(valid, Validate(Minimal(body: body)).Count == 0);
    }

    [Theory]
    [InlineData("Icon=\"40\" IconSet=\"Spell\" Book=\"PrimarySkill\"", "")]
    [InlineData("Icon=\"40\" IconSet=\"Spell\" IsSkill=\"true\"", "")]
    [InlineData("Icon=\"40\" IconSet=\"Spell\"", "<Intents />")]
    [InlineData("Icon=\"40\" IconSet=\"Spell\"", "<Cooldown>10</Cooldown>")]
    public void TraitRejectsCastableFields(string attributes, string body)
    {
        Assert.NotEmpty(Validate(Minimal(attributes, body)));
    }

    // Deserialization

    [Fact]
    public void SampleTraitDeserializes()
    {
        var trait = Trait.Deserialize(Dockworker);

        Assert.Equal("Dockworker", trait.Name);
        Assert.Equal("Dockworker", trait.PrimaryKey);
        Assert.Equal(112, trait.Icon);
        Assert.Equal(IconSet.Skill, trait.IconSet);
        Assert.Equal("Background", trait.Category);
        Assert.Equal("Background", trait.ExclusiveGroup);
        Assert.Equal(ExclusivePolicy.Replace, trait.ExclusivePolicy);
        Assert.Equal(["Scribe", "Courtier"], trait.ConflictsWith);
        Assert.Equal("3", trait.StatModifiers.BonusStr);
    }

    [Fact]
    public void TraitWithoutOptionalBlocksGetsDefaults()
    {
        var trait = Trait.Deserialize(Minimal());

        Assert.Null(trait.Description);
        Assert.Null(trait.Category);
        Assert.Null(trait.ExclusiveGroup);
        Assert.Equal(ExclusivePolicy.Reject, trait.ExclusivePolicy);
        Assert.Empty(trait.ConflictsWith);
        Assert.Null(trait.StatModifiers);
    }

    // Loading

    [Fact]
    public void TraitsLoadFromTheTraitsDirectory()
    {
        var root = Directory.CreateTempSubdirectory("hybrasyl-traits-");
        try
        {
            var traits = root.CreateSubdirectory("traits");
            File.WriteAllText(Path.Combine(traits.FullName, "dockworker.xml"), Dockworker);
            File.WriteAllText(Path.Combine(traits.FullName, "tempered.xml"), Minimal());
            File.WriteAllText(Path.Combine(traits.FullName, "broken.xml"), "<Trait");

            var manager = new XmlDataManager(root.FullName);
            manager.LoadAll<Trait>();

            Assert.Equal(2, manager.Count<Trait>());
            Assert.True(manager.TryGetValue("Dockworker", out Trait dockworker));
            Assert.Equal(ExclusivePolicy.Replace, dockworker.ExclusivePolicy);
            Assert.True(manager.TryGetValue("Tempered", out Trait _));

            var result = manager.GetLoadResult<Trait>();
            Assert.Equal(3, result.TotalProcessed);
            Assert.Equal(1, result.ErrorCount);
        }
        finally
        {
            root.Delete(true);
        }
    }
}
