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

namespace Hybrasyl.Xml.Manager;

/// <summary>
///     Maps an entity type name to the world-data subdirectory it loads from -
///     <c>Castable</c> to <c>castables</c>, <c>Status</c> to <c>statuses</c>.
/// </summary>
/// <remarks>
///     A wrong answer here is silent: a directory that does not exist yields no files and no
///     errors, so nothing reports a type that loaded nothing. <c>WorldDataDirectoryTests</c>
///     pins the name for every type that loads, against a hand-written table.
/// </remarks>
public static class WorldDataDirectory
{
    /// <summary>The subdirectory name for <paramref name="typeName" />.</summary>
    /// <exception cref="ArgumentException"><paramref name="typeName" /> is null or empty.</exception>
    public static string NameFor(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
            throw new ArgumentException("A type name is required.", nameof(typeName));

        var lower = typeName.ToLowerInvariant();

        // Sibilants take -es; a consonant before a final -y turns it into -ies. Every other
        // entity name in this model is regular.
        if (lower.EndsWith("s") || lower.EndsWith("x") || lower.EndsWith("z")
            || lower.EndsWith("ch") || lower.EndsWith("sh"))
            return lower + "es";

        if (lower.EndsWith("y") && !"aeiou".Contains(lower[^2]))
            return lower[..^1] + "ies";

        return lower + "s";
    }

    /// <summary>The subdirectory name for <typeparamref name="T" />.</summary>
    public static string NameFor<T>() => NameFor(typeof(T).Name);
}
