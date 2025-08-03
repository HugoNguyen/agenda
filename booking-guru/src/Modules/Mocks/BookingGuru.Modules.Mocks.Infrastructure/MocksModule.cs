using BookingGuru.Common.Application.EventBus;
using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Application.Repositories;
using BookingGuru.Common.Infrastructure.Outbox;
using BookingGuru.Common.Infrastructure.Repositories;
using BookingGuru.Common.Presentation.Endpoints;
using BookingGuru.Modules.Mock2s.IntegrationEvents;
using BookingGuru.Modules.Mocks.Application.Abstractions.Data;
using BookingGuru.Modules.Mocks.Infrastructure.Database;
using BookingGuru.Modules.Mocks.Infrastructure.Inbox;
using BookingGuru.Modules.Mocks.Infrastructure.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BookingGuru.Modules.Mocks.Infrastructure;

public static class MocksModule
{
    public static IServiceCollection AddMocksModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddDomainEventHandlers();

        services.AddIntegrationEventHandlers();

        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator, string instanceId)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<PublishCreatedIntegrationEvent>>()
            .Endpoint(c => c.InstanceId = instanceId);
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MocksDbContext>((sp, options) =>
            options
                .UseSqlServer(
                    configuration.GetConnectionString("Database"),
                    options => options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Mocks))
                .UseCamelCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .AddInterceptors(sp.GetRequiredService<AuditingInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MocksDbContext>());

        services.AddRepositories();

        services.Configure<OutboxOptions>(configuration.GetSection("Mocks:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("Mocks:Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob>();
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToArray();

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddGenericRepositoriesFromAssembly<MocksDbContext>();

        services.Scan(scan => scan
            .FromAssemblyOf<MocksDbContext>()
            .AddClasses(c => c.AssignableTo<IDecoratedRepository>())
            .As(type => type
                .GetInterfaces()
                .Where(i => i != typeof(IDecoratedRepository)))
            .WithTransientLifetime()
        );
    }
}