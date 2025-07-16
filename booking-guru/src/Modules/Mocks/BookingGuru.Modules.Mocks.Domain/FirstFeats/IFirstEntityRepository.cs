
namespace BookingGuru.Modules.Mocks.Domain.FirstFeats;

public interface IFirstEntityRepository
{
    Task<FirstEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(FirstEntity entity);
}