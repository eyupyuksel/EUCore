using System;

namespace EUCore.Entity.Auditing
{
    public abstract class AuditedPassivableEntityBase<TPrimaryKey> : AuditedEntityBase<TPrimaryKey>, IPassivable
    {
        public virtual bool IsActive { get; set; }
    }
    public abstract class AuditedEntityBase<TPrimaryKey> : CreationAuditedEntityBase<TPrimaryKey>, IAudited
    {
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
    }
    public abstract class AuditedEntityBase<TPrimaryKey, TUser> : AuditedEntityBase<TPrimaryKey>, IAudited<TPrimaryKey, TUser>
        where TUser : IEntity<TPrimaryKey>
    {
        public virtual TUser CreatorUser { get; set; }
        public virtual TUser LastModifierUser { get; set; }
    }
}