using Project.UserService.Api.Common.Extensions;
using Project.UserService.Core.Common.Exceptions;
using Project.UserService.Core.Common.Exceptions.HttpExceptions;
using System.Net;

namespace Project.UserService.Api.Features.Middlewares;

/// <summary>
/// Middleware handles every exceptions
/// </summary>
public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    /// <summary>
    /// Create <see cref="ExceptionMiddleware"/> instance
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Run middleware
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (RequestValidationException ex)
        {
            var problem = ex.GenerateProblemDetails(context, "Невалидные параметры запроса", HttpStatusCode.BadRequest);
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (NotFoundException ex)
        {
            var problem = ex.GenerateProblemDetails(context, "Объект не найден", HttpStatusCode.NotFound);
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (RetryLimitException ex)
        {
            _logger.LogError(ex, "Достигнут лимит запросов в сервис");
            var problem = ex.GenerateProblemDetails(context, "Сервис временно недоступен", HttpStatusCode.ServiceUnavailable);
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обработке запроса");
            var problem = ex.GenerateProblemDetails(context, "Ошибка при обработке запроса", HttpStatusCode.InternalServerError);
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}