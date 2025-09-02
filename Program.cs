using System.Text.Json;
using Weather.App.Bots;
using Weather.App.Core;
using Weather.App.Models;
using Weather.App.Parsing;


// 1) Load config
var configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
if (!File.Exists(configPath))
{
    Console.WriteLine($"Config file not found: {configPath}");
    return;
}


var json = await File.ReadAllTextAsync(configPath);
var cfg = JsonSerializer.Deserialize<AppConfig>(json, new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
});


if (cfg is null)
{
    Console.WriteLine("Invalid config");
    return;
}


// 2) Prepare bots from config
var hub = new WeatherHub();


var rain = new RainBot(cfg.RainBot.Enabled, cfg.RainBot.HumidityThreshold, cfg.RainBot.Message);
var sun = new SunBot(cfg.SunBot.Enabled, cfg.SunBot.TemperatureThreshold, cfg.SunBot.Message);
var snow = new SnowBot(cfg.SnowBot.Enabled, cfg.SnowBot.TemperatureThreshold, cfg.SnowBot.Message);


hub.Subscribe(rain);
hub.Subscribe(sun);
hub.Subscribe(snow);


// 3) Register parsers (Strategy)
var registry = new ParserRegistry()
.Register(new JsonWeatherParser())
.Register(new XmlWeatherParser());


// 4) Run UI loop
ConsoleUi.Run(registry, hub);