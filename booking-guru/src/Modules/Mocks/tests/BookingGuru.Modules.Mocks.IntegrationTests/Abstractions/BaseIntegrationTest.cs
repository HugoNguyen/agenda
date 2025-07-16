using Bogus;
using BookingGuru.Common.Infrastructure.Inbox;
using BookingGuru.Common.Infrastructure.Outbox;
using BookingGuru.Modules.Mocks.Domain.FirstFeats;
using BookingGuru.Modules.Mocks.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookingGuru.Modules.Mocks.IntegrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public abstract class BaseIntegrationTest : IDisposable
{
    protected static readonly Faker Faker = new();
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly MocksDbContext DbContext;
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<MocksDbContext>();
        HttpClient = factory.CreateClient();
    }

    protected IServiceProvider Services => _scope.ServiceProvider;

    protected async Task CleanDatabaseAsync()
    {
        await DbContext.Database.ExecuteSqlRawAsync(
            $"""
            DELETE FROM {Schemas.Mocks}.{nameof(InboxMessageConsumer)};
            DELETE FROM {Schemas.Mocks}.{nameof(InboxMessage)};
            DELETE FROM {Schemas.Mocks}.{nameof(OutboxMessageConsumer)};
            DELETE FROM {Schemas.Mocks}.{nameof(OutboxMessage)};
            DELETE FROM {Schemas.Mocks}.{nameof(FirstEntity)};
            """);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
