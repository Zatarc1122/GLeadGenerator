using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BitMouse.LeadGenerator.Infrastructure.AspNetCore.Validation;

public class InvalidModelStateException : Exception
{
    public ModelStateDictionary ModelState { get; }

    public InvalidModelStateException(ModelStateDictionary modelState)
        : base("Request model state is invalid.")
    {
        ModelState = modelState;
    }
}
