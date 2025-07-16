using BookingGuru.Common.Domain.Entities;

namespace BookingGuru.Common.Domain.Repositories;


public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();

    void Insert(TEntity creatingEntity);
    void Update(TEntity updatingEntity);
    void Delete(TEntity deletingEntity);

    void RemoveRange(IEnumerable<TEntity> entities);
}

public interface IRepository<TEntity, TPrimaryKey>
    : IRepository<TEntity>
    where TEntity : class, IEntity<TPrimaryKey>
    where TPrimaryKey : struct
{
    Task<TEntity?> FindByIdAsync(TPrimaryKey id);

    TEntity? FindById(TPrimaryKey id);

    
    Task DeleteAsync(TPrimaryKey id);
}