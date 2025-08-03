using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Application.Repositories;
using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mock2s.Domain.Publishes;

namespace BookingGuru.Modules.Mock2s.Application.Publishes.GetPublish;

internal sealed class GetPublishQueryHandler(
    IRepository<Publish, Guid> publishRepository)
    : IQueryHandler<GetPublishQuery, PublishResponse>
{
    public async Task<Result<PublishResponse>> Handle(GetPublishQuery request, CancellationToken cancellationToken)
    {
        var entity = await publishRepository.FindByIdAsync(request.Id).ConfigureAwait(true);

        if (entity is null)
        {
            return Result.Failure<PublishResponse>(PublishErrors.NotFound(request.Id));
        }

        return new PublishResponse(entity.Id, entity.Name, entity.PublishDateUtc);
    }
}