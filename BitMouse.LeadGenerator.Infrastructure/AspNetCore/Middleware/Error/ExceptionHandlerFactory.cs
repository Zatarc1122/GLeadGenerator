namespace GLeadGenerator.Infrastructure.AspNetCore.Middleware.Error;

public class ExceptionHandlerFactory
{
    private readonly IEnumerable<IExceptionHandler> _exceptionHandlers;

    public ExceptionHandlerFactory(IEnumerable<IExceptionHandler> exceptionHandlers)
    {
        _exceptionHandlers = exceptionHandlers;
    }

    public IExceptionHandler Create(Exception e)
    {
        var defaultHandler = _exceptionHandlers.Single(handler => handler is DefaultExceptionHandler);

        var handler = _exceptionHandlers.FirstOrDefault(handler => handler != defaultHandler &&
                                                                   handler.CanHandle(e));

        return handler ?? defaultHandler;
    }
}

