using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Project.UserService.Api.Features.Swagger.Configuration;

public static class SwaggerServiceExtensions
{
    public static WebApplicationBuilder ConfigureWebApiSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
        builder.Services.AddSwaggerGen(delegate (SwaggerGenOptions options)
        {
            string path = Assembly.GetEntryAssembly()?.GetName().Name + ".xml";
            string filePath = Path.Combine(AppContext.BaseDirectory, path);
            options.IncludeXmlComments(filePath);
        });
        builder.Services.AddSwaggerGenNewtonsoftSupport();
        return builder;
    }
}
