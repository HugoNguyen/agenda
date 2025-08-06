using BookingGuru.Common.Application.Repositories;
using BookingGuru.Modules.Mocks.Domain.FirstFeats;

namespace BookingGuru.Modules.Mocks.Application.Repositories;

public interface IFirstEntityRepository : IRepository<FirstEntity, Guid>
{
    Task<FirstEntity?> CustomGetAsync(Guid id, CancellationToken cancellationToken = default);
}