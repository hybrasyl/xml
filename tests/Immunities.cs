using Hybrasyl.XmlTests;
using Hybrasyl.Xml.Objects;
using Xunit.Abstractions;

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
    }
}


