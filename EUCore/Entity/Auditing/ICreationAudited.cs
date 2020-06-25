using Newtonsoft.Json;

namespace EUCore.Entity.Auditing
{
    public interface ICreationAudited : IHasCreationTime
    {
        [JsonIgnore]
        long? CreatorUserId { get; set; }
    }
    public interface ICreationAudited<TPrimaryKey, TUser> : ICreationAudited
        where TUser : IEntity<TPrimaryKey>
    {
        [JsonIgnore]
        TUser CreatorUser { get; set; }
    }
}