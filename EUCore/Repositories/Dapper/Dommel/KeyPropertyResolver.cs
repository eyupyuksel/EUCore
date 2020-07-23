using System;
using System.Linq;
using System.Reflection;
using Dommel;

namespace EUCore.Repositories.Dapper.Dommel
{
    public class KeyPropertyResolver : IKeyPropertyResolver
    {
        public ColumnPropertyInfo[] ResolveKeyProperties(Type type)
        {
            var a = new ColumnPropertyInfo[1];
            a[0] = new ColumnPropertyInfo(type.GetProperties().Single(p => p.Name == "Id"));
            return a;
        }
    }
}
/*
        public PropertyInfo[] ResolveKeyProperties(Type type)
        {
            return type.GetProperties().Where(p => p.Name == "Id").ToArray();
        }

        public PropertyInfo[] ResolveKeyProperties(Type type, out bool isIdentity)
        {
            isIdentity = type.GetProperties().Any(p => p.Name == "Id");
            return type.GetProperties().Where(p => p.Name == "Id").ToArray();
        }
 */
