using Project.UserService.Api.Features.Correlation.Abstractions;
using Project.UserService.Api.Features.Correlation.Implemetations;

namespace Project.UserService.Api.Features.Correlation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureCorrelation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICorrelationService, CorrelationService>();
        return services;
    }
}
