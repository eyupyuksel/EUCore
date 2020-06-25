using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EUCore.Audit;
using EUCore.Entity;

namespace EUCore.Repositories.Dapper
{
    public abstract class DapperRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>, IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        protected DapperRepositoryBase(IAuditManager auditManager) : base(auditManager)
        {
        }

        public abstract TEntity Get(TPrimaryKey id);
        public virtual Task<TEntity> GetAsync(TPrimaryKey id) => Task.FromResult(Get(id));

        public abstract TEntity Single(TPrimaryKey id);
        public virtual Task<TEntity> SingleAsync(TPrimaryKey id) => Task.FromResult(Single(id));
        public abstract TEntity Single(Expression<Func<TEntity, bool>> predicate);
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(Single(predicate));

        public abstract IEnumerable<TEntity> GetAll();
        public virtual Task<IEnumerable<TEntity>> GetAllAsync() => Task.FromResult(GetAll());
        public abstract IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        public abstract IEnumerable<TEntity> GetAllByNoFilter(Expression<Func<TEntity, bool>> predicate);
        public virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(GetAll(predicate));

        public virtual Task<IEnumerable<TEntity>> GetPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage) => Task.FromResult(GetPaged(predicate, pageNumber, itemsPerPage));
        public virtual IEnumerable<TEntity> GetAllPaged(int pageNumber = 0, int itemsPerPage = 10) => GetPaged(null, pageNumber, itemsPerPage);
        public abstract IEnumerable<TEntity> GetPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);

        public abstract void Update(TEntity entity);
        public virtual Task UpdateAsync(TEntity entity) => Task.Run(() => Update(entity));

        public abstract void Delete(TEntity entity, bool force = false);

        public virtual Task DeleteAsync(TEntity entity, bool force = false) => Task.Run(() => Delete(entity, force));
        public abstract void Delete(Expression<Func<TEntity, bool>> predicate, bool force = false);
        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool force = false) => Task.Run(() => Delete(predicate, force));

        public abstract void Insert(TEntity entity);
        public virtual Task InsertAsync(TEntity entity) => Task.Run(() => Insert(entity));
        public abstract TPrimaryKey InsertAndGetId(TEntity entity);
        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity) => Task.FromResult(InsertAndGetId(entity));


        public abstract TEntity First(TPrimaryKey id);
        public virtual Task<TEntity> FirstAsync(TPrimaryKey id) => Task.FromResult(First(id));
        public abstract TEntity First(Expression<Func<TEntity, bool>> predicate);
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(First(predicate));
        public abstract TEntity FirstOrDefault(TPrimaryKey id);
        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id) => Task.FromResult(FirstOrDefault(id));
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(FirstOrDefault(predicate));
        public abstract TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        public abstract long Count(Expression<Func<TEntity, bool>> predicate);
        public abstract Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
