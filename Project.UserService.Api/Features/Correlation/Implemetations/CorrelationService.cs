using Project.UserService.Api.Features.Correlation.Abstractions;

namespace Project.UserService.Api.Features.Correlation.Implemetations;

public class CorrelationService : ICorrelationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CorrelationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException("httpContextAccessor");
    }

    public string GetCorrelationHeaderName()
    {
        return "CorrelationId";
    }

    public string GetCorrelationId()
    {
        return _httpContextAccessor.HttpContext!.Request.Headers["CorrelationId"].ToString();
    }
}
