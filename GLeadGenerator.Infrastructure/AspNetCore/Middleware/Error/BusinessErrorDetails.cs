using System.Net;

namespace GLeadGenerator.Infrastructure.AspNetCore.Middleware.Error;

public class BusinessErrorDetails : ErrorDetails
{
    public string Code { get; private set; }
    public string Error { get; private set; }
    public object? Parameter { get; private set; }
    public BusinessErrorDetails(string title,
        HttpStatusCode httpStatusCode,
        string code,
        string error,
        object? parameter)
        : base(title, httpStatusCode)
    {
        Code = code;
        Error = error;
        Parameter = parameter;
    }
}

