using System;
using System.Linq;
using EUCore.Audit;
using EUCore.Entity;

namespace EUCore.Repositories
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        private readonly IAuditManager _auditManager;

        protected RepositoryBase(IAuditManager auditManager)
        {
            _auditManager = auditManager;
        }
        protected void OnInsert(TEntity entity)
        {

            _auditManager.FilterCreation<TEntity, TPrimaryKey>(entity);
            DoCreationReadOnly(false);
        }
        protected void AfterInsert(TEntity entity)
        {
            DoCreationReadOnly(true);
        }
        private void DoCreationReadOnly(bool onOff)
        {
            //todo: yavaşlatabilir.
            var properties = typeof(TEntity).GetProperties();
            var creationTime = properties.SingleOrDefault(p => p.Name.Equals("CreationTime"));
            creationTime?.GetType().GetProperty("IsReadOnly")?.SetValue(creationTime, onOff);

            var creatorUserId = properties.SingleOrDefault(p => p.Name.Equals("CreatorUserId"));
            creatorUserId?.GetType().GetProperty("IsReadOnly")?.SetValue(creatorUserId, onOff);
        }
        protected void OnUpdate(TEntity entity)
        {
            _auditManager.FilterModification<TEntity, TPrimaryKey>(entity);
            DoCreationReadOnly(true);
        }
        protected void OnDelete(TEntity entity)
        {
            _auditManager.FilterDeletion<TEntity, TPrimaryKey>(entity);
        }
    }
}
