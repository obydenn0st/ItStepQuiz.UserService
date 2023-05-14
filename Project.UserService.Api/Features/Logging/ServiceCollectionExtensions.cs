using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Project.UserService.Api.Features.Logging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        services.AddLogging(delegate (ILoggingBuilder x)
        {
            x.ClearProviders();
            x.AddSerilog(Log.Logger);
        });
        return services;
    }
}
