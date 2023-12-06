using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using week2.Models;
using week2.Requests.Planet;
using week2.Requests.Weather;
using week2.Services.Planets;
using week2.Services.Weathers;

namespace week2.Controllers;


public class PlanetsController : CustomApiControllerBase
{
    private readonly IPlanetService _planetService;

    public PlanetsController(IPlanetService planetService) //DI
    {
        _planetService = planetService;
    }

    [HttpPost]
    public IActionResult CreatePlanet(CreatePlanetRequest request)
    {
        var planet = new Planet(Guid.NewGuid(), request.Name);
        ErrorOr<Created> createPlanetResult = _planetService.CreatePlanet(planet);

        if (createPlanetResult.IsError)
        {
            return Problem(createPlanetResult.Errors);
        }

        var response = new PlanetResponse(
            planet.Id,
            planet.Name,
            planet.Weather);

        return CreatedAtAction(actionName: nameof(GetPlanet),
                           routeValues: new { id = planet.Id },
                           value: response);
    }

    [HttpGet("{id}")]
    public IActionResult GetPlanet(Guid id)
    {
        ErrorOr<Planet> getPlanetResult = _planetService.GetPlanet(id);
        return getPlanetResult.Match(
            planet => Ok(new PlanetResponse(planet.Id, planet.Name, planet.Weather)),
            errors => Problem(errors));
    }
}
