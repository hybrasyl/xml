using Hybrasyl.Xml.Objects;
using Xunit;
using Xunit.Abstractions;

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class ImportTests(ITestOutputHelper output, XmlManagerFixture fixture) : IClassFixture<XmlManagerFixture>
{
    private readonly XmlManagerFixture Fixture = fixture;
    private readonly ITestOutputHelper Output = output;

    [Fact]
    public void StatModifiersWithFormulasShouldMergeCorrectly()
    {
        var a = new StatModifiers()
        {
            BaseHp = "SOURCESTR * 50"
        };
        var b = new StatModifiers
        {
            BaseHp = "SOURCESTR * 150"
        };

        var c = a + b;
        Assert.NotNull(c);
        Assert.NotEmpty(c.BaseHp);
        Assert.Equal("SOURCESTR * 50 + SOURCESTR * 150", c.BaseHp);
    }

    [Fact]
    public void CreatureBehaviorSetImportKeepsTheElementalModifiersOfBothSets()
    {
        static CreatureBehaviorSet Set(string name, string element) => new()
        {
            Name = name,
            StatModifiers = new StatModifiers
            {
                ElementalModifiers =
                [
                    new ElementalModifier { Type = ElementalModifierType.Resistance, Element = element, Modifier = 0.1 }
                ]
            }
        };

        var merged = Set("Child", "Fire") & Set("Parent", "Water");

        Assert.Equal(["Fire", "Water"], merged.StatModifiers.Resistances.Select(m => m.Element));
    }

    [Fact]
    public void CreatureBehaviorSetImportsShouldImportCorrectly()
    {
        Assert.True(Fixture.SyncManager.TryGetValue("RareGabbaDynamic", out CreatureBehaviorSet setWithImport));
        Assert.True(Fixture.SyncManager.TryGetValue("RareGabba", out CreatureBehaviorSet importedSet));
        Assert.NotNull(setWithImport);
        Assert.NotNull(importedSet);
        Assert.Equal(importedSet.Castables.Castable.Count + 1, setWithImport.Castables.Castable.Count);
        Assert.Equal(importedSet.Behavior.CastingSets.Count + 1,  setWithImport.Behavior.CastingSets.Count);
        foreach (var importedCastingSet in importedSet.Behavior.CastingSets)
        {
            Assert.Contains(importedCastingSet, setWithImport.Behavior.CastingSets);
        }
        Assert.Contains("mor ioc", setWithImport.Castables.Castable);
        Assert.Contains(setWithImport.Behavior.CastingSets, x => x.Castable.FirstOrDefault(y => y.Value == "mor ioc") != null);
        Assert.NotNull(setWithImport.StatModifiers);
        Assert.NotNull(importedSet.StatModifiers);
        Assert.NotEmpty(setWithImport.StatAlloc);
        Assert.NotEmpty(setWithImport.StatModifiers.BaseHp);
        Assert.NotEmpty(setWithImport.BeforeImport.StatModifiers.BaseHp);
        Assert.NotEmpty(importedSet.StatModifiers.BaseHp);
        Assert.Equal($"{setWithImport.BeforeImport.StatModifiers.BaseHp} + {importedSet.StatModifiers.BaseHp}", setWithImport.StatModifiers.BaseHp);

    }
}