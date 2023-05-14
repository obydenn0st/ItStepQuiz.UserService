using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Project.UserService.Api;

/// <summary>
/// Extension settings up the versioning 
/// </summary>
public static class VersioningExtensions
{
    /// <summary>
    /// Method adds versioning
    /// Provides microsoft.versioning
    /// Adds api versioning
    /// Setup report api versions
    /// Adds versioned api explorer
    /// Setup group name format "v.VVV"
    /// Setup substitute api version in url
    /// </summary>
    /// <param name = "services"> </param>
    /// <returns> </returns>
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddVersionedApiExplorer(options => { options.ApiVersionParameterSource = new UrlSegmentApiVersionReader(); });

        return services;
    }
}