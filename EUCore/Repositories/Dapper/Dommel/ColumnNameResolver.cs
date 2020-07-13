using System;
using System.Reflection;
using Dommel;

namespace EUCore.Repositories.Dapper.Dommel
{
    public class ColumnNameResolver //: DommelMapper.IColumnNameResolver
    {
        public string ResolveColumnName(PropertyInfo propertyInfo)
        {
            return $"{propertyInfo.Name}";
        }
    }
}
