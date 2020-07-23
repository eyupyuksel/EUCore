using System;
using DapperExtensions.Sql;
using Dommel;
using EUCore.Repositories.Dapper.Dommel;

namespace EUCore.Configuration
{
    public class RuntimeInitializer
    {
        
        public RuntimeInitializer SetDialect<TDialect>() where TDialect : SqlDialectBase, new()
        {
            DapperExtensions.DapperExtensions.SqlDialect = new TDialect();
            return this;
        }
        public RuntimeInitializer DefaultMapper(Type type)
        {
            DapperExtensions.DapperExtensions.DefaultMapper = type;
            DommelMapper.SetTableNameResolver(new TableNameResolver());
            DommelMapper.SetKeyPropertyResolver(new KeyPropertyResolver());
            DommelMapper.SetColumnNameResolver(new ColumnNameResolver());
            return this;
        }
    }
}
//1.50.5