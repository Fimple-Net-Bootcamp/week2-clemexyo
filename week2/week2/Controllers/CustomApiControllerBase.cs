﻿using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace week2.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomApiControllerBase : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if(errors.Any(e => e.Type == ErrorType.Unexpected))
        {
            return Problem();
        }

        var firstError = errors[0];
        var statusCode = firstError.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}
