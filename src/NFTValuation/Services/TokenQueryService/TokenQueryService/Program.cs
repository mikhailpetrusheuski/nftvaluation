using System;
using System.Reflection;
using Core.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nethereum.Web3;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using TokenQueryService.Middlewares;
using TokenQueryService.OperationFilters;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Host.UseSerilog((context, _, loggerConfiguration) =>
        loggerConfiguration
            .WriteTo.Async(a => a.Console(theme: AnsiConsoleTheme.Code))
            .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .ReadFrom
            .Configuration(context.Configuration));

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(
        c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Token Query Service", Version = "v1" });
            c.OperationFilter<ApiKeyOperationFilter>();
        }
    );

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

    builder.Services.AddLoggingBehavior();
    builder.Services.AddValidatorBehavior();
    
    //ToDo: Move to config Key
    builder.Services.AddSingleton(sp => new Web3("https://mainnet.infura.io/v3/{KEY}"));

    
    var app = builder.Build();

    app.UseSerilogRequestLogging();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Token Query Service v1"));
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthorization();

    app.UseMiddleware<ApiKeyMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

#pragma warning disable CA1050
public partial class Program
#pragma warning restore CA1050
{
    
}
