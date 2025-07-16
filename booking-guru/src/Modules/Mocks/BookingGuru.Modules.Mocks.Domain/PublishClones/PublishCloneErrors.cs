using BookingGuru.Common.Domain;

namespace BookingGuru.Modules.Mocks.Domain.Publishes;

public static class PublishCloneErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("PublishClones.NotFound", $"The entity with the identifier {id} was not found");
}