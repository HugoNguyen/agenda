using BookingGuru.Common.Application.Repositories;
using BookingGuru.Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingGuru.Common.Infrastructure.Repositories;

public class RepositoryBase<TDbContext, TEntity, TPrimaryKey>
    : IRepository<TEntity, TPrimaryKey>
    where TDbContext : ModuleDbContext
    where TEntity : class, IEntity<TPrimaryKey>
{
    protected TDbContext _dbContext;
    protected DbSet<TEntity> _dbSet;

    public RepositoryBase(TDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _dbContext = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        query.FirstAsync().Wait();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query;
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetAll(predicate, includes);
        return query.ToList();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetAll(predicate, includes);
        return await query.ToListAsync().ConfigureAwait(true);
    }

    public virtual async Task<TEntity?> FindByIdAsync(TPrimaryKey id)
    {
        return await _dbSet.FindAsync(id).ConfigureAwait(true);
    }

    public virtual TEntity? FindById(TPrimaryKey id)
    {
        return _dbSet.Find(id);
    }

    public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetAll(predicate, includes);
        return query.FirstOrDefault();
    }

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetAll(predicate, includes);
        return query.FirstOrDefaultAsync();
    }

    public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetAll(predicate, includes);
        return query.SingleOrDefault();
    }

    public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = GetAll(predicate, includes);
        return query.SingleOrDefaultAsync();
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
        TEntity? entityToDelete = await _dbSet.FindAsync(id).ConfigureAwait(true);
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