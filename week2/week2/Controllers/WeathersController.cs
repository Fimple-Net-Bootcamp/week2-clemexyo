using Microsoft.AspNetCore.Mvc;
using week2.Requests.Weather;

namespace week2.Controllers;

[ApiController]
[Route("[controller]")]
public class WeathersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateWeather(CreateWeatherRequest request)
    {
        return Ok(request);
    }

    [HttpGet("{id}")]
    public IActionResult GetWeather(Guid id)
    {
        return Ok(id);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateWeather(Guid id, UpdateWeatherRequest request)
    {
        return Ok(request);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteWeather(Guid id)
    {
        return Ok(id);
    }
}
