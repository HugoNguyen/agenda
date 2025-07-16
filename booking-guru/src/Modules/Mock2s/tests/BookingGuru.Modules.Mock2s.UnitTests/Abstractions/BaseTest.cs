using Bogus;
using BookingGuru.Common.Domain;
using BookingGuru.Common.Domain.Entities;

namespace BookingGuru.Modules.Mock2s.UnitTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Faker Faker = new();

    public static T AssertDomainEventWasPublished<T>(IDomainEntity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.DomainEvents.OfType<T>().SingleOrDefault();

        if (domainEvent is null)
        {
            throw new Exception($"{typeof(T).Name} was not published");
        }

        return domainEvent;
    }
}
