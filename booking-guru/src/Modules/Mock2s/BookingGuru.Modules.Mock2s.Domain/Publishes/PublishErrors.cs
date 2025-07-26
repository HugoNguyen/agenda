using BookingGuru.Common.Domain;

namespace BookingGuru.Modules.Mock2s.Domain.Publishes;

public static class PublishErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Publishes.NotFound", $"The entity with the identifier {id} was not found");
}