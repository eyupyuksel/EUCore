using System;
using Newtonsoft.Json;

namespace EUCore.Entity.Auditing
{
    public interface IHasCreationTime
    {
        [JsonIgnore]
        DateTime CreationTime { get; set; }
    }
}