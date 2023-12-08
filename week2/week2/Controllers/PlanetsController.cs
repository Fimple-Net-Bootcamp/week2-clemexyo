using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using week2.Models;
using week2.Requests.Planet;
using week2.Requests.Weather;
using week2.ServiceErrors;
using week2.Services.Planets;

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

    [HttpGet]
    public IActionResult GetPlanetsSorted([FromQuery] string sorted)
    {
        if (string.Equals(sorted, "desc", StringComparison.OrdinalIgnoreCase))
        {
            ErrorOr<Dictionary<Guid, Planet>> result = _planetService.GetPlanetsDescSorted();
            return result.Match(
                data => Ok(data),
                errors => Problem(errors));
        }
        else if (string.Equals(sorted, "asc", StringComparison.OrdinalIgnoreCase))
        {
            ErrorOr<Dictionary<Guid, Planet>> result = _planetService.GetPlanetsAscSorted();
            return result.Match(
                data => Ok(data),
                errors => Problem(errors));
        }
        else
        {
            // Handle the case where the 'sorted' parameter is not 'asc' or 'desc'
            return BadRequest("Invalid value for 'sorted' parameter. Use 'asc' or 'desc'.");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetPlanet(Guid id)
    {
        ErrorOr<Planet> getPlanetResult = _planetService.GetPlanet(id);
        return getPlanetResult.Match(
            planet => Ok(new PlanetResponse(planet.Id, planet.Name, planet.Weather)),
            errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePlanet(Guid id)
    {
        ErrorOr<Deleted> deletedResult = _planetService.DeletePlanet(id);

        return deletedResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePlanet(Guid id, UpdatePlanetRequest request)
    {
        var planet = new Planet(
            id,
            request.Name,
            request.Weather);

        var updatePlanetResult = _planetService.UpdatePlanet(planet);
        return updatePlanetResult.Match(
            updated => NoContent(),
            errors => Problem(errors));
    }

    [HttpPatch]
    public IActionResult PatchPlanet(PatchPlanetRequest request)
    {
        Guid id = request.id;
        string name = request.Name;

        var patchPlanetResult = _planetService.PatchPlanet(id, name);

        return patchPlanetResult.Match(
           updated => Ok(),
           errors => Problem(errors));
    }
}
