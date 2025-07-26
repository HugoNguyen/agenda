using BookingGuru.Common.Application.Messaging;
using BookingGuru.Common.Domain;
using BookingGuru.Modules.Mocks.Application.Abstractions.Data;
using BookingGuru.Modules.Mocks.Domain.FirstFeats;

namespace BookingGuru.Modules.Mocks.Application.FirstFeats.CreateFirstEntity;

internal sealed class CreateFirstEntityCommandHandler(IRepository<FirstEntity, Guid> firstEntityRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateFirstEntityCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateFirstEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = FirstEntity.Create(request.Field1, request.Field1Nullable, request.Field2Utc);

        firstEntityRepository.Insert(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(true);

        return entity.Id;
    }
}
