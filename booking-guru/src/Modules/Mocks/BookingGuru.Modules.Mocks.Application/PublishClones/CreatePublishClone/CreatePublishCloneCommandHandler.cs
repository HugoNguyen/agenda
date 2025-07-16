using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mocks.Application.Abstractions.Data;
using BookingGuru.Modules.Mocks.Domain.PublishClones;

namespace BookingGuru.Modules.Mocks.Application.PublishClones.CreatePublishClone;

internal sealed class CreatePublishCloneCommandHandler(IRepository<PublishClone, Guid> publishCloneRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePublishCloneCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePublishCloneCommand request, CancellationToken cancellationToken)
    {
        var entity = PublishClone.Create(request.PublishId, request.Name, request.PublishDateUtc);

        publishCloneRepository.Insert(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(true);

        return entity.Id;
    }
}