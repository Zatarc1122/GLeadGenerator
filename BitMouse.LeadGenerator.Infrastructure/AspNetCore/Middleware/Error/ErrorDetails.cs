using System.Net;

namespace GLeadGenerator.Infrastructure.AspNetCore.Middleware.Error;

public class ErrorDetails
{
    public string Title { get; private set; }
    public HttpStatusCode Status { get; private set; }
    public string? RequestId { get; set; }

    public ErrorDetails(string title, HttpStatusCode httpStatusCode)
    {
        Title = title;
        Status = httpStatusCode;
    }
}
