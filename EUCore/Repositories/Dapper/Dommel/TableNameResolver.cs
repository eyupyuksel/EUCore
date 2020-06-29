using System;
using Dommel;

namespace EUCore.Repositories.Dapper.Dommel
{
    public class TableNameResolver : DommelMapper.ITableNameResolver
    {
        public string ResolveTableName(Type type)
        {
            return $"{type.Name.Replace("Entity", string.Empty)}";
        }
    }
}
