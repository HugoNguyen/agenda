using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Application.Repositories;
using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mock2s.Application.Abstractions.Data;
using BookingGuru.Modules.Mock2s.Domain.Publishes;

namespace BookingGuru.Modules.Mock2s.Application.Publishes.CreatePublish;

internal sealed class CreatePublishCommandHandler(IRepository<Publish, Guid> publishRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePublishCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePublishCommand request, CancellationToken cancellationToken)
    {
        var entity = Publish.Create(request.Name, request.PublishDateUtc);

        publishRepository.Insert(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(true);

        return entity.Id;
    }
}