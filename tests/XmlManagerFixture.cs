using Hybrasyl.Xml.Manager;
using System.Diagnostics;

namespace Hybrasyl.XmlTests;

public class XmlManagerFixture
{

    public XmlDataManager AsyncManager { get; set; }
    public XmlDataManager SyncManager { get; set; }

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
}

