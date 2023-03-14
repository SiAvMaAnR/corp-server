using CSN.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CSN.Persistence.Extensions;

public static class QueryableExtension
{
    public static IQueryable<TEntity> MultipleInclude<TEntity>(this IQueryable<TEntity> dbSet,
       params Expression<Func<TEntity, object>>[] includeProperties)
       where TEntity : BaseEntity
    {
        return includeProperties.Aggregate(dbSet,
            (current, includeProperty) => current.Include(includeProperty));
    }
}

