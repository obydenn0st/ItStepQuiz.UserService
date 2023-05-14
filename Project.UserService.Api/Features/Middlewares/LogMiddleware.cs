using System.Diagnostics;

namespace Project.UserService.Api.Features.Middlewares;

public class LogMiddleware
{
    private readonly ILogger<RequestDelegate> _logger;

    private readonly RequestDelegate _next;

    public LogMiddleware(RequestDelegate next, ILogger<RequestDelegate> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Dictionary<string, object> state = new Dictionary<string, object> {
            {
                "CorrelationId",
                context.Request.Headers["CorrelationId"].ToString()
            } };
        using (_logger.BeginScope(state))
        {
            try
            {
                await _next(context);
            }
            finally
            {
                sw.Stop();
                _logger.LogInformation("Запрос [{HttpMethod} {HttpPath}] был обработан в течение ({ElapsedTime} мс)", context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds);
            }
        }
    }
}