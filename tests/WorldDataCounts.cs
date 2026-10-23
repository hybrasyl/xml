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

using System.Xml.Linq;

namespace Hybrasyl.XmlTests;

/// <summary>
///     What a world data directory should load into, counted from the XML with XDocument rather
///     than through the library under test. Items and creatures expand from templates, so their
///     totals apply the same expansion rules the loader does.
/// </summary>
public static class WorldDataCounts
{
    private static readonly XNamespace Ns = "http://www.hybrasyl.com/XML/Hybrasyl/2020-02";

    // The loader reads subdirectories and skips any path containing ".ignore".
    public static IEnumerable<string> XmlFiles(string root, string directory)
    {
        var path = Path.Join(root, directory);
        return Directory.Exists(path)
            ? Directory.GetFiles(path, "*.xml", SearchOption.AllDirectories)
                .Where(predicate: file => !file.Contains(".ignore"))
            : [];
    }

    public static int Files(string root, string directory) => XmlFiles(root, directory).Count();

    /// <summary>
    ///     Each item, plus one for every variant in each group it names, plus one for each variant
    ///     it names singly.
    /// </summary>
    public static int Items(string root)
    {
        var groupSizes = XmlFiles(root, "variantgroups").Select(XDocument.Load).ToDictionary(
            keySelector: group => (string) group.Root!.Element(Ns + "Name")!,
            elementSelector: group => group.Root!.Elements(Ns + "Variant").Count());

        return XmlFiles(root, "items").Sum(selector: file =>
        {
            var variants = XDocument.Load(file).Root!.Element(Ns + "Properties")?.Element(Ns + "Variants");
            if (variants is null) return 1;
            return 1 + variants.Elements(Ns + "Group").Sum(selector: group => groupSizes.GetValueOrDefault(group.Value)) +
                   variants.Elements(Ns + "Name").Count();
        });
    }

    /// <summary>Each creature, plus one for each of its subtypes.</summary>
    public static int Creatures(string root) => XmlFiles(root, "creatures").Sum(selector: file =>
        1 + (XDocument.Load(file).Root!.Element(Ns + "Types")?.Elements(Ns + "Type").Count() ?? 0));

    /// <summary>The variants in every group, which the loader also stores individually.</summary>
    public static int Variants(string root) => XmlFiles(root, "variantgroups")
        .Sum(selector: file => XDocument.Load(file).Root!.Elements(Ns + "Variant").Count());
}
