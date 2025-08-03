using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Application.Repositories;
using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mocks.Application.Publishes.GetPublishClone;
using BookingGuru.Modules.Mocks.Domain.PublishClones;
using BookingGuru.Modules.Mocks.Domain.Publishes;

namespace BookingGuru.Modules.Mocks.Application.PublishClones.GetPublishClone;

internal sealed class GetPublishQueryHandler(
    IRepository<PublishClone, Guid> publishRepository)
    : IQueryHandler<GetPublishCloneQuery, PublishCloneResponse>
{
    public async Task<Result<PublishCloneResponse>> Handle(GetPublishCloneQuery request, CancellationToken cancellationToken)
    {
        var entity = await publishRepository.FindByIdAsync(request.Id).ConfigureAwait(true);

        if (entity is null)
        {
            return Result.Failure<PublishCloneResponse>(PublishCloneErrors.NotFound(request.Id));
        }

        return new PublishCloneResponse(entity.Id, entity.Name, entity.PublishDateUtc);
    }
}