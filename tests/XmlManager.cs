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
using System.IO;
using Hybrasyl.Xml.Enums;
using Hybrasyl.Xml.Manager;
using Hybrasyl.Xml.Objects;
using Serilog;
using Serilog.Sinks.TestCorrelator;
using Xunit.Abstractions;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Hybrasyl.XmlTests;

[Collection("Xml")]
public class XmlManagerTests : IClassFixture<XmlManagerFixture>
{
    private readonly ITestOutputHelper output;
    private readonly XmlManagerFixture fixture;

    public XmlManagerTests(ITestOutputHelper output, XmlManagerFixture fixture)
    {
        this.output = output;
        this.fixture = fixture;
    }

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

        var castableStatus = fixture.SyncManager.GetLoadResult<Castable>();
        Log.Information(
            $"castable load status: total {castableStatus.TotalProcessed} errors {castableStatus.ErrorCount} success {castableStatus.SuccessCount}");
        foreach (var kvp in castableStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, castableStatus.ErrorCount);

        var itemStatus = fixture.SyncManager.GetLoadResult<Item>();
        Log.Information(
            $"item load status: total {itemStatus.TotalProcessed} errors {itemStatus.ErrorCount} success {itemStatus.SuccessCount}");
        foreach (var kvp in itemStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, itemStatus.ErrorCount);

        var npcStatus = fixture.SyncManager.GetLoadResult<Npc>();
        Log.Information(
            $"npc load status: total {npcStatus.TotalProcessed} errors {npcStatus.ErrorCount} success {npcStatus.SuccessCount}");
        foreach (var kvp in npcStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, npcStatus.ErrorCount);

        var mapStatus = fixture.SyncManager.GetLoadResult<Map>();
        Log.Information(
            $"map load status: total {mapStatus.TotalProcessed} errors {mapStatus.ErrorCount} success {mapStatus.SuccessCount}");
        foreach (var kvp in mapStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, mapStatus.ErrorCount);

        var creatureStatus = fixture.SyncManager.GetLoadResult<Creature>();
        Log.Information(
            $"creature load status: total {creatureStatus.TotalProcessed} errors {creatureStatus.ErrorCount} success {creatureStatus.SuccessCount}");
        foreach (var kvp in creatureStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, creatureStatus.ErrorCount);

        var variantgroupStatus = fixture.SyncManager.GetLoadResult<VariantGroup>();
        Log.Information(
            $"variantgroup load status: total {variantgroupStatus.TotalProcessed} errors {variantgroupStatus.ErrorCount} success {variantgroupStatus.SuccessCount}");
        foreach (var kvp in variantgroupStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, variantgroupStatus.ErrorCount);

        var lootsetStatus = fixture.SyncManager.GetLoadResult<LootSet>();
        Log.Information(
            $"lootset load status: total {lootsetStatus.TotalProcessed} errors {lootsetStatus.ErrorCount} success {lootsetStatus.SuccessCount}");
        foreach (var kvp in lootsetStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, lootsetStatus.ErrorCount);

        var nationStatus = fixture.SyncManager.GetLoadResult<Nation>();
        Log.Information(
            $"nation load status: total {nationStatus.TotalProcessed} errors {nationStatus.ErrorCount} success {nationStatus.SuccessCount}");
        foreach (var kvp in nationStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, nationStatus.ErrorCount);

        var statusStatus = fixture.SyncManager.GetLoadResult<Status>();
        Log.Information(
            $"status load status: total {statusStatus.TotalProcessed} errors {statusStatus.ErrorCount} success {statusStatus.SuccessCount}");
        foreach (var kvp in statusStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, statusStatus.ErrorCount);

        var worldmapStatus = fixture.SyncManager.GetLoadResult<WorldMap>();
        Log.Information(
            $"worldmap load status: total {worldmapStatus.TotalProcessed} errors {worldmapStatus.ErrorCount} success {worldmapStatus.SuccessCount}");
        foreach (var kvp in worldmapStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, worldmapStatus.ErrorCount);

        var spawngroupStatus = fixture.SyncManager.GetLoadResult<SpawnGroup>();
        Log.Information(
            $"spawngroup load status: total {spawngroupStatus.TotalProcessed} errors {spawngroupStatus.ErrorCount} success {spawngroupStatus.SuccessCount}");
        foreach (var kvp in spawngroupStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, spawngroupStatus.ErrorCount);

        var elementTableStatus = fixture.SyncManager.GetLoadResult<ElementTable>();
        Log.Information(
            $"elementtable load status: total {elementTableStatus.TotalProcessed} errors {elementTableStatus.ErrorCount} success {elementTableStatus.SuccessCount}");
        foreach (var kvp in elementTableStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, elementTableStatus.ErrorCount);
        var creaturebehaviorsetStatus = fixture.SyncManager.GetLoadResult<CreatureBehaviorSet>();
        Log.Information(
            $"creaturebehaviorset load status: total {creaturebehaviorsetStatus.TotalProcessed} errors {creaturebehaviorsetStatus.ErrorCount} success {creaturebehaviorsetStatus.SuccessCount}");
        foreach (var kvp in creaturebehaviorsetStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, creaturebehaviorsetStatus.ErrorCount);
        var serverConfigStatus = fixture.SyncManager.GetLoadResult<ServerConfig>();
        Log.Information(
            $"serverconfig load status: total {serverConfigStatus.TotalProcessed} errors {serverConfigStatus.ErrorCount} success {serverConfigStatus.SuccessCount}");
        foreach (var kvp in serverConfigStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, serverConfigStatus.ErrorCount);

        var localizationStatus = fixture.SyncManager.GetLoadResult<Localization>();
        Log.Information(
            $"localization load status: total {localizationStatus.TotalProcessed} errors {localizationStatus.ErrorCount} success {localizationStatus.SuccessCount}");
        foreach (var kvp in localizationStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, localizationStatus.ErrorCount);
    }

    [Fact]
    public void LoadDataContainsNoProcessingErrors()
    {
        ContainsNoProcessingErrors(fixture.SyncManager);
    }

    [Fact]
    public void LoadDataAsyncContainsNoProcessingErrors()
    {
        ContainsNoProcessingErrors(fixture.AsyncManager);
    }

    private void ContainsNoProcessingErrors(XmlDataManager manager)
    {
        Log.Information("-- Load Data: Processing Checks --");
        var creatureStatus = manager.GetProcessResult<Creature>();
        Log.Information(
            $"creature process status: total {creatureStatus.TotalProcessed} additional {creatureStatus.AdditionalCount}");
        foreach (var kvp in creatureStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, creatureStatus.ErrorCount);

        var itemStatus = manager.GetProcessResult<Item>();
        Log.Information(
            $"item process status: total {itemStatus.TotalProcessed} additional {itemStatus.AdditionalCount}");
        foreach (var kvp in itemStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, itemStatus.ErrorCount);

        var creaturebehaviorsetStatus = manager.GetProcessResult<CreatureBehaviorSet>();
        Log.Information(
            $"creaturebehaviorset process status: total {creaturebehaviorsetStatus.TotalProcessed} additional {creaturebehaviorsetStatus.AdditionalCount}");
        foreach (var kvp in creaturebehaviorsetStatus.Errors) Log.Error($"{kvp.Key}: {kvp.Value}");

        Assert.Equal(0, creaturebehaviorsetStatus.ErrorCount); Log.Information("-- Load Data: Processing Checks --");
    }


    [Fact]
    public void LoadDataContainsExpectedData()
    {
        ContainsExpectedData(fixture.SyncManager);
    }

    [Fact]
    public void LoadDataAsyncContainsExpectedData()
    {
        ContainsExpectedData(fixture.AsyncManager);
    }

    private void ContainsExpectedData(XmlDataManager manager)
    {
        Log.Information("-- Load Data: Contains Expected Data --");
        Log.Information("LoadData Test\n-------------");
        Log.Information($"Directory used: {Settings.XmlTests.JsonSettings.WorldDataDirectory}");
        Log.Information(
            $"Castables: {manager.Count<Castable>()} Items: {manager.Count<Item>()} NPCs: {manager.Count<Npc>()} Maps: {manager.Count<Map>()}");
        Log.Information(
            $"Creatures: {manager.Count<Creature>()} Variants: {manager.Count<VariantGroup>()} Lootsets: {manager.Count<LootSet>()} Nations: {manager.Count<Nation>()}");
        Log.Information(
            $"Statuses: {manager.Count<Status>()} World Maps: {manager.Count<WorldMap>()} Spawngroups: {manager.Count<SpawnGroup>()} Behavior Sets: {manager.Count<CreatureBehaviorSet>()}");
        Log.Information(
            $"Element Tables: {manager.Count<ElementTable>()} Server Configs: {manager.Count<ServerConfig>()} Localization Files: {manager.Count<Localization>()}");
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "castables"), "*.xml").Length,
            manager.Count<Castable>());
        Assert.Equal(167, manager.Count<Item>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "npcs"), "*.xml").Length, manager.Count<Npc>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "maps"), "*.xml").Length, manager.Count<Map>());
        Assert.Equal(17, manager.Count<Creature>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "variantgroups"), "*.xml").Length,
            manager.Count<VariantGroup>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "lootsets"), "*.xml").Length,
            manager.Count<LootSet>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "nations"), "*.xml").Length,
            manager.Count<Nation>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "statuses"), "*.xml").Length,
            manager.Count<Status>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "worldmaps"), "*.xml").Length,
            manager.Count<WorldMap>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "spawngroups"), "*.xml").Length,
            manager.Count<SpawnGroup>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "creaturebehaviorsets"), "*.xml").Length,
            manager.Count<CreatureBehaviorSet>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "elementtables"), "*.xml").Length,
            manager.Count<ElementTable>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "serverconfigs"), "*.xml").Length,
            manager.Count<ServerConfig>());
        Assert.Equal(Directory.GetFiles(Path.Join(manager.RootPath, "localizations"), "*.xml").Length,
            manager.Count<Localization>());

    }

    [Fact]
    public void Get()
    {
        Assert.NotNull(fixture.SyncManager.Get<Map>(500));
        Assert.NotNull(fixture.SyncManager.Get<Nation>("Mileth"));
    }

    [Fact]
    public void GetByIndex()
    {
        Assert.NotNull(fixture.SyncManager.GetByIndex<Castable>("Assail"));
        Assert.NotNull(fixture.SyncManager.GetByIndex<Map>("Mileth Inn"));
    }

    [Fact]
    public void GetByGuid()
    {
        var mInn = fixture.SyncManager.Get<Map>(500);
        Assert.NotNull(mInn);
        var mInnByGuid = fixture.SyncManager.GetByGuid<Map>(mInn.Guid);
        Assert.NotNull(mInnByGuid);
    }

    [Fact]
    public void TryGetValueByIndex()
    {
        var found = fixture.SyncManager.TryGetValueByIndex<Castable>("Assail", out var result);
        Assert.True(found);
        Assert.NotNull(result);
    }

    [Fact]
    public void ContainsKey()
    {
        Assert.True(fixture.SyncManager.ContainsKey<Map>(500));
    }

    [Fact]
    public void ContainsIndex()
    {
        Assert.True(fixture.SyncManager.ContainsIndex<Castable>("Assail"));
    }

    [Fact]
    public void FindSkills()
    {
        var skills = fixture.SyncManager.FindSkills(25, 3, 3, 12, 12);
        Assert.NotNull(skills);
        Assert.NotEmpty(skills);
    }

    [Fact]
    public void FindSpells()
    {
        var skills = fixture.SyncManager.FindSpells(25, 3, 3, 3, 3);
        Assert.NotNull(skills);
        Assert.NotEmpty(skills);
    }
    [Fact]
    public void FindSpellsWithCategory()
    {
        var skills = fixture.SyncManager.FindSpells(69, 36, 36, 36, 36, "ElementST");
        Assert.NotNull(skills);
        Assert.NotEmpty(skills);
    }

    [Fact]
    public void Find()
    {
        var item = fixture.SyncManager.Find<Item>(x => x.Name == "Hide Boots");
        Assert.NotNull(item);
        Assert.Single(item);
        var item2 = fixture.SyncManager.Find<Item>(x => x.Name.Contains("Rotten"));
        Assert.NotNull(item2);
        Assert.Equal(6, item2.Count());
    }

    [Fact]
    public void AddToStore()
    {
        var c1 = CategoryCastable;
        fixture.SyncManager.Add(c1);
        fixture.SyncManager.Remove<Castable>(c1.PrimaryKey);
    }

    [Fact]
    public void FlagAsError()
    {
        var c1 = fixture.SyncManager.GetStore<Castable>().GetByFilename("all_psp_nis.xml");
        Assert.NotNull(c1);
        fixture.SyncManager.FlagAsError(c1, XmlError.ProcessingError, "idk man its all wangled");
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
        Assert.True(fixture.SyncManager.TryGetValueByIndex<Item>("Variant All Belt", out var variant));
        Assert.True(fixture.SyncManager.TryGetValueByIndex<Item>("Abundance Variant All Belt", out var baseItem));
        Assert.True(fixture.SyncManager.TryGetValue<VariantGroup>("enchant1", out var variantGroup));
    }

    [Fact]
    public void LogErrors()
    {
        using var ctx = TestCorrelator.CreateContext();
        fixture.SyncManager.LogResult(Log.Logger);
        var events = TestCorrelator.GetLogEventsFromCurrentContext();
        // TODO: improve coverage
        Assert.NotNull(events);
        Assert.NotEmpty(events);
    }

    [Fact]
    public void LoadDataAsync()
    {

    }
}
