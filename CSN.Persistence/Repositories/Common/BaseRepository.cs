using CSN.Domain.Entities.Common;
using CSN.Domain.Interfaces.Repository;
using CSN.Infrastructure.Extensions;
using CSN.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSN.Persistence.Repositories.Common
{
    public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbSet<TEntity> dbSet;

        public BaseRepository(EFContext dbContext)
        {
            this.dbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
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
    }
}
