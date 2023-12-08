using ErrorOr;
using week2.Models;

namespace week2.Services.Planets;

public interface IPlanetService
{
    ErrorOr<Created> CreatePlanet(Planet planet);
    ErrorOr<Deleted> DeletePlanet(Guid id);
    ErrorOr<Planet> GetPlanet(Guid id);
    ErrorOr<Updated> UpdatePlanet(Planet planet);

    ErrorOr<Dictionary<Guid, Planet>> GetPlanetsDescSorted();
    ErrorOr<Dictionary<Guid, Planet>> GetPlanetsAscSorted();
}
