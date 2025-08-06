using BookingGuru.Common.Domain.Attributes;
using BookingGuru.Common.Infrastructure.Repositories;
using BookingGuru.Modules.Mocks.Application.Repositories;
using BookingGuru.Modules.Mocks.Domain.FirstFeats;
using BookingGuru.Modules.Mocks.Infrastructure.Database;

namespace BookingGuru.Modules.Mocks.Infrastructure.FirstFeats;

[ModularRepository]
public sealed class FirstEntityRepository(MocksDbContext context) : RepositoryBase<MocksDbContext, FirstEntity, Guid>(context), IFirstEntityRepository
{
    public Task<FirstEntity?> CustomGetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return FindByIdAsync(id);
    }
}