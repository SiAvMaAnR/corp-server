using CSN.Domain.Common;
using CSN.Domain.Interfaces.Repository;
using CSN.Persistence.DBContext;
using CSN.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CSN.Persistence.Repositories.Common
{
    public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(EFContext dbContext)
        {
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            await Task.FromResult(dbSet.Update(entity));
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Task.FromResult(dbSet.Remove(entity));
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await dbSet.MultipleInclude(includeProperties)
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(dbSet
                .AsNoTracking()
                .Where(predicate));
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            return await Task.FromResult(dbSet.AsNoTracking());
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Task.FromResult(dbSet
                .MultipleInclude(includeProperties)
                .AsNoTracking());
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Task.FromResult(dbSet
                .MultipleInclude(includeProperties)
                .AsNoTracking()
                .Where(predicate));

        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }

        public virtual async Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AllAsync(predicate);
        }
    }
}
