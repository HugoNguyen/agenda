using BookingGuru.Common.Application.EventBus;
using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Infrastructure.Outbox;
using BookingGuru.Common.Infrastructure.Repositories;
using BookingGuru.Common.Presentation.Endpoints;
using BookingGuru.Modules.Mock2s.Application.Abstractions.Data;
using BookingGuru.Modules.Mock2s.Infrastructure.Database;
using BookingGuru.Modules.Mock2s.Infrastructure.Inbox;
using BookingGuru.Modules.Mock2s.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;

namespace BookingGuru.Modules.Mock2s.Infrastructure;

public static class Mock2sModule
{
    public static IServiceCollection AddMock2sModule(
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

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Mock2sDbContext>((sp, options) =>
            options
                .UseSqlServer(
                    configuration.GetConnectionString("Database"),
                    options => options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Mock2s))
                .UseCamelCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .AddInterceptors(sp.GetRequiredService<AuditingInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<Mock2sDbContext>());

        services.AddGenericRepositoriesFromAssembly<Mock2sDbContext>();

        services.Configure<OutboxOptions>(configuration.GetSection("Mock2s:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("Mock2s:Inbox"));

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
}