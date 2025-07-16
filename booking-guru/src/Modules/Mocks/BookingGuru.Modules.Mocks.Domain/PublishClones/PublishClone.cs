using BookingGuru.Common.Domain.Entities;

namespace BookingGuru.Modules.Mocks.Domain.PublishClones;

public sealed class PublishClone : DomainEntity<Guid>
{
    private PublishClone(Guid id) : base(id) { }

    public string Name { get; private set; }

    public DateTimeOffset PublishDateUtc { get; private set; }

    public static PublishClone Create(Guid publishId, string name, DateTimeOffset publishDateUtc)
    {
        var obj = new PublishClone(publishId)
        {
            Name = name,
            PublishDateUtc = publishDateUtc,
        };

        return obj;
    }
}
