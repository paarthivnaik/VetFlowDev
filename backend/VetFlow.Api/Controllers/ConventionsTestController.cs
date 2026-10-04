using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using VetFlow.Api.Extensions;
using VetFlow.Shared.Results;

namespace VetFlow.Api.Controllers;

[ApiController]
[Route("api/test")]
public sealed class ConventionsTestController : ControllerBase
{
    [HttpGet("success")]
    public ActionResult<string> GetSuccess()
    {
        return Result<string>.Success("ok").ToActionResult();
    }

    [HttpGet("not-found")]
    public ActionResult<string> GetNotFound()
    {
        return Result<string>.Failure(Error.NotFound("Item", 42)).ToActionResult();
    }

    [HttpGet("conflict")]
    public ActionResult<string> GetConflict()
    {
        return Result<string>.Failure(Error.Conflict("Resource already exists.")).ToActionResult();
    }

    [HttpGet("validation-exception")]
    public IActionResult ThrowValidationException()
    {
        var failures = new List<ValidationFailure>
        {
            new("Email", "Email address is invalid."),
            new("Name", "Name is required.")
        };
        throw new ValidationException(failures);
    }

    [HttpGet("unhandled-exception")]
    public IActionResult ThrowUnhandledException()
    {
        throw new InvalidOperationException("Test unhandled exception");
    }
}
