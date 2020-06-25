using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUCore.Extension
{
    public static class ListExtensions
    {
        public static IEnumerable<T> ListForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            var list = source.ToList();
            list.ForEach(action);
            return list;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> body)
        {
            List<Exception> exceptions = null;
            foreach (var item in source)
            {
                try
                {
                    body(item);
                }
                catch (Exception exc)
                {
                    if (exceptions == null) exceptions = new List<Exception>();
                    exceptions.Add(exc);
                }
            }
            if (exceptions != null)
                throw new AggregateException(exceptions);
        }
        public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body)
        {
            List<Exception> exceptions = null;
            foreach (var item in source)
            {
                try { await body(item); }
                catch (Exception exc)
                {
                    if (exceptions == null) exceptions = new List<Exception>();
                    exceptions.Add(exc);
                }
            }
            if (exceptions != null)
                throw new AggregateException(exceptions);
        }
    }
}
