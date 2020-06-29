using System;
using System.Linq.Expressions;
using EUCore.Entity;
using EUCore.Entity.Auditing;
using EUCore.Extension;
namespace EUCore.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static Expression<Func<TEntity, bool>> AndDeleteFilter<TEntity>(this Expression<Func<TEntity, bool>> predicate)
            where TEntity : IEntity
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                var filter = CreateCondition<TEntity, bool>("IsDeleted", false);
                if (predicate == null)
                    return filter;
                return predicate.And(filter);
            }
            return predicate; //?? (x => true);
        }

        public static Expression<Func<TEntity, bool>> And<TEntity, TProperty>(
            this Expression<Func<TEntity, bool>> predicate, string name, TProperty value)
            where TEntity : IEntity
            where TProperty : struct
            => predicate.And(CreateCondition<TEntity, TProperty>(name, value));

        public static Expression<Func<TEntity, bool>> CreateKeyCondition<TEntity, TPrimaryKey>(this TPrimaryKey id)
            where TEntity : IEntity<TPrimaryKey>
            => CreateCondition<TEntity, TPrimaryKey>("Id", id);

        private static Expression<Func<TEntity, bool>> CreateCondition<TEntity, TProperty>(string propertyName, TProperty value)
            where TEntity : IEntity
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, propertyName),
                Expression.Constant(value, typeof(TProperty))
            );
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
