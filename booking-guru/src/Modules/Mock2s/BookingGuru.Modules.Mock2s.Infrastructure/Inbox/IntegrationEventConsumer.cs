using BookingGuru.Common.Application.Data;
using BookingGuru.Common.Application.EventBus;
using BookingGuru.Common.Infrastructure.Inbox;
using BookingGuru.Common.Infrastructure.Serialization;
using BookingGuru.Modules.Mock2s.Infrastructure.Database;
using Dapper;
using MassTransit;
using Newtonsoft.Json;
using System.Data.Common;

namespace BookingGuru.Modules.Mock2s.Infrastructure.Inbox;

internal sealed class IntegrationEventConsumer<TIntegrationEvent>(IDbConnectionFactory dbConnectionFactory)
    : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        TIntegrationEvent integrationEvent = context.Message;

        var inboxMessage = new InboxMessage
        {
            Id = integrationEvent.Id,
            Type = integrationEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(integrationEvent, SerializerSettings.Instance),
            OccurredOnUtc = integrationEvent.OccurredOnUtc
        };

        const string sql =
            $"""
            INSERT INTO {Schemas.Mock2s}.{nameof(InboxMessage)}(id, type, content, occurredOnUtc)
            VALUES (@Id, @Type, @Content, @OccurredOnUtc)
            """;

        await connection.ExecuteAsync(sql, inboxMessage);
    }
}
