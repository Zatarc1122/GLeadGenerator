using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace GLeadGenerator.Infrastructure.AspNetCore.Middleware.Error;

public class ErrorMiddleware
{
    private readonly ILogger<ErrorMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly ExceptionHandlerFactory _exceptionHandlerFactory;

    public ErrorMiddleware(ILogger<ErrorMiddleware> logger,
        RequestDelegate next,
        ExceptionHandlerFactory exceptionHandlerFactory)
    {
        _logger = logger;
        _next = next;
        _exceptionHandlerFactory = exceptionHandlerFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var request = context.Request;
            var url = $"{request.Path}{request.QueryString}";
            var body = await GetBodyAsync(context);

            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["RequestId"] = context.TraceIdentifier,
                ["RequestUrl"] = url,
                ["RequestPayload"] = body
            }))
            {
                _logger.LogError(e, "{ErrorMessage}", e.Message);
            }

            var exceptionHandler = _exceptionHandlerFactory.Create(e);
            var errorDetails = exceptionHandler.Handle(e);
            errorDetails.RequestId = context.TraceIdentifier;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var errorDetailsJson = JsonSerializer.Serialize(errorDetails, errorDetails.GetType(), options);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)errorDetails.Status;

            await response.WriteAsync(errorDetailsJson);
        }
    }

    private static async Task<string> GetBodyAsync(HttpContext httpContext)
    {
        var request = httpContext.Request;
        var body = string.Empty;

        request.EnableBuffering();

        using (var reader = new StreamReader(
            request.Body,
            encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            leaveOpen: true))
        {
            body = await reader.ReadToEndAsync();

            request.Body.Position = 0;
        }

        return body;
    }
}

