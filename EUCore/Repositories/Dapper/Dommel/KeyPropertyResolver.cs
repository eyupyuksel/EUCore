using System;
using System.Linq;
using System.Reflection;
using Dommel;

namespace EUCore.Repositories.Dapper.Dommel
{
    public class KeyPropertyResolver //: DommelMapper.IKeyPropertyResolver
    {
        public PropertyInfo[] ResolveKeyProperties(Type type)
        {
            return type.GetProperties().Where(p => p.Name == "Id").ToArray();
        }

        public PropertyInfo[] ResolveKeyProperties(Type type, out bool isIdentity)
        {
            isIdentity = type.GetProperties().Any(p => p.Name == "Id");
            return type.GetProperties().Where(p => p.Name == "Id").ToArray();
        }
    }
}
