using Newtonsoft.Json;

namespace EUCore.Entity.Auditing
{
    public interface IModificationAudited : IHasModificationTime
    {
        [JsonIgnore]
        long? LastModifierUserId { get; set; }
    }

    public interface IModificationAudited<TPrimaryKey, TUser> : IModificationAudited
        where TUser : IEntity<TPrimaryKey>
    {
        [JsonIgnore]
        TUser LastModifierUser { get; set; }
    }
}