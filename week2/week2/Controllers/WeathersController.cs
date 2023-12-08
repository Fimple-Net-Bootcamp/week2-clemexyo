using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using week2.Models;
using week2.Requests.Weather;
using week2.ServiceErrors;
using week2.Services.Weathers;

namespace week2.Controllers;

public class WeathersController : CustomApiControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeathersController(IWeatherService weatherService) //dependency injection
    {
        _weatherService = weatherService;
    }

    [HttpPost]
    public IActionResult CreateWeather(CreateWeatherRequest request)
    {
        var weather = new Weather(
            Guid.NewGuid(),
            request.Name,
            request.Description);

        ErrorOr<Created> createWeatherResult = _weatherService.CreateWeather(weather);

        if(createWeatherResult.IsError)
        {
            return Problem(createWeatherResult.Errors);
        }

        var response = new WeatherResponse(
            weather.Id,
            weather.Name,
            weather.Description);

        return CreatedAtAction(actionName: nameof(GetWeather),
                               routeValues: new { id = weather.Id },
                               value: response);
    }

    [HttpGet("{id}")]
    public IActionResult GetWeather(Guid id)
    {
        ErrorOr<Weather> getWeatherResult = _weatherService.GetWeather(id);
        return getWeatherResult.Match(
            weather => Ok(new WeatherResponse(weather.Id, weather.Name, weather.Description)),
            errors => Problem(errors)
            );
    }

    [HttpPut("{id}")]
    public IActionResult UpdateWeather(Guid id, UpdateWeatherRequest request)
    {
        var weather = new Weather(
            id,
            request.Name,
            request.Description);

        var updateWeatherResult = _weatherService.UpdateWeather(weather);
        return updateWeatherResult.Match(
            updated => NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteWeather(Guid id)
    {
        ErrorOr<Deleted> deletedResult = _weatherService.DeleteWeather(id);

        return deletedResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }

    [HttpGet]
    public IActionResult GetWeathersSorted([FromQuery] string sorted)
    {
        if (string.Equals(sorted, "desc", StringComparison.OrdinalIgnoreCase))
        {
            ErrorOr<Dictionary<Guid, Weather>> result = _weatherService.GetWeathersDescSorted();
            return result.Match(
                data => Ok(data),
                errors => Problem(errors));
        }
        else if (string.Equals(sorted, "asc", StringComparison.OrdinalIgnoreCase))
        {
            ErrorOr<Dictionary<Guid, Weather>> result = _weatherService.GetWeathersAscSorted();
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
}
