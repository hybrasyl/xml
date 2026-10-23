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

using System.Linq;
using Hybrasyl.Xml.Interfaces;

namespace Hybrasyl.Xml.Objects;

public partial class ElementTable : ILoadOnStart<ElementTable>
{
    public override string PrimaryKey => Name;

    public new static void LoadAll(IWorldDataManager manager, string path = null) =>
        HybrasylEntity<ElementTable>.LoadAll(manager, path);

    /// <summary>
    ///     Get the damage multiplier for an attack of element <paramref name="source" /> against a defender of
    ///     element <paramref name="target" />.
    /// </summary>
    /// <remarks>
    ///     A table declares only the pairs it needs. A pair with no Source row, no Target entry, or a Target entry
    ///     without a Multiplier uses <see cref="DefaultMultiplier" />.
    /// </remarks>
    public double GetMultiplier(ElementType source, ElementType target)
    {
        var entry = Source.FirstOrDefault(s => s.Element == source)?.Target
            .FirstOrDefault(t => t.Element == target);
        return entry is { MultiplierSpecified: true } ? entry.Multiplier : DefaultMultiplier;
    }
}