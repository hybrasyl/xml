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

using System;
using System.Text.RegularExpressions;

namespace Hybrasyl.Xml.Extensions;

public static class StringExtensions
{
    public static bool Contains(this string source, string toCheck, StringComparison comparision) =>
        source?.IndexOf(toCheck, comparision) >= 0;

    public static string Capitalize(this string s)
    {
        if (string.IsNullOrEmpty(s))
            return string.Empty;

        var a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

    public static string Normalize(string key) => Regex.Replace(key.ToLower(), @"\s+", "");
}