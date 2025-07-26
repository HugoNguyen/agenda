using BookingGuru.Common.Domain.Entities;
using BookingGuru.Common.Infrastructure.Repositories;
using BookingGuru.Modules.Mock2s.Application.Abstractions.Data;
using BookingGuru.Modules.Mock2s.Infrastructure.Database;

namespace BookingGuru.Modules.Mock2s.Infrastructure.Repositories;

internal class Repository<TEntity, TPrimaryKey> : RepositoryBase<Mock2sDbContext, TEntity, TPrimaryKey>, IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    where TPrimaryKey : struct
{
    public Repository(Mock2sDbContext context) : base(context)
    {
    }
}