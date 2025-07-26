using BookingGuru.Api.Extensions;
using BookingGuru.Api.Middleware;
using BookingGuru.Api.OpenTelemetry;
using BookingGuru.Common.Application;
using BookingGuru.Common.Infrastructure;
using BookingGuru.Common.Infrastructure.Configuration;
using BookingGuru.Common.Infrastructure.EventBus;
using BookingGuru.Common.Infrastructure.Timing;
using BookingGuru.Common.Presentation.Endpoints;
using BookingGuru.Modules.Mock2s.Infrastructure;
using BookingGuru.Modules.Mocks.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using Serilog;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDocumentation();

Assembly[] moduleApplicationAssemblies = [
    BookingGuru.Modules.Mocks.Application.AssemblyReference.Assembly,
    BookingGuru.Modules.Mock2s.Application.AssemblyReference.Assembly
];

builder.Services.AddApplication(moduleApplicationAssemblies);

// Clock
builder.Services.Configure<ClockOptions>(options =>
{
    options.Kind = DateTimeKind.Utc;
});

// Read connections
string databaseConnectionString = builder.Configuration.GetConnectionStringOrThrow("Database");
string redisConnectionString = builder.Configuration.GetConnectionStringOrThrow("Cache");
string rabbitConnectionSTring = builder.Configuration.GetConnectionStringOrThrow("Queue");
ConnectionFactory connectionFactory = new ConnectionFactory()
{
    Uri = new Uri(builder.Configuration.GetConnectionStringOrThrow("Queue")),
};
var rabbitMqSettings = new RabbitMqSettings(connectionFactory.Endpoint.ToString(), connectionFactory.UserName, connectionFactory.Password);

builder.Services.AddInfrastructure(
    serviceName: DiagnosticsConfig.ServiceName,
    moduleConfigureConsumers: [
        MocksModule.ConfigureConsumers],
    rabbitMqSettings: rabbitMqSettings,
    databaseConnectionString: databaseConnectionString,
    redisConnectionString: redisConnectionString);

Uri keyCloakHealthUrl = builder.Configuration.GetKeyCloakHealthUrl();

builder.Services.AddHealthChecks()
    .AddSqlServer(databaseConnectionString)
    .AddRedis(redisConnectionString)
    //TODO: should share IConnect
    .AddRabbitMQ(factory: (service) =>
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(rabbitMqSettings.Host),
            UserName = rabbitMqSettings.Username,
            Password = rabbitMqSettings.Password
        };
        return factory.CreateConnectionAsync().GetAwaiter().GetResult();
    })
    .AddKeyCloak(keyCloakHealthUrl);

builder.Configuration.AddModuleConfiguration(["mocks", "mock2s"]);

builder.Services.AddMocksModule(builder.Configuration);

builder.Services.AddMock2sModule(builder.Configuration);


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseLogContextTraceLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();


#pragma warning disable S6966 // Awaitable method should be used
app.Run();
#pragma warning restore S6966 // Awaitable method should be used

[System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "<Pending>")]
public partial class Program;