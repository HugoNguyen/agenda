using BookingGuru.Common.Domain.Entities;
using BookingGuru.Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingGuru.Common.Infrastructure.Repositories;

public abstract class RepositoryBase<TDbContext, TEntity, TPrimaryKey>
    : IRepository<TEntity, TPrimaryKey>
    where TDbContext : DbContext
    where TEntity : class, IEntity<TPrimaryKey>
    where TPrimaryKey : struct
{
    protected TDbContext _dbContext;
    protected DbSet<TEntity> _dbSet;

    protected RepositoryBase(TDbContext context)
    {
        _dbContext = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public virtual async Task<TEntity?> FindByIdAsync(TPrimaryKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual TEntity? FindById(TPrimaryKey id)
    {
        return _dbSet.Find(id);
    }

    public virtual void Insert(TEntity creatingEntity)
    {
        _dbSet.Add(creatingEntity);
    }

    public virtual void Update(TEntity updatingEntity)
    {
        _dbSet.Attach(updatingEntity);
        _dbContext.Entry(updatingEntity).State = EntityState.Modified;
    }

    public virtual async Task DeleteAsync(TPrimaryKey id)
    {
        ArgumentNullException.ThrowIfNull(_dbSet);
        TEntity? entityToDelete = await _dbSet.FindAsync(id);
        ArgumentNullException.ThrowIfNull(entityToDelete);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity deletingEntity)
    {
        if (_dbContext.Entry(deletingEntity).State == EntityState.Detached)
        {
            _dbSet.Attach(deletingEntity);
        }
        _dbSet.Remove(deletingEntity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        foreach (var entity in entities)
        {
            Delete(entity);
        }
    }
}