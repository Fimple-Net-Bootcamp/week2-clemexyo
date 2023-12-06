using ErrorOr;
using week2.ServiceErrors;
using week2.Models;
using week2.Services.Weathers;


namespace week2.Services.Planets
{
    public class PlanetService : IPlanetService
    {
        private static readonly Dictionary<Guid, Planet> _planets = new Dictionary<Guid, Planet>(); //will serve as database table.
        private readonly IWeatherService _weatherService;

        public PlanetService(IWeatherService weatherService) //DI
        {
            _weatherService = weatherService;
        }

        public ErrorOr<Created> CreatePlanet(Planet planet)
        {
            ErrorOr<Weather> randomWeatherResult = _weatherService.GetRandomWeather();
            
            if (randomWeatherResult.IsError)
            {
                return randomWeatherResult.Errors;
            }

            planet.Weather = randomWeatherResult.Value;
            _planets.Add(planet.Id, planet);

            return Result.Created;
        }

        public ErrorOr<Deleted> DeletePlanet(Guid id)
        {
            if (_planets.ContainsKey(id))
            {
                _planets.Remove(id);
                return Result.Deleted;
            }
            return Errors.Planet.NotFound;
        }

        public ErrorOr<Planet> GetPlanet(Guid id)
        {
            if (_planets.TryGetValue(id, out var planet))
            {
                return planet;
            }

            return Errors.Planet.NotFound;
        }

        public ErrorOr<Updated> UpdatePlanet(Planet planet)
        {
            if (_planets.ContainsKey(planet.Id))
            {
                _planets[(Guid)planet.Id] = planet;
                return Result.Updated;
            }

            return Errors.Planet.NotFound;
        }
    }
}
