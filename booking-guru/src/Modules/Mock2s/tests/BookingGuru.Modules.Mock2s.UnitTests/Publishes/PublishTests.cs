using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mock2s.Domain.Publishes;
using BookingGuru.Modules.Mock2s.UnitTests.Abstractions;
using FluentAssertions;

namespace BookingGuru.Modules.Mock2s.UnitTests.Publishes;

public class PublishTests : BaseTest
{
    [Fact]
    public void Create_ShouldRaiseDomainEvent_WhenPublishIsCreated()
    {
        //Act
        Result<Publish> result = Publish.Create(Faker.Music.Genre(), DateTime.Now);

        //Assert
        PublishCreatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<PublishCreatedDomainEvent>(result.Value);

        domainEvent.PublishId.Should().Be(result.Value.Id);
    }
}