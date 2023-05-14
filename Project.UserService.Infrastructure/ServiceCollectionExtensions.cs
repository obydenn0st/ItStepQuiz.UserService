using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.UserService.Core.Interfaces.Persistance;
using Project.UserService.Infrastructure.Persistence;
using Project.UserService.Infrastructure.Utils;
using System.Reflection;

namespace Project.UserService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddConfigurations(configuration)
            .AddTransientServices()
            .AddScopedServices(configuration)
            .AddClients()
            .AddMapper();

        return services;
    }


    /// <summary>
    /// inject transient lifecycle services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
    private static IServiceCollection AddConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }

    /// <summary>
    /// inject transient lifecycle services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddTransientServices(this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    /// inject scope lifecycle services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddScopedServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

    private static IServiceCollection AddClients(this IServiceCollection services)
    {
        return services;
    }
    /// <summary>
    /// inject persistence
    /// </summary>
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DataContext>(options =>
        {
            if (builder.Environment.EnvironmentName.Equals(EnvironmentName.Production))
            {
                var decryptedConnectionString = builder.Configuration.GetConnectionString("DatabaseConnection").Decrypt("DefConnStr");

                options.UseSqlServer(decryptedConnectionString,
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            3,
                            TimeSpan.FromSeconds(10),
                            null);
                    });
            }
            else
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            3,
                            TimeSpan.FromSeconds(10),
                            null);
                    });
            }
        });

        builder.Services.AddScoped<IDataContext, DataContext>();

        return builder;
    }
}