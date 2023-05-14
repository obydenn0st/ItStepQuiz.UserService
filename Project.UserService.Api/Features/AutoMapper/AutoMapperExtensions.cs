using System.Reflection;

namespace Project.UserService.Api.Features.AutoMapper;

/// <summary>
/// 
/// </summary>
public static class AutoMapperExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddConfiguredAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}