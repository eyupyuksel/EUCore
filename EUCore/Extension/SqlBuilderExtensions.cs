using System;
using Dapper;

namespace EUCore.Extension
{
    public static class SqlBuilderExtensions
    {
        public static SqlBuilder WhereIfNotNull<TParameterType>(this SqlBuilder builder, TParameterType? param, string sql, dynamic parameters = null) where TParameterType : struct
        {
            if (param.HasValue)
            {
                if ((param is int || param is long) && param.Value.Equals(0))
                    return builder;

                return builder.Where(sql, parameters: parameters);
            }
            return builder;
        }
        public static SqlBuilder WhereIfNotNull<TParameterType>(this SqlBuilder builder, TParameterType param, string sql, dynamic parameters = null)
        {
            if (param == null || (param as Array)?.Length == 0)
                return builder;
            return builder.Where(sql, parameters: parameters);
        }
        public static SqlBuilder OrWhereIfNotNull<TParameterType>(this SqlBuilder builder, TParameterType? param, string sql, dynamic parameters = null) where TParameterType : struct
        {
            if (param.HasValue)
            {
                if ((param is int || param is long) && param.Value.Equals(0))
                    return builder;

                return builder.OrWhere(sql, parameters: parameters);
            }
            return builder;
        }
        public static SqlBuilder OrWhereIfNotNull<TParameterType>(this SqlBuilder builder, TParameterType param, string sql, dynamic parameters = null)
        {
            if (param == null)
                return builder;
            var type = typeof(TParameterType);
            if (type == typeof(String) && !string.IsNullOrEmpty(param.ToString()))
                return builder.OrWhere(sql, parameters: parameters);
            if (type == typeof(int) && !param.Equals(0))
                return builder.OrWhere(sql, parameters: parameters);
            return builder.Where(sql, parameters: parameters);
        }
        public static SqlBuilder WhereNotDeleted(this SqlBuilder builder, string columnName)
        {
            return builder.Where($"{columnName} = @IsDeleted", new { IsDeleted = false });
        }
        public static SqlBuilder WhereActive(this SqlBuilder builder, string columnName)
        {
            return builder.Where($"{columnName} = @IsActive", new { IsActive = true });
        }
    }
}
