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
using System.Xml.Serialization;
using Hybrasyl.Xml.Interfaces;

namespace Hybrasyl.Xml.Objects;

public partial class ServerConfig : ILoadOnStart<ServerConfig>
{
    // In case there is nothing defined in XML, we still need some associations for basic
    // functionality
    [XmlIgnoreAttribute] private static Dictionary<byte, (string key, string setting)> Default = new()
    {
        { 6, ("exchange", "Exchange") },
        { 2, ("group", "Allow Grouping") }
    };

    public override string PrimaryKey => Name;

    [XmlIgnoreAttribute] public Dictionary<byte, ClientSetting> SettingsNumberIndex { get; set; }

    [XmlIgnoreAttribute] public Dictionary<string, ClientSetting> SettingsKeyIndex { get; set; }

    public new static void LoadAll(IWorldDataManager manager, string path = null) =>
        HybrasylEntity<ServerConfig>.LoadAll(manager, path);

    public void InitializeClientSettings()
    {
        SettingsNumberIndex = new Dictionary<byte, ClientSetting>();
        SettingsKeyIndex = new Dictionary<string, ClientSetting>();

        for (byte x = 1; x <= 8; x++)
        {
            var newcs = new ClientSetting
            {
                Default = true,
                Number = x,
                Key = Default.ContainsKey(x) ? Default[x].key : $"setting{x}",
                Value = Default.ContainsKey(x) ? Default[x].setting : $"Setting {x}"
            };

            if (ClientSettings == null) // No settings at all in xml
            {
                SettingsNumberIndex.Add(x, newcs);
                SettingsKeyIndex.Add(newcs.Key, newcs);
            }
            else
            {
                var cs = ClientSettings.FirstOrDefault(predicate: val => val.Number == x);
                if (cs == default(ClientSetting))
                {
                    // No specified setting for this number
                    SettingsNumberIndex.Add(x, newcs);
                    SettingsKeyIndex.Add(newcs.Key, newcs);
                }
                else
                {
                    // We have a defined setting in xml, use it
                    SettingsNumberIndex.Add(x, cs);
                    SettingsKeyIndex.Add(cs.Key.ToLower(), cs);
                }
            }
        }
    }

    public string GetSettingLabel(byte number) => SettingsNumberIndex[number].Value;
    public byte GetSettingNumber(string key) => SettingsKeyIndex[key.ToLower()].Number;

    private Dictionary<byte, string> ClassNames { get; set; } = new();
    private Dictionary<string, byte> ClassIds { get; set; } = new();

    private void GenerateIndex()
    {
        ClassNames.Clear();
        ClassNames[0] = Constants.ClassName0;
        ClassNames[1] = Constants.ClassName1;
        ClassNames[2] = Constants.ClassName2;
        ClassNames[3] = Constants.ClassName3;
        ClassNames[4] = Constants.ClassName4;
        ClassNames[5] = Constants.ClassName5;
        ClassIds[Constants.ClassName0] = 0;
        ClassIds[Constants.ClassName1] = 1;
        ClassIds[Constants.ClassName2] = 2;
        ClassIds[Constants.ClassName3] = 3;
        ClassIds[Constants.ClassName4] = 4;
        ClassIds[Constants.ClassName5] = 5;
    }

    public byte GetClassId(string name)
    {
        if (!ClassNames.Any())
            GenerateIndex();
        return ClassIds.TryGetValue(name, out var id) ? id : (byte) 254;
    }

    public string GetClassName(byte id)
    {
        if (!ClassIds.Any())
            GenerateIndex();
        return ClassNames.TryGetValue(id, out var name) ? name : "Unknown";
    }

}