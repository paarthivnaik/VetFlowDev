using Microsoft.AspNetCore.Mvc;
using VetFlow.Shared.Results;

namespace VetFlow.Api.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }

        return CreateProblemDetailsResult(result.Error!);
    }

    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        return CreateProblemDetailsResult(result.Error!);
    }

    private static ObjectResult CreateProblemDetailsResult(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = error.Type.ToString(),
            Detail = error.Description,
            Extensions = { ["errorCode"] = error.Code }
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = statusCode
        };
    }
}
