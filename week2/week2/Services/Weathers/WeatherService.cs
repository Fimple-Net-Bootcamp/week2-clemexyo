using ErrorOr;
using week2.Models;
using week2.ServiceErrors;

namespace week2.Services.Weathers;

public class WeatherService : IWeatherService
{
    private static readonly Dictionary<Guid, Weather> _weathers = new Dictionary<Guid, Weather>();
    public ErrorOr<Created> CreateWeather(Weather weather)
    {
        _weathers.Add(weather.Id, weather);
        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteWeather(Guid id)
    {
        if (_weathers.ContainsKey(id))
        {
            _weathers.Remove(id);
            return Result.Deleted;
        }
        return Errors.Weather.NotFound;
    }

    public ErrorOr<Weather> GetWeather(Guid id)
    {
       if(_weathers.TryGetValue(id, out var weather))
       { 
            return weather; 
       }

        return Errors.Weather.NotFound;
    }

    public ErrorOr<Updated> UpdateWeather(Weather weather)
    {
        if (_weathers.ContainsKey(weather.Id))
        {
            _weathers[(Guid)weather.Id] = weather;
            return Result.Updated;
        }

        return Errors.Weather.NotFound;
    }

    public ErrorOr<Weather> GetRandomWeather()
    {
        var random = new Random();

        if(_weathers.Count > 0)
        {
            int randomIndex = random.Next(0, _weathers.Count);
            Guid randomGuid = _weathers.Keys.ElementAt(randomIndex);
            return _weathers[randomGuid];
        }
        return Errors.Weather.NotFound;
    }

    public ErrorOr<Dictionary<Guid, Weather>> GetWeathersDescSorted()
    {
        if (_weathers.Count == 0)
        {
            return Errors.Weather.NotFound;
        }
        return _weathers.OrderByDescending(k => k.Value.CreatedAt).ToDictionary(k => k.Key, k => k.Value);
    }

    public ErrorOr<Dictionary<Guid, Weather>> GetWeathersAscSorted()
    {
        if(_weathers.Count == 0)
        {
            return Errors.Weather.NotFound;
        }
        return _weathers.OrderBy(k => k.Value.CreatedAt).ToDictionary(k => k.Key, k => k.Value);
    }
}
