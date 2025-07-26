using BookingGuru.Common.Domain.Entities;

namespace BookingGuru.Modules.Mock2s.Domain.Publishes;

public sealed class Publish : DomainEntity<Guid>
{
    private Publish() { }

    public string Name { get; private set; }

    public DateTimeOffset PublishDateUtc { get; private set; }

    public static Publish Create(string name, DateTimeOffset publishDateUtc)
    {
        var obj = new Publish
        {
            Id = Guid.NewGuid(),
            Name = name,
            PublishDateUtc = publishDateUtc,
        };

        obj.Raise(new PublishCreatedDomainEvent(obj.Id));

        return obj;
    }
}
