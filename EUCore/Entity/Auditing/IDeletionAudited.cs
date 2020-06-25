using Newtonsoft.Json;

namespace EUCore.Entity.Auditing
{
    public interface IDeletionAudited : IHasDeletionTime
    {
        [JsonIgnore]
        long? DeleterUserId { get; set; }
    }

    public interface IDeletionAudited<TPrimaryKey, TUser> : IDeletionAudited
        where TUser : IEntity<TPrimaryKey>
    {
        [JsonIgnore]
        TUser DeleterUser { get; set; }
    }
}