using System;
using EUCore.Entity;
using DapperExtensions;
using Dommel;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EUCore.Audit;
using EUCore.UnitofWorks;
using EUCore.Repositories.Extensions;
using EUCore.Extension;

namespace EUCore.Repositories.Dapper
{
    public class DapperRepository<TEntity, TPrimaryKey> : DapperRepositoryBase<TEntity, TPrimaryKey>
       where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        protected DbConnection Connection => (DbConnection)_unitOfWorkManager.GetConnection();
        protected DbTransaction Transaction => (DbTransaction)_unitOfWorkManager.GetTransaction();

        public DapperRepository(IAuditManager auditManager, IUnitOfWorkManager unitOfWorkManager) : base(auditManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }
        public override TEntity Single(TPrimaryKey id)
        {
            return Single(id.CreateKeyCondition<TEntity, TPrimaryKey>());
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.Select(predicate.AndDeleteFilter(), Transaction).Single();
        }

        public override TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            var result = FirstOrDefault(predicate);
            return result ?? throw new KeyNotFoundException();
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(id.CreateKeyCondition<TEntity, TPrimaryKey>());
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.Select(predicate.AndDeleteFilter(), Transaction)?.FirstOrDefault();
        }

        public override TEntity Get(TPrimaryKey id) => DommelMapper.Get<TEntity>(Connection, id, Transaction) ?? throw new Exception("EntityNotFoundException");

        public override IEnumerable<TEntity> GetAll()
        {
            return Connection.Select(default(Expression<Func<TEntity, bool>>).AndDeleteFilter(), Transaction);
        }
        public override long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.Count(predicate.AndDeleteFilter(), Transaction);
        }

        public override Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.CountAsync(predicate, Transaction);
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.Select(predicate.AndDeleteFilter(), Transaction);
        }
        public override IEnumerable<TEntity> GetAllByNoFilter(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.GetAll<TEntity>(Transaction);
        }
        public override IEnumerable<TEntity> GetPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            return Connection.SelectPaged(predicate.AndDeleteFilter(), pageNumber, pageSize, Transaction);
        }
        public override void Insert(TEntity entity) => InsertAndGetId(entity);
        public override bool Update(TEntity entity)
        {
            OnUpdate(entity);
            return Connection.Update(entity, Transaction);
        }
        public override void Delete(TEntity entity, bool force = false)
        {
            if (force)
            {
                Connection.Delete(entity, Transaction);
                return;
            }
            OnDelete(entity);
            if (entity is ISoftDelete)
                Connection.Update(entity, Transaction);
            else
                Connection.Delete(entity, Transaction);
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate, bool force = false)
        {
            if (force)
                Connection.DeleteMultiple(predicate, Transaction);
            GetAll(predicate.AndDeleteFilter()).ForEach(x => Delete(x, force));
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            OnInsert(entity);
            TPrimaryKey primaryKey = Connection.Insert(entity, Transaction);
            AfterInsert(entity);
            return primaryKey;
        }
        public override TEntity First(TPrimaryKey id) => FirstOrDefault(id) ?? throw new Exception("EntityNotFoundException");
    }
}
