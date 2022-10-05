using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using CSN.Domain.Entities.Common;

namespace CSN.Infrastructure.Extensions
{
    public static class QueryableExtention
    {
        public static IQueryable<TEntity> MultipleInclude<TEntity>(this IQueryable<TEntity> dbSet,
           params Expression<Func<TEntity, object>>[] includeProperties)
           where TEntity : BaseEntity
        {
            return includeProperties.Aggregate(dbSet,
                (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
