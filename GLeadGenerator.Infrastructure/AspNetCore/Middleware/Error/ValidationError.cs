namespace GLeadGenerator.Infrastructure.AspNetCore.Middleware.Error;

public class ValidationError
{
    public string PropertyName { get; private set; }
    public string Code { get; private set; }
    public string Message { get; private set; }

    public ValidationError(string propertyName, string code, string message)
    {
        PropertyName = propertyName;
        Code = code;
        Message = message;
    }
}

