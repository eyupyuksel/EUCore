using System;
using Newtonsoft.Json;

namespace EUCore.Entity.Auditing
{
    public interface IHasModificationTime
    {
        [JsonIgnore]
        DateTime? LastModificationTime { get; set; }
    }
}