using BookingGuru.Common.Application.Caching;
using BookingGuru.Common.Application.Data;
using BookingGuru.Common.Application.EventBus;
using BookingGuru.Common.Application.Timing;
using BookingGuru.Common.Infrastructure.Authentication;
using BookingGuru.Common.Infrastructure.Authorization;
using BookingGuru.Common.Infrastructure.Caching;
using BookingGuru.Common.Infrastructure.Data;
using BookingGuru.Common.Infrastructure.EventBus;
using BookingGuru.Common.Infrastructure.Outbox;
using BookingGuru.Common.Infrastructure.Repositories;
using BookingGuru.Common.Infrastructure.Serialization;
using BookingGuru.Common.Infrastructure.Timing;
using Dapper;
using MassTransit;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Quartz;
using StackExchange.Redis;

namespace BookingGuru.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string serviceName,
        Action<IRegistrationConfigurator, string>[] moduleConfigureConsumers,
        RabbitMqSettings rabbitMqSettings,
        string databaseConnectionString,
        string redisConnectionString)
    {
        services.AddAuthenticationInternal();

        services.AddAuthorizationInternal();

        services.TryAddSingleton<IClock, Clock>();

        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        services.AddScoped<AuditingInterceptor>();

        services.TryAddScoped<IDbConnectionFactory>(q => new DbConnectionFactory(databaseConnectionString));

        SqlMapper.AddTypeHandler(new GenericArrayHandler<string>());

        services.AddQuartz(configurator =>
        {
            var scheduler = Guid.NewGuid();
            configurator.SchedulerId = $"default-id-{scheduler}";
            configurator.SchedulerName = $"default-name-{scheduler}";
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.AddSingleton(connectionMultiplexer);
            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();

        services.AddMassTransit(configure =>
        {
            string instanceId = serviceName.ToLowerInvariant().Replace('.', '-');
            foreach (Action<IRegistrationConfigurator, string> configureConsumers in moduleConfigureConsumers)
            {
                configureConsumers(configure, instanceId);
            }

            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(rabbitMqSettings.Host), h =>
                {
                    h.Username(rabbitMqSettings.Username);
                    h.Password(rabbitMqSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddRedisInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddQuartzInstrumentation()
                    .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

                tracing.AddOtlpExporter();
            });

        //Enforce UTC for API Requests & Responses
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new UtcDateTimeConverter());
        });

        return services;
    }
}
