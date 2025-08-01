﻿using System.Data.Common;
using Dapper;
using BookingGuru.Common.Application.Data;
using BookingGuru.Common.Application.EventBus;
using BookingGuru.Common.Infrastructure.Inbox;
using BookingGuru.Modules.Mock2s.Infrastructure.Database;

namespace BookingGuru.Modules.Mock2s.Infrastructure.Inbox;

internal sealed class IdempotentIntegrationEventHandler<TIntegrationEvent>(
    IIntegrationEventHandler<TIntegrationEvent> decorated,
    IDbConnectionFactory dbConnectionFactory)
    : IntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    public override async Task Handle(
        TIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var inboxMessageConsumer = new InboxMessageConsumer(integrationEvent.Id, decorated.GetType().Name);

        if (await InboxConsumerExistsAsync(connection, inboxMessageConsumer))
        {
            return;
        }

        await decorated.Handle(integrationEvent, cancellationToken);

        await InsertInboxConsumerAsync(connection, inboxMessageConsumer);
    }

    private static async Task<bool> InboxConsumerExistsAsync(
        DbConnection dbConnection,
        InboxMessageConsumer inboxMessageConsumer)
    {
        const string sql =
            $"""
            SELECT COUNT(1)
                FROM {Schemas.Mock2s}.{nameof(InboxMessageConsumer)}
                WHERE inboxMessageId = @InboxMessageId AND
                      name = @Name
            """;

        return await dbConnection.ExecuteScalarAsync<bool>(sql, inboxMessageConsumer);
    }

    private static async Task InsertInboxConsumerAsync(
        DbConnection dbConnection,
        InboxMessageConsumer inboxMessageConsumer)
    {
        const string sql =
            $"""
            INSERT INTO {Schemas.Mock2s}.{nameof(InboxMessageConsumer)}(inboxMessageId, name)
            VALUES (@InboxMessageId, @Name)
            """;

        await dbConnection.ExecuteAsync(sql, inboxMessageConsumer);
    }
}
