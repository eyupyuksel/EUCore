using EUCore.Entity;
using EUCore.Serialization;
using Newtonsoft.Json;

namespace EUCore.Extensions
{
    public static class SerializationExtensions
    {
        public static string RemoveJsonAttibutes(this IEntity entity)
        {
            var settings = new JsonSerializerSettings
                {
                    ContractResolver = new IgnoreJsonAttributesResolver(),
                    Formatting = Formatting.Indented
                };
            return JsonConvert.SerializeObject(entity, settings);
        }
    }
}
