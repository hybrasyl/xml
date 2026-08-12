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
using System.IO;
using System.Text.Json;
using Serilog;

namespace Hybrasyl.XmlTests;

public class Settings
{
    private static Settings? _settings;
    public JsonSettings JsonSettings;

    /// <summary>
    ///     Where the tests read world data from, resolved at construction.
    /// </summary>
    public string WorldDataDirectory { get; }

    private Settings()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("hybrasyl-xml-tests-.log", rollingInterval: RollingInterval.Day)
            .WriteTo.TestCorrelator()
            .CreateLogger();

        var json = File.ReadAllText("xmltest-settings.json");
        JsonSettings = JsonSerializer.Deserialize<JsonSettings>(json) ?? new JsonSettings();
        WorldDataDirectory = ResolveWorldDataDirectory(JsonSettings.WorldDataDirectory);
    }

    /// <summary>
    ///     Resolve the world data directory. Checks, in order:
    ///     1. HYB_WORLD_DIR environment variable (shared with the server's tests)
    ///     2. WorldDataDirectory in xmltest-settings.json, if set
    ///     3. Sibling "ceridwen/xml" relative to the solution root
    ///     4. "ceridwen/xml" inside the solution root (submodule)
    /// </summary>
    private static string ResolveWorldDataDirectory(string configured)
    {
        var envDir = Environment.GetEnvironmentVariable("HYB_WORLD_DIR");
        if (!string.IsNullOrWhiteSpace(envDir) && Directory.Exists(envDir))
            return Path.GetFullPath(envDir);

        if (!string.IsNullOrWhiteSpace(configured) && Directory.Exists(configured))
            return Path.GetFullPath(configured);

        // Walk up from the build output to the solution, so the checkout can live
        // anywhere and the path never differs between Windows and everything else.
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        string? solutionRoot = null;
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir, "XML.sln")))
            {
                solutionRoot = dir;
                break;
            }

            dir = Path.GetDirectoryName(dir);
        }

        if (solutionRoot == null)
            throw new DirectoryNotFoundException(
                "Could not find XML.sln. Set HYB_WORLD_DIR to your ceridwen/xml path.");

        var siblingPath = Path.GetFullPath(Path.Combine(solutionRoot, "..", "ceridwen", "xml"));
        if (Directory.Exists(siblingPath))
            return siblingPath;

        var nestedPath = Path.GetFullPath(Path.Combine(solutionRoot, "ceridwen", "xml"));
        if (Directory.Exists(nestedPath))
            return nestedPath;

        throw new DirectoryNotFoundException(
            "Could not find world data. Tried:\n" +
            $"  - $HYB_WORLD_DIR ({(string.IsNullOrWhiteSpace(envDir) ? "not set" : envDir)})\n" +
            $"  - xmltest-settings.json ({(string.IsNullOrWhiteSpace(configured) ? "not set" : configured)})\n" +
            $"  - {siblingPath}\n" +
            $"  - {nestedPath}");
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
    public string WorldDataDirectory { get; set; } = string.Empty;
}