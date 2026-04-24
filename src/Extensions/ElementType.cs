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

namespace Hybrasyl.Xml.Objects;

public static class ElementTypeExtensions
{
    private static readonly ElementType[] TemuairSet =
    {
        ElementType.Fire, ElementType.Water, ElementType.Wind, ElementType.Earth
    };

    private static readonly ElementType[] ClassicSet =
    {
        ElementType.Fire, ElementType.Water, ElementType.Wind, ElementType.Earth,
        ElementType.Light, ElementType.Dark
    };

    private static readonly ElementType[] ExpandedSet =
    {
        ElementType.Arcane, ElementType.Void, ElementType.Life, ElementType.Metal
    };

    private static readonly ElementType[] AllSet =
    {
        ElementType.Force, ElementType.Fire, ElementType.Water, ElementType.Wind,
        ElementType.Earth, ElementType.Flesh, ElementType.Spirit, ElementType.Nature,
        ElementType.Metal, ElementType.Time, ElementType.Stasis, ElementType.Light,
        ElementType.Dark, ElementType.Life, ElementType.Arcane, ElementType.Void
    };

    public static ElementType Resolve(this ElementType element) => element switch
    {
        ElementType.RandomTemuair => TemuairSet.PickRandom(),
        ElementType.RandomClassic => ClassicSet.PickRandom(),
        ElementType.RandomExpanded => ExpandedSet.PickRandom(),
        ElementType.RandomAll => AllSet.PickRandom(),
        _ => element
    };
}
