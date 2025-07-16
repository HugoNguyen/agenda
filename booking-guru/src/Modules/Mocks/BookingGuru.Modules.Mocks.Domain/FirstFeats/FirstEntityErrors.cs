using BookingGuru.Common.Domain;

namespace BookingGuru.Modules.Mocks.Domain.FirstFeats;

public static class FirstEntityErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("FirstEntities.NotFound", $"The entity with the identifier {id} was not found");
}