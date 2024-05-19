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

namespace Hybrasyl.XmlTests;

public class Settings
{
    private static Settings _settings;
    public JsonSettings JsonSettings;

    private Settings()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("hybrasyl-xml-tests-.log", rollingInterval: RollingInterval.Day)
            .WriteTo.TestCorrelator()
            .CreateLogger();

        var json = File.ReadAllText("xmltest-settings.json");
        JsonSettings = JsonConvert.DeserializeObject<JsonSettings>(json);
    }

    private static object _lock { get; } = new();

    public static Settings XmlTests
    {
        get
        {
            lock (_lock)
            {
                _settings ??= new Settings();
                return _settings;
            }
        }
    }
}

public class JsonSettings
{
    public string WorldDataDirectory { get; set; }
}