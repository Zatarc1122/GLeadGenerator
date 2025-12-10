using BitMouse.LeadGenerator.Infrastructure.AspNetCore.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BitMouse.LeadGenerator.Infrastructure.AspNetCore.Middleware.Error;

public class DataAnnotationsValidationExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception e)
    {
        return e is InvalidModelStateException;
    }

    public ErrorDetails Handle(Exception e)
    {
        var invalidModelStateException = (InvalidModelStateException)e;
        var errors = invalidModelStateException.ModelState
            .Where(entry => entry.Value?.Errors.Count > 0)
            .SelectMany(CreateValidationErrors)
            .ToList();

        return new ValidationErrorDetails(
            title: "One or more validation errors occurred.",
            httpStatusCode: HttpStatusCode.BadRequest,
            errors: errors);
    }

    private static IEnumerable<ValidationError> CreateValidationErrors(KeyValuePair<string, ModelStateEntry?> entry)
    {
        if (entry.Value is null)
        {
            yield break;
        }

        foreach (var error in entry.Value.Errors)
        {
            yield return new ValidationError(
                propertyName: entry.Key,
                code: "Invalid",
                message: string.IsNullOrWhiteSpace(error.ErrorMessage)
                    ? "The value supplied is invalid."
                    : error.ErrorMessage);
        }
    }
}
