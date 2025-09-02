using Weather.App.Bots;
using Weather.App.Models;


namespace Weather.App.Core;


public class WeatherHub
{
    private readonly List<IWeatherBot> _subscribers = new();


    public void Subscribe(IWeatherBot bot) => _subscribers.Add(bot);


    public void Publish(WeatherData data)
    {
        foreach (var bot in _subscribers)
            bot.OnWeather(data);
    }
}