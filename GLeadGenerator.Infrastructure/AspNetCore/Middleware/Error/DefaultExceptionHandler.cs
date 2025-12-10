using System.Net;

namespace GLeadGenerator.Infrastructure.AspNetCore.Middleware.Error;

public class DefaultExceptionHandler : IExceptionHandler
{
    public bool CanHandle(Exception e)
    {
        return true;
    }

    public ErrorDetails Handle(Exception e)
    {
        var errorDetails = new ErrorDetails("An error occured", HttpStatusCode.InternalServerError);

        return errorDetails;
    }
}

