using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EUCore.Entity;
using JetBrains.Annotations;

namespace EUCore.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>, new()
    {

    }
    public interface IRepository<TEntity, TPrimaryKey> :
        IPagedRepository<TEntity, TPrimaryKey>,
        ICrudRepository<TEntity, TPrimaryKey>,
        IBasicRepository<TEntity, TPrimaryKey>,
        IGetRepository<TEntity, TPrimaryKey>,
        ICountRepository<TEntity>,
        IListRepository<TEntity>
        where TEntity : class, IEntity<TPrimaryKey>, new()
    {

    }

    public interface IPagedRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        Task<IEnumerable<TEntity>> GetPagedAsync([NotNull] Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage);
        IEnumerable<TEntity> GetAllPaged(int pageNumber = 0, int itemsPerPage = 10);
        IEnumerable<TEntity> GetPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
    }
    public interface ICrudRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        TPrimaryKey InsertAndGetId([NotNull] TEntity entity);
        [NotNull]
        Task InsertAsync([NotNull] TEntity entity);
        [NotNull]
        Task<TPrimaryKey> InsertAndGetIdAsync([NotNull] TEntity entity);
        [NotNull]
        Task<bool> UpdateAsync([NotNull] TEntity entity);
        void Delete([NotNull] Expression<Func<TEntity, bool>> predicate, bool force = false);
        [NotNull]
        Task DeleteAsync([NotNull] TEntity entity, bool force = false);
        [NotNull]
        Task DeleteAsync([NotNull] Expression<Func<TEntity, bool>> predicate, bool force = false);
    }
    public interface IBasicRepository<TEntity, in TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        [NotNull]
        TEntity Get([NotNull] TPrimaryKey id);
        void Insert([NotNull] TEntity entity);
        bool Update([NotNull] TEntity entity);
        void Delete([NotNull] TEntity entity, bool force = false);
    }
    public interface IGetRepository<TEntity, in TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        [NotNull]
        TEntity Single([NotNull] TPrimaryKey id);
        [NotNull]
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        [NotNull]
        Task<TEntity> SingleAsync([NotNull] TPrimaryKey id);
        [NotNull]
        Task<TEntity> GetAsync([NotNull] TPrimaryKey id);

        [CanBeNull]
        TEntity First([NotNull] TPrimaryKey id);
        [CanBeNull]
        Task<TEntity> FirstAsync([NotNull] TPrimaryKey id);
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);

        [CanBeNull]
        TEntity FirstOrDefault([NotNull] TPrimaryKey id);
        [CanBeNull]
        Task<TEntity> FirstOrDefaultAsync([NotNull] TPrimaryKey id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
    public interface ICountRepository<TEntity> : IRepository where TEntity : class, IEntity, new()
    {
        long Count(Expression<Func<TEntity, bool>> predicate);
        [NotNull]
        Task<long> CountAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
    }
    public interface IListRepository<TEntity> : IRepository where TEntity : class, IEntity, new()
    {
        [NotNull]
        IEnumerable<TEntity> GetAll();
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllAsync();
        [NotNull]
        IEnumerable<TEntity> GetAll([NotNull] Expression<Func<TEntity, bool>> predicate);
        [NotNull]
        Task<IEnumerable<TEntity>> GetAllAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
    }
    public interface IRepository
    {

    }
}
