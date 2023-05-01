using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Hybrasyl.Xml.Objects;

public partial class NewPlayer
    {
        public StartMap GetStartMap()
        {
            StartMaps.OrderBy(keySelector: x => Guid.NewGuid()).FirstOrDefault();
            return StartMaps.First();
        }
    }

public partial class ServerConfig
{
    // In case there is nothing defined in XML, we still need some associations for basic
    // functionality
    [XmlIgnoreAttribute] private static Dictionary<byte, (string key, string setting)> Default = new()
    {
        { 6, ("exchange", "Exchange") },
        { 2, ("group", "Allow Grouping") }
    };

    [XmlIgnoreAttribute] public Dictionary<byte, ClientSetting> SettingsNumberIndex { get; set; }

    [XmlIgnoreAttribute] public Dictionary<string, ClientSetting> SettingsKeyIndex { get; set; }

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
}

public partial class Access
{
    private List<string> _privilegedUsers = new();
    private List<string> _reservedNames = new();

    public bool AllPrivileged { get; set; }

    public List<string> PrivilegedUsers
    {
        get
        {
            if (!string.IsNullOrEmpty(Privileged) && _privilegedUsers.Count == 0)
                foreach (var p in Privileged.Trim().Split(' '))
                {
                    _privilegedUsers.Add(p.Trim().ToLower());
                    if (p.Trim().ToLower() == "*")
                        AllPrivileged = true;
                }

            return _privilegedUsers;
        }
    }

    public List<string> ReservedNames
    {
        get
        {
            if (!string.IsNullOrEmpty(Reserved) && _reservedNames.Count == 0)
                foreach (var p in Reserved.Trim().Split(' '))
                    _reservedNames.Add(p.Trim().ToLower());
            return _reservedNames;
        }
    }

    public bool IsPrivileged(string user)
    {
        if (PrivilegedUsers.Count > 0)
            return PrivilegedUsers.Contains(user.ToLower()) || PrivilegedUsers.Contains("*");
        return false;
    }

    public bool IsReservedName(string user)
    {
        if (ReservedNames.Count > 0)
            return ReservedNames.Contains(user.ToLower());
        return false;
    }
}
