using Microsoft.AspNetCore.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Project.UserService.Api;
using Project.UserService.Api.Features.AutoMapper;
using Project.UserService.Api.Features.Correlation;
using Project.UserService.Api.Features.Cryptor;
using Project.UserService.Api.Features.Logging;
using Project.UserService.Api.Features.Middlewares;
using Project.UserService.Api.Features.Swagger.Configuration;
using Project.UserService.Core;
using Project.UserService.Infrastructure;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Services.ConfigureLogger(builder.Configuration);
    builder.Services.ConfigureCorrelation();
    Log.Logger.Information("Переменная среды для запуска проекта ({Environment})...", builder.Environment.EnvironmentName);
    Log.Logger.Information("Конфигурация проекта ({Application})...", builder.Environment.ApplicationName);

    builder.ConfigureWebApiSwagger();
    builder.Services.AddVersioning();
    builder.Services.AddConfiguredAutoMapper();

    builder.Services.AddControllers()
        .AddNewtonsoftJson(x =>
        {
            x.SerializerSettings.Converters.Add(new StringEnumConverter());
            x.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
            x.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
            x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.AddPersistence();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHealthChecks().AddSqlServer(
    builder.Environment.EnvironmentName.Equals(Project.UserService.Core.Common.Constants.EnvironmentName.Production, StringComparison.Ordinal) ?
    builder.Configuration.GetConnectionString("DatabaseConnection").Decrypt("DefConnStr") : builder.Configuration.GetConnectionString("DatabaseConnection"));

    var defaultCulture = new RequestCulture("ru-RU", "ru-RU");
    var supportedCultures = new List<CultureInfo> { new("ru-RU"), new("kk-KZ"), new("en-US"), };

    var localizationOptions =
        new RequestLocalizationOptions
        {
            DefaultRequestCulture = defaultCulture,
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures,
            ApplyCurrentCultureToResponseHeaders = true,
        };

    var app = builder.Build();
    app.MapHealthChecks("/health");
    app.UseConfiguredSwagger();
    app.UseApiVersioning();
    app.UseHttpsRedirection();
    app.UseRequestLocalization(localizationOptions);
    app.UseMiddleware<CorrelationMiddleware>();
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseMiddleware<LogMiddleware>();
    app.MapControllers();
    Log.Logger.Information("Запуск проекта ({Application})...", builder.Environment.ApplicationName);
    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Error(ex, "Ошибка при запуске проекта ({Application})...", builder.Environment.ApplicationName);
}
finally
{
    await Log.CloseAndFlushAsync();
}