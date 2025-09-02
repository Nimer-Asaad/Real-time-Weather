using Weather.App.Models;


namespace Weather.App.Bots;


public class RainBot : IWeatherBot
{
    private readonly double _threshold;
    private readonly string _message;
    public string Name => nameof(RainBot);
    public bool IsEnabled { get; }


    public RainBot(bool enabled, double humidityThreshold, string message)
    {
        IsEnabled = enabled;
        _threshold = humidityThreshold;
        _message = message;
    }


    public void OnWeather(WeatherData data)
    {
        if (!IsEnabled) return;
        if (data.Humidity > _threshold)
        {
            Console.WriteLine("RainBot activated!");
            Console.WriteLine($"RainBot: \"{_message}\"");
        }
    }
}