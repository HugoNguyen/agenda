using BookingGuru.Common.Application.Data;
using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Domain;
using BookingGuru.Common.Infrastructure.Outbox;
using BookingGuru.Modules.Mocks.Infrastructure.Database;
using Dapper;
using System.Data.Common;

namespace BookingGuru.Modules.Mocks.Infrastructure.Outbox;

internal sealed class IdempotentDomainEventHandler<TDomainEvent>(
    IDomainEventHandler<TDomainEvent> decorated,
    IDbConnectionFactory dbConnectionFactory)
    : DomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public override async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var outboxMessageConsumer = new OutboxMessageConsumer(domainEvent.Id, decorated.GetType().Name);

        if (await OutboxConsumerExistsAsync(connection, outboxMessageConsumer))
        {
            return;
        }

        await decorated.Handle(domainEvent, cancellationToken);

        await InsertOutboxConsumerAsync(connection, outboxMessageConsumer);
    }

    private static async Task<bool> OutboxConsumerExistsAsync(
        DbConnection dbConnection,
        OutboxMessageConsumer outboxMessageConsumer)
    {
        const string sql =
            $"""
            SELECT COUNT(1)
                FROM {Schemas.Mocks}.{nameof(OutboxMessageConsumer)}
                WHERE outboxMessageId = @OutboxMessageId AND
                      name = @Name
            """;

        return await dbConnection.ExecuteScalarAsync<bool>(sql, outboxMessageConsumer);
    }

    private static async Task InsertOutboxConsumerAsync(
        DbConnection dbConnection,
        OutboxMessageConsumer outboxMessageConsumer)
    {
        const string sql =
            $"""
            INSERT {Schemas.Mocks}.{nameof(OutboxMessageConsumer)}(outboxMessageId, name)
            VALUES (@OutboxMessageId, @Name)
            """;

        await dbConnection.ExecuteAsync(sql, outboxMessageConsumer);
    }
}
