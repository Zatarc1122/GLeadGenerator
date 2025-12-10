namespace GLeadGenerator.Infrastructure.Domain;

public class BusinessException : Exception
{
    public string Code { get; private set; }
    public object? Parameter { get; private set; }
    public BusinessException(string message, string code, object? parameter = null) : base(message)
    {
        Code = code;
        Parameter = parameter;
    }
}

