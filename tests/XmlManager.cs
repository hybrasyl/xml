using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;
using System.Diagnostics;
using Hybrasyl.Xml.Enums;
using Serilog;
using Serilog.Core;
using Xunit.Abstractions;

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class XmlManagerTests
{
    private readonly ITestOutputHelper output;
    protected XmlDataManager Manager { get; set; }
    private Stopwatch Watch { get; set; }

    public XmlManagerTests(ITestOutputHelper output)
    {
        this.output = output;
        Manager = new XmlDataManager(Settings.XmlTests.JsonSettings.WorldDataDirectory);
        Watch = new Stopwatch();
        Watch.Start();
        Manager.LoadData();
        Watch.Stop();
    }

    private Castable CategoryCastable { get; set; } = new Castable
    {
        Book = Book.PrimarySkill, Name = "Test Skill", Categories = new List<Category>()
        {
            new Category { Value = "Category 1" },
            new Category { Value = "Category 2" }
        }
    };

    [Fact]
    public void LoadDataContainsNoLoadErrors()
    {
        Log.Information("-- Load Data: Loading Checks --");

        var castableStatus = Manager.GetLoadStatus<Castable>();
        Log.Information(
            $"castable load status: total {castableStatus.TotalProcessed} errors {castableStatus.ErrorCount} success {castableStatus.SuccessCount}");
        foreach (var kvp in castableStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, castableStatus.ErrorCount);

        var itemStatus = Manager.GetLoadStatus<Item>();
        Log.Information(
            $"item load status: total {itemStatus.TotalProcessed} errors {itemStatus.ErrorCount} success {itemStatus.SuccessCount}");
        foreach (var kvp in itemStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, itemStatus.ErrorCount);

        var npcStatus = Manager.GetLoadStatus<Npc>();
        Log.Information(
            $"npc load status: total {npcStatus.TotalProcessed} errors {npcStatus.ErrorCount} success {npcStatus.SuccessCount}");
        foreach (var kvp in npcStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, npcStatus.ErrorCount);

        var mapStatus = Manager.GetLoadStatus<Map>();
        Log.Information(
            $"map load status: total {mapStatus.TotalProcessed} errors {mapStatus.ErrorCount} success {mapStatus.SuccessCount}");
        foreach (var kvp in mapStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, mapStatus.ErrorCount);

        var creatureStatus = Manager.GetLoadStatus<Creature>();
        Log.Information(
            $"creature load status: total {creatureStatus.TotalProcessed} errors {creatureStatus.ErrorCount} success {creatureStatus.SuccessCount}");
        foreach (var kvp in creatureStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, creatureStatus.ErrorCount);

        var variantgroupStatus = Manager.GetLoadStatus<VariantGroup>();
        Log.Information(
            $"variantgroup load status: total {variantgroupStatus.TotalProcessed} errors {variantgroupStatus.ErrorCount} success {variantgroupStatus.SuccessCount}");
        foreach (var kvp in variantgroupStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, variantgroupStatus.ErrorCount);

        var lootsetStatus = Manager.GetLoadStatus<LootSet>();
        Log.Information(
            $"lootset load status: total {lootsetStatus.TotalProcessed} errors {lootsetStatus.ErrorCount} success {lootsetStatus.SuccessCount}");
        foreach (var kvp in lootsetStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, lootsetStatus.ErrorCount);

        var nationStatus = Manager.GetLoadStatus<Nation>();
        Log.Information(
            $"nation load status: total {nationStatus.TotalProcessed} errors {nationStatus.ErrorCount} success {nationStatus.SuccessCount}");
        foreach (var kvp in nationStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, nationStatus.ErrorCount);

        var statusStatus = Manager.GetLoadStatus<Status>();
        Log.Information(
            $"status load status: total {statusStatus.TotalProcessed} errors {statusStatus.ErrorCount} success {statusStatus.SuccessCount}");
        foreach (var kvp in statusStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, statusStatus.ErrorCount);

        var worldmapStatus = Manager.GetLoadStatus<WorldMap>();
        Log.Information(
            $"worldmap load status: total {worldmapStatus.TotalProcessed} errors {worldmapStatus.ErrorCount} success {worldmapStatus.SuccessCount}");
        foreach (var kvp in worldmapStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, worldmapStatus.ErrorCount);

        var spawngroupStatus = Manager.GetLoadStatus<SpawnGroup>();
        Log.Information(
            $"spawngroup load status: total {spawngroupStatus.TotalProcessed} errors {spawngroupStatus.ErrorCount} success {spawngroupStatus.SuccessCount}");
        foreach (var kvp in spawngroupStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, spawngroupStatus.ErrorCount);

        var creaturebehaviorsetStatus = Manager.GetLoadStatus<CreatureBehaviorSet>();
        Log.Information(
            $"creaturebehaviorset load status: total {creaturebehaviorsetStatus.TotalProcessed} errors {creaturebehaviorsetStatus.ErrorCount} success {creaturebehaviorsetStatus.SuccessCount}");
        foreach (var kvp in creaturebehaviorsetStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, creaturebehaviorsetStatus.ErrorCount);
    }

    [Fact]
    public void LoadDataContainsNoProcessingErrors()
    {
        Log.Information("-- Load Data: Processing Checks --");
        var creatureStatus = Manager.GetProcessStatus<Creature>();
        Log.Information(
            $"creature process status: total {creatureStatus.TotalProcessed} additional {creatureStatus.AdditionalCount}");
        foreach (var kvp in creatureStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, creatureStatus.ErrorCount);

        var itemStatus = Manager.GetProcessStatus<Item>();
        Log.Information(
            $"item process status: total {itemStatus.TotalProcessed} additional {itemStatus.AdditionalCount}");
        foreach (var kvp in itemStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, itemStatus.ErrorCount);

        var creaturebehaviorsetStatus = Manager.GetProcessStatus<CreatureBehaviorSet>();
        Log.Information(
            $"creaturebehaviorset process status: total {creaturebehaviorsetStatus.TotalProcessed} additional {creaturebehaviorsetStatus.AdditionalCount}");
        foreach (var kvp in creaturebehaviorsetStatus.Errors)
        {
            Log.Error($"{kvp.Key}: {kvp.Value}");
        }

        Assert.Equal(0, creaturebehaviorsetStatus.ErrorCount);
    }

    [Fact]
    public void LoadDataContainsExpectedData()
    {
        Log.Information("-- Load Data: Contains Expected Data --");
        TimeSpan ts = Watch.Elapsed;
        var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
        Log.Information($"LoadData Test\n-------------");
        Log.Information($"Time to load: {elapsedTime}");
        Log.Information($"Directory used: {Settings.XmlTests.JsonSettings.WorldDataDirectory}");
        Log.Information(
            $"Castables: {Manager.Count<Castable>()} Items: {Manager.Count<Item>()} NPCs: {Manager.Count<Npc>()} Maps: {Manager.Count<Map>()}");
        Log.Information(
            $"Creatures: {Manager.Count<Creature>()} Variants: {Manager.Count<VariantGroup>()} Lootsets: {Manager.Count<LootSet>()} Nations: {Manager.Count<Nation>()}");
        Log.Information(
            $"Statuses: {Manager.Count<Status>()} World Maps: {Manager.Count<WorldMap>()} Spawngroups: {Manager.Count<SpawnGroup>()} Behavior Sets: {Manager.Count<CreatureBehaviorSet>()}");
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "castables"), "*.xml").Length,
            Manager.Count<Castable>());
        Assert.Equal(287, Manager.Count<Item>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "npcs"), "*.xml").Length, Manager.Count<Npc>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "maps"), "*.xml").Length, Manager.Count<Map>());
        Assert.Equal(17, Manager.Count<Creature>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "variantgroups"), "*.xml").Length,
            Manager.Count<VariantGroup>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "lootsets"), "*.xml").Length,
            Manager.Count<LootSet>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "nations"), "*.xml").Length,
            Manager.Count<Nation>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "statuses"), "*.xml").Length,
            Manager.Count<Status>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "worldmaps"), "*.xml").Length,
            Manager.Count<WorldMap>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "spawngroups"), "*.xml").Length,
            Manager.Count<SpawnGroup>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "creaturebehaviorsets"), "*.xml").Length,
            Manager.Count<CreatureBehaviorSet>());
    }

    [Fact]
    public void Get()
    {
        Assert.NotNull(Manager.Get<Map>(500));
        Assert.NotNull(Manager.Get<Nation>("Mileth"));
    }

    [Fact]
    public void GetByIndex()
    {
        Assert.NotNull(Manager.GetByIndex<Castable>("Assail"));
        Assert.NotNull(Manager.GetByIndex<Map>("Mileth Inn"));
    }

    [Fact]
    public void GetByGuid()
    {
        var mInn = Manager.Get<Map>(500);
        Assert.NotNull(mInn);
        var mInnByGuid = Manager.GetByGuid<Map>(mInn.Guid);
        Assert.NotNull(mInnByGuid);
    }

    [Fact]
    public void TryGetValueByIndex()
    {
        var found = Manager.TryGetValueByIndex<Castable>("Assail", out var result);
        Assert.True(found);
        Assert.NotNull(result);
    }

    [Fact]
    public void ContainsKey()
    {
        Assert.True(Manager.ContainsKey<Map>(500));
    }

    [Fact]
    public void ContainsIndex()
    {
        Assert.True(Manager.ContainsIndex<Castable>("Assail"));
    }

    [Fact]
    public void FindSkills()
    {
        var skills = Manager.FindSkills(25, 3, 3, 12, 12);
        Assert.NotNull(skills);
        Assert.NotEmpty(skills);
    }

    [Fact]
    public void FindSpells()
    {
        var skills = Manager.FindSpells(25, 3,3,3,3);
        Assert.NotNull(skills);
        Assert.NotEmpty(skills);

    }

    [Fact]
    public void AddToStore()
    {
        var c1 = CategoryCastable;
        Manager.Add(c1);
    }

    [Fact]
    public void FlagAsError()
    {
        var c1 = Manager.GetStore<Castable>().GetByFilename("all_tst_plus-wis.xml");
        Assert.NotNull(c1);
        Manager.FlagAsError(c1, XmlError.ProcessingError, "idk man its all wangled");
        Assert.Equal(XmlError.ProcessingError, c1.Error);
        Assert.Equal("idk man its all wangled", c1.LoadErrorMessage);
    }

    [Fact]
    public void GetByCategory()
    {
    }
}
