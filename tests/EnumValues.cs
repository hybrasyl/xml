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
using System.Text.Json;
using Hybrasyl.Xml.Objects;
using Xunit;

namespace Hybrasyl.XmlTests;

/// <summary>
///     Holds every generated enum member to the value allocated in
///     <c>tools/enum-values.json</c>.
/// </summary>
/// <remarks>
///     These enums are generated from the XSD, where a member's number is its declaration
///     position, and several are cast to a byte and put on the wire. Without this the schema
///     is an undeclared wire format: moving an <c>xs:enumeration</c> changes what goes over
///     the network with no compile error anywhere. The registry is the authority and the
///     assembly is checked against it, so a generator change, a partial patch or a hand edit
///     all fail here rather than in production.
/// </remarks>
public class EnumValueTests
{
    private static readonly Dictionary<string, Dictionary<string, int>> Registry =
        JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, int>>>(
            File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "enum-values.json")))
        ?? throw new InvalidOperationException("enum-values.json did not deserialize.");

    [Fact]
    public void RegistryIsNotEmpty()
    {
        // The whole suite below passes vacuously if the file failed to load or ship.
        Assert.NotEmpty(Registry);
        Assert.All(Registry.Values, members => Assert.NotEmpty(members));
    }

    [Fact]
    public void EveryPinnedMemberHasItsAllocatedValue()
    {
        var assembly = typeof(Class).Assembly;

        foreach (var (enumName, members) in Registry)
        {
            var type = assembly.GetTypes()
                .FirstOrDefault(predicate: t => t.IsEnum && t.Name == enumName);

            Assert.True(type != null, $"{enumName} is pinned but no longer exists.");

            foreach (var (member, expected) in members)
            {
                Assert.True(Enum.IsDefined(type!, member),
                    $"{enumName}.{member} is pinned but missing from the assembly. Removing a "
                    + "member is a deliberate registry edit, and its number stays retired.");
                Assert.Equal(expected, Convert.ToInt32(Enum.Parse(type!, member)));
            }
        }
    }

    [Fact]
    public void NoGeneratedEnumMemberIsUnpinned()
    {
        // The direction the test above cannot catch: a member added to the XSD lands in the
        // assembly with a position-derived number and nothing pins it.
        var unpinned = new List<string>();

        foreach (var (enumName, members) in Registry)
        {
            var type = typeof(Class).Assembly.GetTypes()
                .FirstOrDefault(predicate: t => t.IsEnum && t.Name == enumName);
            if (type == null) continue;

            unpinned.AddRange(Enum.GetNames(type)
                .Where(predicate: n => !members.ContainsKey(n))
                .Select(selector: n => $"{enumName}.{n}"));
        }

        Assert.True(unpinned.Count == 0,
            $"Unpinned generated enum member(s): {string.Join(", ", unpinned)}. Allocate a "
            + "value in tools/enum-values.json; append, never renumber.");
    }

    [Fact]
    public void AllocatedValuesAreUniqueWithinAnEnum()
    {
        foreach (var (enumName, members) in Registry)
        {
            var duplicated = members.GroupBy(keySelector: kvp => kvp.Value)
                .Where(predicate: g => g.Count() > 1)
                .Select(selector: g => $"{g.Key} ({string.Join("/", g.Select(x => x.Key))})")
                .ToList();

            Assert.True(duplicated.Count == 0,
                $"{enumName} allocates a value twice: {string.Join(", ", duplicated)}.");
        }
    }
}
