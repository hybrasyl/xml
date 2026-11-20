using Hybrasyl.Xml.Manager;
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

    private static CreatureBehaviorSet SetWithBehavior(string name, CreatureHostility players, string cookie) => new()
    {
        Name = name,
        Behavior = new CreatureBehavior
        {
            Hostility = players == null ? null : new CreatureHostilitySettings { Players = players },
            SetCookies = cookie == null ? [] : [new CreatureCookie { Name = cookie, Value = "1" }]
        }
    };

    [Fact]
    public void CreatureBehaviorSetMissingImportLoadsTheSetAloneWithAnError()
    {
        var manager = new XmlDataManager();
        var set = new CreatureBehaviorSet { Name = "Orphan", Import = "NoSuchSet" };
        manager.Add(set, set.Name);

        CreatureBehaviorSet.ProcessAll(manager);

        Assert.True(manager.TryGetValue("Orphan", out CreatureBehaviorSet loaded));
        Assert.Same(set, loaded);
        var error = Assert.Single(manager.GetStore<CreatureBehaviorSet>().ProcessResult.Errors);
        Assert.Contains("NoSuchSet", error.Value);
    }

    [Fact]
    public void CreatureBehaviorSetMissingImportCountsAsProcessed()
    {
        var manager = new XmlDataManager();
        manager.Add(new CreatureBehaviorSet { Name = "Parent" }, "Parent");
        manager.Add(new CreatureBehaviorSet { Name = "Child", Import = "Parent" }, "Child");
        manager.Add(new CreatureBehaviorSet { Name = "Orphan", Import = "NoSuchSet" }, "Orphan");

        CreatureBehaviorSet.ProcessAll(manager);

        var result = manager.GetStore<CreatureBehaviorSet>().ProcessResult;
        Assert.Equal(2, result.TotalProcessed);
        Assert.Single(result.Errors);
    }

    // A set with its own Castables keeps its own Auto. A set with none takes the imported Auto.
    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void CreatureBehaviorSetImportTakesAutoFromTheSetWithCastables(bool setHasCastables, bool expected)
    {
        var set = new CreatureBehaviorSet
            { Name = "Child", Castables = setHasCastables ? new CreatureCastables { Auto = true } : null };
        var imported = new CreatureBehaviorSet { Name = "Parent", Castables = new CreatureCastables { Auto = false } };

        Assert.Equal(expected, (set & imported).Castables.Auto);
    }

    [Fact]
    public void CreatureBehaviorSetImportWithNullImmunitiesKeepsTheSetsOwn()
    {
        var own = new CreatureImmunity { Type = CreatureImmunityType.Element, Value = "Fire" };
        var set = new CreatureBehaviorSet { Name = "Child", Immunities = [own] };
        var imported = new CreatureBehaviorSet { Name = "Parent", Immunities = null };

        var merged = set & imported;

        Assert.Equal([own], merged.Immunities);
    }

    [Fact]
    public void CreatureBehaviorSetImportWithNoImmunitiesOfItsOwnTakesTheImported()
    {
        var inherited = new CreatureImmunity { Type = CreatureImmunityType.Element, Value = "Water" };
        var set = new CreatureBehaviorSet { Name = "Child", Immunities = null };
        var imported = new CreatureBehaviorSet { Name = "Parent", Immunities = [inherited] };

        var merged = set & imported;

        Assert.Equal([inherited], merged.Immunities);
    }

    [Fact]
    public void CreatureBehaviorSetImportAppliesTheImportedCookiesWhenTheSetHasANullList()
    {
        var set = SetWithBehavior("Child", null, null);
        set.Behavior.SetCookies = null;
        var imported = SetWithBehavior("Parent", null, "parent_cookie");

        var merged = set & imported;

        Assert.Equal(["parent_cookie"], merged.Behavior.SetCookies.Select(c => c.Name));
    }

    [Fact]
    public void CreatureBehaviorSetImportKeepsTheSetsOwnHostilityAndCookies()
    {
        var set = SetWithBehavior("Child", new CreatureHostility(), "child_cookie");
        var imported = SetWithBehavior("Parent", new CreatureHostility(), "parent_cookie");

        var merged = set & imported;

        Assert.Same(set.Behavior.Hostility, merged.Behavior.Hostility);
        Assert.Equal(["child_cookie"], merged.Behavior.SetCookies.Select(c => c.Name));
    }

    [Fact]
    public void CreatureBehaviorSetImportAppliesTheImportedHostilityAndCookiesWhenTheSetHasNone()
    {
        var set = SetWithBehavior("Child", null, null);
        var imported = SetWithBehavior("Parent", new CreatureHostility(), "parent_cookie");

        var merged = set & imported;

        Assert.Same(imported.Behavior.Hostility, merged.Behavior.Hostility);
        Assert.Equal(["parent_cookie"], merged.Behavior.SetCookies.Select(c => c.Name));
    }
}