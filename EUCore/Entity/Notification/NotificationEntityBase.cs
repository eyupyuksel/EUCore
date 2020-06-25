using EUCore.Entity.Auditing;

namespace EUCore.Entity.Notification
{
#pragma warning disable S2436 // Classes and methods should not have too many generic parameters
    public abstract class NotificationEntityBase<TPrimaryKey, TUserKey, TReferenceKey> : CreationAuditedEntityBase<TPrimaryKey>, INotificationEntity<TPrimaryKey, TUserKey, TReferenceKey>
#pragma warning restore S2436 // Classes and methods should not have too many generic parameters
    {
        public TReferenceKey ReferenceId { get; set; }
        public TUserKey UserId { get; set; }
        public string Body { get; set; }
        public string Code { get; set; }
        public bool IsRead { get; set; }
        public string Subject { get; set; }
    }
}
