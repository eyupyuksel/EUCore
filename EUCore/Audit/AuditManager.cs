using System;
using EUCore.Entity;
using EUCore.Entity.Auditing;
using EUCore.Services;

namespace EUCore.Audit
{
    public interface IAuditManager : IService
    {
        void FilterCreation<TEntity, TPrimaryKey>(TEntity instance) where TEntity : class, IEntity<TPrimaryKey>, new();
        void FilterModification<TEntity, TPrimaryKey>(TEntity instance) where TEntity : class, IEntity<TPrimaryKey>, new();
        void FilterDeletion<TEntity, TPrimaryKey>(TEntity instance) where TEntity : class, IEntity<TPrimaryKey>, new();
    }
    public class AuditManager : IAuditManager
    {
        public AuditManager()
        {
        }
        public void FilterCreation<TEntity, TPrimaryKey>(TEntity instance) where TEntity : class, IEntity<TPrimaryKey>, new()
        {

            if (instance is IHasCreationTime hasCreation)
                hasCreation.CreationTime = DateTime.Now;

            //if (instance is ICreationAudited creation)
            //    creation.CreatorUserId = creation.CreatorUserId ?? _provider.User.Id;
        }

        public void FilterModification<TEntity, TPrimaryKey>(TEntity instance) where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            if (instance is IHasModificationTime hasModification)
                hasModification.LastModificationTime = DateTime.Now;

            //if (instance is IModificationAudited modificationAudited)
            //    modificationAudited.LastModifierUserId = _provider.User.Id;
        }

        public void FilterDeletion<TEntity, TPrimaryKey>(TEntity instance) where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            if (instance is IHasDeletionTime hasModification)
                hasModification.DeletionTime = DateTime.Now;

            if (instance is IDeletionAudited deletionAudited)
            {
                //deletionAudited.DeleterUserId = _provider.User.Id;
                deletionAudited.IsDeleted = true;
            }
        }
    }
}
