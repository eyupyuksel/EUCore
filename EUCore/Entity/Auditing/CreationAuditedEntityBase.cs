using System;

namespace EUCore.Entity.Auditing
{
    public abstract class CreationAuditedEntityBase<TPrimaryKey> : EntityBase<TPrimaryKey>, ICreationAudited
    {

        public virtual DateTime CreationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        
    }
    public abstract class CreationAuditWithDeletionEntityBase<TPrimaryKey> : CreationAuditedEntityBase<TPrimaryKey>, IDeletionAudited
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
    }
    public abstract class CreationAuditedEntityBase<TPrimaryKey, TUser> : CreationAuditedEntityBase<TPrimaryKey>, ICreationAudited<TPrimaryKey,TUser>
        where TUser : IEntity<TPrimaryKey>
    {
        public virtual TUser CreatorUser { get; set; }
    }
}