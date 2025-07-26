using BookingGuru.Common.Domain.Entities;

namespace BookingGuru.Modules.Mock2s.Application.Abstractions.Data;

public interface IRepository<TEntity, TPrimaryKey>
    : Common.Domain.Repositories.IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    where TPrimaryKey : struct
{
}