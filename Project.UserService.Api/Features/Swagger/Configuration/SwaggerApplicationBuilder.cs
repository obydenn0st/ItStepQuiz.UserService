using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Project.UserService.Api.Features.Swagger.Configuration;

public static class SwaggerApplicationBuilder
{
    public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
    {
        IApiVersionDescriptionProvider apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(delegate (SwaggerUIOptions options)
        {
            foreach (ApiVersionDescription apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(apiVersionDescription.GroupName + "/swagger.json", apiVersionDescription.GroupName.ToUpperInvariant());
                options.RoutePrefix = "swagger";
            }
        });
        return app;
    }

}
