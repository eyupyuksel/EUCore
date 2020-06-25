using System.Collections.Generic;
using System.Linq;

namespace EUCore.Extension
{
    public static class DictionaryUtility
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            dict.TryGetValue(key, out var result);
            return result;
        }
    }
    public static class AnonymousTypeUtility
    {
        public static T ToAnonymousType<T, TValue>(this IDictionary<string, TValue> dict, T anonymousPrototype)
        {
            var ctor = anonymousPrototype.GetType().GetConstructors().Single();

            var args = from p in ctor.GetParameters()
                let val = dict.GetValueOrDefault(p.Name)
                select val != null && p.ParameterType.IsInstanceOfType(val) ? (object)val : null;

            return (T)ctor.Invoke(args.ToArray());
        }
    }
}
