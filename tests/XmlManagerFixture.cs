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

using System.Diagnostics;
using Hybrasyl.Xml.Manager;
using Serilog;

namespace Hybrasyl.XmlTests;

public class XmlManagerFixture
{
    public XmlManagerFixture()
    {
        SyncManager = new XmlDataManager(Settings.XmlTests.JsonSettings.WorldDataDirectory);
        var syncWatch = new Stopwatch();
        syncWatch.Start();
        SyncManager.LoadData();
        syncWatch.Stop();

        Log.Information($"LoadData execution time: {syncWatch.ElapsedMilliseconds}ms");

        AsyncManager = new XmlDataManager(Settings.XmlTests.JsonSettings.WorldDataDirectory);
        var asyncWatch = new Stopwatch();
        asyncWatch.Start();
        var task = Task.Run(AsyncManager.LoadDataAsync);
        while (true)
        {
            if (AsyncManager.IsReady)
                break;
            Task.Delay(125).Wait();
        }

        asyncWatch.Stop();
        Log.Information($"LoadDataAsync execution time: {asyncWatch.ElapsedMilliseconds}ms");
    }

    public XmlDataManager AsyncManager { get; set; }
    public XmlDataManager SyncManager { get; set; }
}