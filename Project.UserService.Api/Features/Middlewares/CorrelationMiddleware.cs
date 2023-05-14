namespace Project.UserService.Api.Features.Middlewares;

public class CorrelationMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (string.IsNullOrWhiteSpace(context.Request.Headers["CorrelationId"].ToString()))
        {
            context.Request.Headers["CorrelationId"] = Guid.NewGuid().ToString();
        }

        await _next(context);
    }
}
