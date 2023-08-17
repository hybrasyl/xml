using Hybrasyl.Xml.Objects;
using Xunit.Abstractions;

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class XmlEntityTests : IClassFixture<XmlManagerFixture>
{
    private readonly ITestOutputHelper output;
    private readonly XmlManagerFixture fixture;

    public XmlEntityTests(ITestOutputHelper output, XmlManagerFixture fixture)
    {
        this.output = output;
        this.fixture = fixture;
    }

    // xsd2code has a few oddities / bugs that result in the wrong type being used or 
    // a type being a list when it is a singular object. These tests ensure nothing gets
    // committed and pushed out to Nuget with those errors.
    [Fact]
    public void CreatureAssailSoundIsByte()
    {
        // Occasionally xsd2code will make this an sbyte, for unknown reasons,
        // so we test for that hre
        var f = new Creature();
        Assert.IsType<byte>(f.AssailSound);
    }

    [Fact]
    public void ElementSourceTableIsNotList()
    {
        var check = new ElementTableSourceElement();
        Assert.IsType<ElementType>(check.Element);
    }

    [Fact]
    public void ElementTargetTableIsNotList()
    {
        var check = new ElementTableTargetElement();
        Assert.IsType<ElementType>(check.Element);
    }

    [Fact]
    public void EquipmentRestrictionTypeIsNotList()
    {
        var check = new EquipmentRestriction();
        Assert.IsType<WeaponType>(check.WeaponType);
    }

    [Fact]
    public void BehaviorSetCastingSetHealthPercentageNonNegative()
    {
        var check = new CreatureBehaviorSet();
        check.Behavior = new CreatureBehavior();
        check.Behavior.CastingSets = new List<CreatureCastingSet>
        {
            new()
        };
        Assert.True(check.Behavior.CastingSets.First().HealthPercentage >= 0);
    }

    [Fact]
    public void BehaviorSetImportStatAlloc()
    {
        var original = new CreatureBehaviorSet();
        var import = new CreatureBehaviorSet();
        original.StatAlloc = null;
        import.StatAlloc = "Str Str Int Con Dex";
        var merged = original & import;
        Assert.Equal(import.StatAlloc, merged.StatAlloc);

        original = new CreatureBehaviorSet();
        import = new CreatureBehaviorSet();
        import.StatAlloc = null;
        original.StatAlloc = "Str Str Int Con Dex";
        merged = import & original;
        Assert.Equal(original.StatAlloc, merged.StatAlloc);
    }

    [Fact]
    public void ItemMetafileDescription()
    {
        var item = new Item();
        item.Name = "Test";
        item.Properties.StatModifiers = new StatModifiers() { BonusDmg = "0.005", BonusHit = "0.004", BonusInt = "2" };
        Assert.Equal("+2 Int\n+0.5% Dmg\n+0.4% Hit\n",item.Properties.StatModifiers.BonusString);
    }

    [Fact]
    public void CreatureHostilitySettings()
    {
        var creature = fixture.SyncManager.Get<Creature>("Mouse");
        Assert.NotNull(creature);
        Assert.NotNull(creature.Hostility);
        Assert.NotNull(creature.Hostility.Players);
    }

    [Fact]
    public void VariantBeltElementalTest()
    {
        var belt = fixture.SyncManager.Find<Item>(x => x.Name.Contains("Light Variant Single Belt")).FirstOrDefault();
        Assert.NotNull(belt);
        Assert.True(belt.Properties.StatModifiers.BaseDefensiveElement == ElementType.Light );
    }

}
