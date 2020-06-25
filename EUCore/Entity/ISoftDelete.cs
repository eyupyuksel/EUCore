using Newtonsoft.Json;

namespace EUCore.Entity
{
    public interface ISoftDelete
    {
        [JsonIgnore]
        bool IsDeleted { get; set; }
    }
}
