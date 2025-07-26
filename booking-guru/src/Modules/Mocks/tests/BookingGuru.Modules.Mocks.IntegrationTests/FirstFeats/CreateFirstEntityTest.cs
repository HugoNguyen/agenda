using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mocks.Application.FirstFeats.CreateFirstEntity;
using BookingGuru.Modules.Mocks.Application.FirstFeats.GetFirstEntity;
using BookingGuru.Modules.Mocks.IntegrationTests.Abstractions;
using FluentAssertions;
using System.Net.Http.Json;

namespace BookingGuru.Modules.Mocks.IntegrationTests.FirstFeats;

public class CreateFirstEntityTest : BaseIntegrationTest
{
    public CreateFirstEntityTest(IntegrationTestWebAppFactory factory)
        : base(factory)
    {

    }

    public static readonly TheoryData<string, DateTime?> CreateFirstEntiyData = new()
    {
        { Faker.Music.Genre(), DateTime.Now },
        { Faker.Music.Genre(), DateTime.UtcNow },
        { Faker.Music.Genre(), null },
    };

    [Theory]
    [MemberData(nameof(CreateFirstEntiyData))]
    public async Task Should_Field2UtcNotChange_AfterCreating(string field1, DateTime? field2)
    {
        // Arrange
        Result<Guid> result = await Sender.Send(new CreateFirstEntityCommand(field1, null, field2));
        Guid newId = result.Value;
        // Act
        Result<FirstEntityResponse> firstEntityResult = await Sender.Send(new GetFirstEntityQuery(newId));

        // Assert
        
        firstEntityResult.Value.Field2Utc.Should().Be(field2);
    }

    [Fact]
    public async Task Should_ReadDateTime_AsUtc()
    {
        // Arrange
        var payload = new
        {
            Field1 = Faker.Name.FirstName(),
            Field2 = "2025-01-01",
        };

        var expect = DateTime.SpecifyKind(DateTime.Parse(payload.Field2), DateTimeKind.Utc);

        // Act
        var httpResponse = await HttpClient.PostAsJsonAsync("/first-entities", payload);
        httpResponse.EnsureSuccessStatusCode();
        Guid.TryParse(await httpResponse.Content.ReadFromJsonAsync<string>(), out Guid newId);
        var newItem = DbContext.FirstEntities.Single(q => q.Id == newId);

        // Assert
        newItem.Field2Utc.Should().Be(expect);
    }
}