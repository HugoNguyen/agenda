using BookingGuru.Modules.Mocks.Domain.SecondFeats;
using BookingGuru.Modules.Mocks.IntegrationTests.Abstractions;
using FluentAssertions;

namespace BookingGuru.Modules.Mocks.IntegrationTests.SecondFeats;

public class SecondFeatTest : BaseIntegrationTest
{
    public SecondFeatTest(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_SecondEntityHasUser_AfterInserting()
    {
        var user = new User(Guid.NewGuid())
        {
            UserName = Faker.Name.FirstName(),
        };

        var secondEntity = new SecondEntity(Guid.NewGuid()) { Field1 = Faker.Name.LastName(), CreationTime = DateTime.Now, CreatorUser = user };

        DbContext.SecondEntities.Add(secondEntity);

        await DbContext.SaveChangesAsync();

        var retrieve = await DbContext.SecondEntities.FindAsync(secondEntity.Id);

        retrieve.CreatorUserId.Should().Be(user.Id);
    }
}