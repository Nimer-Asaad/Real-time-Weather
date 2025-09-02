namespace Weather.App.Models;


public class RainBotConfig
{
    public bool Enabled { get; set; }
    public double HumidityThreshold { get; set; }
    public string Message { get; set; } = "";
}


public class SunBotConfig
{
    public bool Enabled { get; set; }
    public double TemperatureThreshold { get; set; }
    public string Message { get; set; } = "";
}


public class SnowBotConfig
{
    public bool Enabled { get; set; }
    public double TemperatureThreshold { get; set; }
    public string Message { get; set; } = "";
}


public class AppConfig
{
    public RainBotConfig RainBot { get; set; } = new();
    public SunBotConfig SunBot { get; set; } = new();
    public SnowBotConfig SnowBot { get; set; } = new();
}