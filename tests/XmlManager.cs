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
// (C) 2020-2023 ERISCO, LLC
// 
// For contributors and individual authors please refer to CONTRIBUTORS.MD.

using System.Diagnostics;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;
using Serilog;
using Serilog.Sinks.TestCorrelator;
using Xunit.Abstractions;

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class XmlManagerTests
{
    private readonly ITestOutputHelper output;

    public XmlManagerTests(ITestOutputHelper output)
    {
        this.output = output;
        Manager = new XmlDataManager(Settings.XmlTests.JsonSettings.WorldDataDirectory);
        Watch = new Stopwatch();
        Watch.Start();
        Manager.LoadData();
        Watch.Stop();
    }

    protected XmlDataManager Manager { get; set; }
    private Stopwatch Watch { get;  }

    private Castable CategoryCastable { get;  } = new()
    {
        Book = Book.PrimarySkill, Name = "Test Skill", Categories = new List<Category>
        {
            new()  { Value = "Category 1" },
            new()  { Value = "Category 2" }
        }
    };

    [Fact]
    public void LoadDataContainsNoLoadErrors()
    {
        Log.Information("-- Load Data: Loading Checks --");

        var castableStatus = Manager.GetLoadResult<Castable>();
        Log.Information(
            $"castable load status: total {castableStatus.TotalProcessed} errors {castableStatus.ErrorCount} success {castableStatus.SuccessCount}");
        foreach (var kvp in castableStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, castableStatus.ErrorCount);

        var itemStatus = Manager.GetLoadResult<Item>();
        Log.Information(
            $"item load status: total {itemStatus.TotalProcessed} errors {itemStatus.ErrorCount} success {itemStatus.SuccessCount}");
        foreach (var kvp in itemStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, itemStatus.ErrorCount);

        var npcStatus = Manager.GetLoadResult<Npc>();
        Log.Information(
            $"npc load status: total {npcStatus.TotalProcessed} errors {npcStatus.ErrorCount} success {npcStatus.SuccessCount}");
        foreach (var kvp in npcStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, npcStatus.ErrorCount);

        var mapStatus = Manager.GetLoadResult<Map>();
        Log.Information(
            $"map load status: total {mapStatus.TotalProcessed} errors {mapStatus.ErrorCount} success {mapStatus.SuccessCount}");
        foreach (var kvp in mapStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, mapStatus.ErrorCount);

        var creatureStatus = Manager.GetLoadResult<Creature>();
        Log.Information(
            $"creature load status: total {creatureStatus.TotalProcessed} errors {creatureStatus.ErrorCount} success {creatureStatus.SuccessCount}");
        foreach (var kvp in creatureStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, creatureStatus.ErrorCount);

        var variantgroupStatus = Manager.GetLoadResult<VariantGroup>();
        Log.Information(
            $"variantgroup load status: total {variantgroupStatus.TotalProcessed} errors {variantgroupStatus.ErrorCount} success {variantgroupStatus.SuccessCount}");
        foreach (var kvp in variantgroupStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, variantgroupStatus.ErrorCount);

        var lootsetStatus = Manager.GetLoadResult<LootSet>();
        Log.Information(
            $"lootset load status: total {lootsetStatus.TotalProcessed} errors {lootsetStatus.ErrorCount} success {lootsetStatus.SuccessCount}");
        foreach (var kvp in lootsetStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, lootsetStatus.ErrorCount);

        var nationStatus = Manager.GetLoadResult<Nation>();
        Log.Information(
            $"nation load status: total {nationStatus.TotalProcessed} errors {nationStatus.ErrorCount} success {nationStatus.SuccessCount}");
        foreach (var kvp in nationStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, nationStatus.ErrorCount);

        var statusStatus = Manager.GetLoadResult<Status>();
        Log.Information(
            $"status load status: total {statusStatus.TotalProcessed} errors {statusStatus.ErrorCount} success {statusStatus.SuccessCount}");
        foreach (var kvp in statusStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, statusStatus.ErrorCount);

        var worldmapStatus = Manager.GetLoadResult<WorldMap>();
        Log.Information(
            $"worldmap load status: total {worldmapStatus.TotalProcessed} errors {worldmapStatus.ErrorCount} success {worldmapStatus.SuccessCount}");
        foreach (var kvp in worldmapStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, worldmapStatus.ErrorCount);

        var spawngroupStatus = Manager.GetLoadResult<SpawnGroup>();
        Log.Information(
            $"spawngroup load status: total {spawngroupStatus.TotalProcessed} errors {spawngroupStatus.ErrorCount} success {spawngroupStatus.SuccessCount}");
        foreach (var kvp in spawngroupStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, spawngroupStatus.ErrorCount);

        var elementTableStatus = Manager.GetLoadResult<ElementTable>();
        Log.Information(
            $"elementtable load status: total {elementTableStatus.TotalProcessed} errors {elementTableStatus.ErrorCount} success {elementTableStatus.SuccessCount}");
        foreach (var kvp in elementTableStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, elementTableStatus.ErrorCount);
        var creaturebehaviorsetStatus = Manager.GetLoadResult<CreatureBehaviorSet>();
        Log.Information(
            $"creaturebehaviorset load status: total {creaturebehaviorsetStatus.TotalProcessed} errors {creaturebehaviorsetStatus.ErrorCount} success {creaturebehaviorsetStatus.SuccessCount}");
        foreach (var kvp in creaturebehaviorsetStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, creaturebehaviorsetStatus.ErrorCount);
        var serverConfigStatus = Manager.GetLoadResult<ServerConfig>();
        Log.Information(
            $"serverconfig load status: total {serverConfigStatus.TotalProcessed} errors {serverConfigStatus.ErrorCount} success {serverConfigStatus.SuccessCount}");
        foreach (var kvp in serverConfigStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, serverConfigStatus.ErrorCount);

        var localizationStatus = Manager.GetLoadResult<Localization>();
        Log.Information(
            $"localization load status: total {localizationStatus.TotalProcessed} errors {localizationStatus.ErrorCount} success {localizationStatus.SuccessCount}");
        foreach (var kvp in localizationStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, localizationStatus.ErrorCount);
    }

    [Fact]
    public void LoadDataContainsNoProcessingErrors()
    {
        Log.Information("-- Load Data: Processing Checks --");
        var creatureStatus = Manager.GetProcessResult<Creature>();
        Log.Information(
            $"creature process status: total {creatureStatus.TotalProcessed} additional {creatureStatus.AdditionalCount}");
        foreach (var kvp in creatureStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, creatureStatus.ErrorCount);

        var itemStatus = Manager.GetProcessResult<Item>();
        Log.Information(
            $"item process status: total {itemStatus.TotalProcessed} additional {itemStatus.AdditionalCount}");
        foreach (var kvp in itemStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, itemStatus.ErrorCount);

        var creaturebehaviorsetStatus = Manager.GetProcessResult<CreatureBehaviorSet>();
        Log.Information(
            $"creaturebehaviorset process status: total {creaturebehaviorsetStatus.TotalProcessed} additional {creaturebehaviorsetStatus.AdditionalCount}");
        foreach (var kvp in creaturebehaviorsetStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, creaturebehaviorsetStatus.ErrorCount);
    }

    [Fact]
    public void LoadDataContainsExpectedData()
    {
        Log.Information("-- Load Data: Contains Expected Data --");
        var ts = Watch.Elapsed;
        var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
        Log.Information("LoadData Test\n-------------");
        Log.Information($"Time to load: {elapsedTime}");
        Log.Information($"Directory used: {Settings.XmlTests.JsonSettings.WorldDataDirectory}");
        Log.Information(
            $"Castables: {Manager.Count<Castable>()} Items: {Manager.Count<Item>()} NPCs: {Manager.Count<Npc>()} Maps: {Manager.Count<Map>()}");
        Log.Information(
            $"Creatures: {Manager.Count<Creature>()} Variants: {Manager.Count<VariantGroup>()} Lootsets: {Manager.Count<LootSet>()} Nations: {Manager.Count<Nation>()}");
        Log.Information(
            $"Statuses: {Manager.Count<Status>()} World Maps: {Manager.Count<WorldMap>()} Spawngroups: {Manager.Count<SpawnGroup>()} Behavior Sets: {Manager.Count<CreatureBehaviorSet>()}");
        Log.Information(
            $"Element Tables: {Manager.Count<ElementTable>()} Server Configs: {Manager.Count<ServerConfig>()} Localization Files: {Manager.Count<Localization>()}");
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "castables"), "*.xml").Length,
            Manager.Count<Castable>());
        Assert.Equal(288, Manager.Count<Item>());
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
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "elementtables"), "*.xml").Length,
            Manager.Count<ElementTable>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "serverconfigs"), "*.xml").Length,
            Manager.Count<ServerConfig>());
        Assert.Equal(Directory.GetFiles(Path.Join(Manager.RootPath, "localizations"), "*.xml").Length,
            Manager.Count<Localization>());

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
        var skills = Manager.FindSpells(25, 3, 3, 3, 3);
        Assert.NotNull(skills);
        Assert.NotEmpty(skills);
    }
    [Fact]
    public void FindSpellsWithCategory()
    {
        var skills = Manager.FindSpells(69, 36, 36, 36, 36, "ElementST");
        Assert.NotNull(skills);
        Assert.NotEmpty(skills);
    }

    [Fact]
    public void Find()
    {
        var item = Manager.Find<Item>(x => x.Name == "Test Boots 2");
        Assert.NotNull(item);
        Assert.Single(item);
        var item2 = Manager.Find<Item>(x => x.Name.Contains("Test Boots 2"));
        Assert.NotNull(item2);
        Assert.Equal(10, item2.Count());
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
        var c1 = Manager.GetStore<Castable>().GetByFilename("all_psp_nis.xml");
        Assert.NotNull(c1);
        Manager.FlagAsError(c1, XmlError.ProcessingError, "idk man its all wangled");
        Assert.Equal(XmlError.ProcessingError, c1.Error);
        Assert.Equal("idk man its all wangled", c1.LoadErrorMessage);
    }

    [Fact]
    public void GetByCategory()
    {

    }

    [Fact]
    public void VariantExists()
    {
        Assert.True(Manager.TryGetValueByIndex<Item>("Variant Test Boots 2", out var variant));
        Assert.True(Manager.TryGetValueByIndex<Item>("Test Boots 2", out var baseItem));
        Assert.True(Manager.TryGetValue<VariantGroup>("TestGroup", out var variantGroup));
    }

    [Fact]
    public void LogErrors()
    {
        using var ctx = TestCorrelator.CreateContext();
        Manager.LogResult(Log.Logger);
        var events = TestCorrelator.GetLogEventsFromCurrentContext();
        // TODO: improve coverage
        Assert.NotNull(events);
        Assert.NotEmpty(events);

    }
}
