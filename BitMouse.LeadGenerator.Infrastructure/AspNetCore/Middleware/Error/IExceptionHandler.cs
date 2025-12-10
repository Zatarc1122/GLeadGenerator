namespace GLeadGenerator.Infrastructure.AspNetCore.Middleware.Error;

public interface IExceptionHandler
{
    public bool CanHandle(Exception e);
    public ErrorDetails Handle(Exception e);
}

