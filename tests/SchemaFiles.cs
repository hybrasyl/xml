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
using System.Linq;
using System.Xml.Linq;
using Hybrasyl.Xml.Enums;
using Xunit;

namespace Hybrasyl.XmlTests;

/// <summary>Checks on the XSD files and on the hand-written enums that match them.</summary>
public class SchemaFileTests
{
    private static readonly XNamespace Xs = "http://www.w3.org/2001/XMLSchema";

    private static readonly (string File, XDocument Doc)[] Schemas =
        SchemaValidation.XsdFiles.Select(selector: f => (Path.GetFileName(f), XDocument.Load(f))).ToArray();

    // .NET accepts a space at the start or end of a type name, but libxml2 (lxml, xmllint) does not
    // compile the schema (HXML-3).
    [Fact]
    public void NoTypeNameOrReferenceHasStrayWhitespace()
    {
        string[] attributes = ["name", "type", "base", "ref", "itemType", "memberTypes", "substitutionGroup"];
        var bad = new List<string>();

        foreach (var (file, doc) in Schemas)
        foreach (var element in doc.Descendants().Where(predicate: e => e.Name.Namespace == Xs))
        foreach (var name in attributes)
        {
            var value = element.Attribute(name)?.Value;
            if (value is not null && value != value.Trim())
                bad.Add($"{file}: {element.Name.LocalName} {name}=\"{value}\"");
        }

        Assert.True(bad.Count == 0, string.Join("; ", bad));
    }

    // The flags enums are written by hand, and several go on the wire as a bit mask, so their values are
    // pinned here. Append a new member with the next bit. Do not renumber.
    public static TheoryData<Type, string> FlagsEnums => new()
    {
        { typeof(DamageFlags), "None=1 NoResistance=2 NoThreat=4 Nonlethal=8 NoDodge=16 NoCrit=32 NoElement=64 NoPositional=128" },
        {
            typeof(ItemFlags),
            "Bound=1 Depositable=2 Enchantable=4 Consecratable=8 Tailorable=16 Smithable=32 Exchangeable=64 Vendorable=128 " +
            "Perishable=256 UniqueInventory=512 MasterOnly=1024 UniqueEquipped=2048 Identifiable=4096 Undamageable=8192 Consumable=16384"
        },
        { typeof(MapFlags), "Snow=1 Rain=2 Dark=4 NoMap=8 Winter=16" },
        { typeof(SpawnFlags), "Active=1 MovementDisabled=2 AiDisabled=4 DeathDisabled=8" },
        {
            typeof(CreatureCondition),
            "Stun=1 Sleep=2 Root=4 Blind=8 Coma=16 Poison=32 Sight=64 Invisible=128 Mute=256 Invulnerable=512 Charm=1024 " +
            "ProhibitItemUse=2048 ProhibitEquipChange=4096 ProhibitSpeech=8192 ProhibitWhisper=16384 ProhibitShout=32768 " +
            "Disoriented=65536 Disarm=131072 Fear=262144 ProhibitHpRegen=524288 ProhibitMpRegen=1048576 " +
            "ProhibitHpIncrease=2097152 ProhibitMpIncrease=4194304 ProhibitMpDecrease=8388608 ProhibitXpIncrease=16777216"
        }
    };

    [Fact]
    public void EveryFlagsEnumIsPinned()
    {
        var pinned = FlagsEnums.Select(selector: row => (Type) row[0]).ToHashSet();
        var flags = typeof(DamageFlags).Assembly.GetTypes()
            .Where(predicate: t => t.IsEnum && t.Namespace == typeof(DamageFlags).Namespace &&
                                   t.IsDefined(typeof(FlagsAttribute), false));

        Assert.All(flags, action: t => Assert.Contains(t, pinned));
    }

    [Theory]
    [MemberData(nameof(FlagsEnums))]
    public void FlagsEnumKeepsItsValues(Type type, string expected)
    {
        var actual = string.Join(" ", Enum.GetValues(type).Cast<object>()
            .Select(selector: v => $"{Enum.GetName(type, v)}={Convert.ToInt64(v)}"));

        Assert.Equal(expected, actual);
    }

    // Each token that the schema allows needs a member in the hand-written enum, and the reverse.
    [Theory]
    [MemberData(nameof(FlagsEnums))]
    public void FlagsEnumMatchesTheSchemaList(Type type, string _)
    {
        var simpleType = Schemas.SelectMany(selector: s => s.Doc.Descendants(Xs + "simpleType"))
            .Single(predicate: t => t.Attribute("name")?.Value == type.Name);
        var tokens = simpleType.Descendants(Xs + "enumeration").Select(selector: e => e.Attribute("value")!.Value)
            .Order(StringComparer.Ordinal);

        Assert.Equal(tokens, Enum.GetNames(type).Order(StringComparer.Ordinal));
    }
}
