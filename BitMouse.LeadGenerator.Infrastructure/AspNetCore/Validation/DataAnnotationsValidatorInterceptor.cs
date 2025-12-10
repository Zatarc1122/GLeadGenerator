using Microsoft.AspNetCore.Mvc.Filters;

namespace BitMouse.LeadGenerator.Infrastructure.AspNetCore.Validation;

public class DataAnnotationsValidatorInterceptor : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            throw new InvalidModelStateException(context.ModelState);
        }

        await next();
    }
}
