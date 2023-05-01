using Newtonsoft.Json;
using Serilog;

namespace Hybrasyl.XmlTests;

public class Settings
{
    private static object _lock { get; set; } = new();

    private static Settings _settings;
    public JsonSettings JsonSettings;

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

    private Settings()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("hybrasyl-xml-tests-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var json = File.ReadAllText("xmltest-settings.json");
        JsonSettings = JsonConvert.DeserializeObject<JsonSettings>(json);
    }

}

public class JsonSettings
{
    public string WorldDataDirectory { get; set; }
}
