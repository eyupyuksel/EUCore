using EUCore.Entity.Auditing;

namespace EUCore.Entity.Notification
{
    public interface INotificationEntity : INotificationEntity<int, int, int>
    {
        
    }
    public interface INotificationEntity<TPrimaryKey, TUserKey, TReferenceKey> : IEntity<TPrimaryKey>, ICreationAudited
    {
        TReferenceKey ReferenceId { get; set; }
        TUserKey UserId { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        string Code { get; set; }
        bool IsRead { get; set; }
    }
}
