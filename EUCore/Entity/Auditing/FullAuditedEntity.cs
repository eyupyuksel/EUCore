using System;

namespace EUCore.Entity.Auditing
{
    public abstract class FullAuditedEntityBase<TPrimaryKey> : AuditedPassivableEntityBase<TPrimaryKey>, IFullAudited
    {
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual void Verify()
        {
            if (!IsActive || IsDeleted)
                throw new Exception("EntityNotActiveException");
        }
    }
    public abstract class DeletableAuditedEntityBase<TPrimaryKey> : AuditedEntityBase<TPrimaryKey>, IDeletableAudited
    {
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
    }
    public abstract class FullAuditedEntityBase<TPrimaryKey, TUser> : AuditedEntityBase<TPrimaryKey, TUser>, IFullAudited<TPrimaryKey,TUser>
        where TUser : IEntity<TPrimaryKey>
    {
        public virtual bool IsDeleted { get; set; }
        public virtual TUser DeleterUser { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public bool IsActive { get; set; }
        public void Verify()
        {
            throw new NotImplementedException();
        }
    }
}