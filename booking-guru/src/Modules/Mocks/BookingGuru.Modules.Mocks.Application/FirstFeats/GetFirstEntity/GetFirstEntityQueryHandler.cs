using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mocks.Application.Abstractions.Data;
using BookingGuru.Modules.Mocks.Domain.FirstFeats;

namespace BookingGuru.Modules.Mocks.Application.FirstFeats.GetFirstEntity;

internal sealed class GetFirstEntityQueryHandler(
    IRepository<FirstEntity, Guid> firstEntityRepository)
    : IQueryHandler<GetFirstEntityQuery, FirstEntityResponse>
{
    public async Task<Result<FirstEntityResponse>> Handle(GetFirstEntityQuery request, CancellationToken cancellationToken)
    {
        var entity = await firstEntityRepository.FindByIdAsync(request.Id).ConfigureAwait(true);

        if (entity is null)
        {
            return Result.Failure<FirstEntityResponse>(FirstEntityErrors.NotFound(request.Id));
        }

        return new FirstEntityResponse(entity.Id, entity.Field1, entity.Field1Nullable, entity.Field2Utc);
    }
}