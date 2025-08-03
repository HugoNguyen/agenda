using BookingGuru.Common.Infrastructure.Inbox;
using BookingGuru.Common.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace BookingGuru.Common.Infrastructure.Repositories;

public class ModuleDbContextBuilder : IModuleDbContextBuilder
{
    private bool _useInbox;
    private bool _useOutbox;
    private string? _defaultSchema;

    public ModuleDbContextBuilder WithDefaultSchema(string schema)
    {
        _defaultSchema = schema;
        return this;
    }

    public ModuleDbContextBuilder WithInbox()
    {
        _useInbox = true;
        return this;
    }

    public ModuleDbContextBuilder WithOutbox()
    {
        _useOutbox = true;
        return this;
    }

    public void Configure(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        if (!string.IsNullOrEmpty(_defaultSchema))
        {
            modelBuilder.HasDefaultSchema(_defaultSchema);
        }

        if (_useInbox)
        {
            modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
            modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        }

        if (_useOutbox)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        }
    }
}