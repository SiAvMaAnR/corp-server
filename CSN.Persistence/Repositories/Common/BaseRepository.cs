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
        protected readonly DbSet<TEntity> dbSet;

        public BaseRepository(EFContext dbContext)
        {
            this.dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await this.dbSet.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Task.FromResult(this.dbSet.Update(entity));
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            await Task.FromResult(this.dbSet.Remove(entity));
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await this.dbSet.MultipleInclude(includeProperties)
                .FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(this.dbSet
                .Where(predicate));
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            return await Task.FromResult(this.dbSet);
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Task.FromResult(this.dbSet
                .MultipleInclude(includeProperties));
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await Task.FromResult(this.dbSet
                .MultipleInclude(includeProperties)
                .Where(predicate));

        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.dbSet.AnyAsync(predicate);
        }

        public virtual async Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.dbSet.AllAsync(predicate);
        }

        public virtual async Task<IQueryable<TEntity>> CustomAsync()
        {
            return await Task.FromResult(this.dbSet);
        }
    }
}
