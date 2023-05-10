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