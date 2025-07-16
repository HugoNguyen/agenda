using BookingGuru.Modules.Mocks.Domain.FirstFeats;
using BookingGuru.Modules.Mocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BookingGuru.Modules.Mocks.Infrastructure.FirstFeats;

internal sealed class FirstEntityRepository(MocksDbContext context) : IFirstEntityRepository
{
    public async Task<FirstEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.FirstEntities.SingleOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public void Insert(FirstEntity entity)
    {
        context.FirstEntities.Add(entity);
    }
}