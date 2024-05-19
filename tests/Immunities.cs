using Hybrasyl.Xml.Objects;

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class ImmunitiesTests(ITestOutputHelper output, XmlManagerFixture fixture) : IClassFixture<XmlManagerFixture>
{
    private readonly ITestOutputHelper Output = output;
    private readonly XmlManagerFixture Fixture = fixture;

    [Fact]
    public void CastableImmunity()
    {
        var castableImmune = Fixture.SyncManager.Get<CreatureBehaviorSet>("CastableImmune");
        Assert.NotNull(castableImmune);
        var immuneTo = Fixture.SyncManager.GetByIndex<Castable>("ard srad");
        Assert.NotNull(immuneTo);
        Assert.True(castableImmune.ImmuneToCastable(immuneTo, out var immunity));
        Assert.True(castableImmune.ImmuneToCastable("ard srad", out immunity));
        Assert.True(castableImmune.ImmuneToCastable("aRd sRaD", out immunity));
        var notImmune = Fixture.SyncManager.Get<CreatureBehaviorSet>("BasicAssCritter");
        Assert.NotNull(notImmune);
        Assert.False(notImmune.ImmuneToCastable(immuneTo, out immunity));
        Assert.False(notImmune.ImmuneToCastable("ard srad", out immunity));
    }

    [Fact]
    public void CastableCategoryImmunity()
    {
        var castableImmune = Fixture.SyncManager.Get<CreatureBehaviorSet>("CastCatImmune");
        Assert.NotNull(castableImmune);
        Assert.True(castableImmune.ImmuneToCastableCategory("Debuff", out var immunity));
        Assert.True(castableImmune.ImmuneToCastableCategory("DeBuFf", out immunity));
        Assert.True(castableImmune.ImmuneToCastableCategories(new List<string> { "debuff", "DEBUFF" }, out _));
        Assert.True(castableImmune.ImmuneToCastableCategories(new List<string> { "bazbar", "DEBUFF" }, out _));
        Assert.False(castableImmune.ImmuneToCastableCategories(new List<string> { "bazbar", "quux" }, out _));

    }

    [Fact]
    public void StatusImmunity()
    {
        var statusImmune = Fixture.SyncManager.Get<CreatureBehaviorSet>("StatusImmune");
        var status = Fixture.SyncManager.Get<Status>("TestPlusAc");

        Assert.NotNull(statusImmune);
        Assert.NotNull(status);
        Assert.True(statusImmune.ImmuneToStatus("TeStPlUsAc", out _));
        Assert.True(statusImmune.ImmuneToStatus("testplusac", out _));
        Assert.True(statusImmune.ImmuneToStatus(status, out _));
    }

    [Fact]
    public void StatusCategoryImmunity()
    {
        var statusImmune = Fixture.SyncManager.Get<CreatureBehaviorSet>("StatCatImmune");
        Assert.NotNull(statusImmune);
        Assert.True(statusImmune.ImmuneToStatusCategory("Str", out _));
        Assert.True(statusImmune.ImmuneToStatusCategories(new List<string> { "str", "STR" }, out _));
        Assert.True(statusImmune.ImmuneToStatusCategories(new List<string> { "iNt", "STR" }, out _));
        Assert.False(statusImmune.ImmuneToStatusCategories(new List<string> { "int", "wis" }, out _));

    }

    [Fact]
    public void ElementalImmunity()
    {
        var elementalImmune = Fixture.SyncManager.Get<CreatureBehaviorSet>("ElementImmune");
        Assert.NotNull(elementalImmune);
        Assert.True(elementalImmune.ImmuneToElement("Fire", out _));
        Assert.True(elementalImmune.ImmuneToElement(ElementType.Fire, out _));
    }

    [Fact]
    public void ImmunityImports()
    {
        var importImmune = Fixture.SyncManager.Get<CreatureBehaviorSet>("ImmunityImport");
        var importedImmune = Fixture.SyncManager.Get<CreatureBehaviorSet>(importImmune.Import);
        Assert.NotNull(importImmune);
        Assert.NotNull(importedImmune);
        Assert.Equal(importImmune.Immunities.Count, importedImmune.Immunities.Count + importImmune.Immunities.Count);
    }
}


