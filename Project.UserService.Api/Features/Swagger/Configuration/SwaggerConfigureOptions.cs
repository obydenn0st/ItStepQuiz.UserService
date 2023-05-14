using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Project.UserService.Api.Features.Swagger.Configuration;

public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (ApiVersionDescription apiVersionDescription in _provider.ApiVersionDescriptions)
        {
            try
            {
                options.SwaggerDoc(apiVersionDescription.GroupName, CreateInfoForApiVersion(apiVersionDescription));
            }
            catch (Exception)
            {
            }
        }
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        OpenApiInfo openApiInfo = new OpenApiInfo
        {
            Title = "Web Api для работы с депозитами",
            Version = GetAssemblyVersion(),
            Description = GetSwaggerDescription()
        };
        if (description.IsDeprecated)
        {
            openApiInfo.Description += " This API version has been deprecated";
        }

        return openApiInfo;
    }

    private static string GetAssemblyVersion()
    {
        return Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "undefined";
    }

    private string GetSwaggerDescription()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "SwaggerDescription.xml");
        try
        {
            return File.ReadAllText(path);
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}
