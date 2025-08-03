using BookingGuru.Common.Domain.Entities;
using System.Linq.Expressions;

namespace BookingGuru.Common.Application.Repositories;


public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    void Insert(TEntity creatingEntity);
    void Update(TEntity updatingEntity);
    void Delete(TEntity deletingEntity);

    void RemoveRange(IEnumerable<TEntity> entities);
}

public interface IRepository<TEntity, TPrimaryKey>
    : IRepository<TEntity>
    where TEntity : class, IEntity<TPrimaryKey>
{
    TEntity? FindById(TPrimaryKey id);

    Task<TEntity?> FindByIdAsync(TPrimaryKey id);
    
    Task DeleteAsync(TPrimaryKey id);
}