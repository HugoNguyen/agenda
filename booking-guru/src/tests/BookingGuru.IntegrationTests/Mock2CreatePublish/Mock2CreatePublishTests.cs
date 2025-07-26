using BookingGuru.Common.Domain;
using BookingGuru.IntegrationTests.Abstractions;
using BookingGuru.Modules.Mock2s.Application.Publishes.CreatePublish;
using BookingGuru.Modules.Mocks.Application.Publishes.GetPublishClone;
using FluentAssertions;

namespace BookingGuru.IntegrationTests.Mock2CreatePublish;

public sealed class Mock2CreatePublishTests : BaseIntegrationTest
{
    public Mock2CreatePublishTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
        
    }

    [Fact]
    public async Task CreatePublish_Should_PropagateToMockModule()
    {
        // Create Publish
        var command = new CreatePublishCommand(Faker.Internet.Email(), DateTime.UtcNow);

        Result<Guid> createPublishResult = await Sender.Send(command);

        createPublishResult.IsSuccess.Should().BeTrue();

        // Get Publish
        Result<PublishCloneResponse> publishCloneResult = await Poller.WaitAsync(
            TimeSpan.FromSeconds(15),
            async () =>
            {
                var query = new GetPublishCloneQuery(createPublishResult.Value);
                Result<PublishCloneResponse> result = await Sender.Send(query);
                return result;
            });

        // Assert
        publishCloneResult.IsSuccess.Should().BeTrue();
    }
}
