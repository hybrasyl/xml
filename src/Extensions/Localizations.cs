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

using System.Collections.Generic;
using System.Linq;
using Hybrasyl.Xml.Interfaces;

namespace Hybrasyl.Xml.Objects;

public partial class Localization : ILoadOnStart<Localization>
{
    private Dictionary<string, string> _index = new();
    private Dictionary<string, string> _responseIndex = new();
    public override string PrimaryKey => Locale;

    public bool IsEmpty => Common.Count == 0 && Merchant.Count == 0 && MonsterSpeak.Count == 0 && NpcSpeak.Count == 0 &&
                           NpcResponses.Count == 0;

    public new static void LoadAll(IWorldDataManager manager, string path) =>
        HybrasylEntity<Localization>.LoadAll(manager, path);

    public void Reindex()
    {
        _index.Clear();
        // TODO: clean up this xml structure

        foreach (var str in Common.Where(predicate: str =>
                     !string.IsNullOrEmpty(str.Key) && !string.IsNullOrEmpty(str.Value)))
            _index.Add(str.Key, str.Value);
        foreach (var str in Merchant.Where(predicate: str =>
                     !string.IsNullOrEmpty(str.Key) && !string.IsNullOrEmpty(str.Value)))
            _index.Add(str.Key, str.Value);
        foreach (var str in MonsterSpeak.Where(predicate: str =>
                     !string.IsNullOrEmpty(str.Key) && !string.IsNullOrEmpty(str.Value)))
            _index.Add(str.Key, str.Value);
        foreach (var str in NpcSpeak.Where(predicate: str =>
                     !string.IsNullOrEmpty(str.Key) && !string.IsNullOrEmpty(str.Value)))
            _index.Add(str.Key, str.Value);

        foreach (var resp in NpcResponses)
        {
            var key = resp.Call.ToLower().Trim();
            _responseIndex.Add(key, resp.Value);
        }
    }

    /// <summary>
    ///     Fetch a localized string from a given key
    /// </summary>
    /// <param name="key">The key for the string (eg item_equip_too_heavy)</param>
    /// <returns>The localized string, or the key itself, if it is missing</returns>
    public string GetString(string key)
    {
        if (_index.Count == 0 && !IsEmpty)
            Reindex();
        return _index.TryGetValue(key, out var str) ? str : key;
    }

    /// <summary>
    ///     Fetch a localized response from a given call
    /// </summary>
    /// <param name="call">The call (spoken phrase) to which the object will respond</param>
    /// <returns>The spoken response, or null</returns>
    public string GetResponse(string call)
    {
        if (_responseIndex.Count == 0 && !IsEmpty)
            Reindex();
        return _responseIndex.TryGetValue(call.ToLower().Trim(), out var str) ? str : null;
    }
}