using BookingGuru.Common.Domain.Entities;
using BookingGuru.Common.Infrastructure.Repositories;
using BookingGuru.Modules.Mocks.Application.Abstractions.Data;
using BookingGuru.Modules.Mocks.Infrastructure.Database;

namespace BookingGuru.Modules.Mocks.Infrastructure.Repositories;

internal class Repository<TEntity, TPrimaryKey> : RepositoryBase<MocksDbContext, TEntity, TPrimaryKey>, IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    where TPrimaryKey : struct
{
    public Repository(MocksDbContext context) : base(context)
    {
    }
}