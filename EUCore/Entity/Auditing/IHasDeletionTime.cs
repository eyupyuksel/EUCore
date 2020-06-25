using System;
using Newtonsoft.Json;

namespace EUCore.Entity.Auditing
{

    public interface IHasDeletionTime : ISoftDelete
    {
        [JsonIgnore]
        DateTime? DeletionTime { get; set; }
    }
}