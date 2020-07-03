using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace EUCore.Extensions
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public static class LamdaExpressions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {

            ParameterExpression p = a.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.Subst[b.Parameters[0]] = p;

            Expression body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {

            ParameterExpression p = a.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.Subst[b.Parameters[0]] = p;

            Expression body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }
        public static string GetMemberName(this LambdaExpression memberSelector)
        {
            string NameSelector(Expression e)
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Parameter:
                        return ((ParameterExpression) e).Name;
                    case ExpressionType.MemberAccess:
                        return ((MemberExpression) e).Member.Name;
                    case ExpressionType.Call:
                        return ((MethodCallExpression) e).Method.Name;
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                        return NameSelector(((UnaryExpression) e).Operand);
                    case ExpressionType.Invoke:
                        return NameSelector(((InvocationExpression) e).Expression);
                    case ExpressionType.ArrayLength:
                        return "Length";
                    default:
                        throw new Exception("EUCore -> MemberSelectorIsNotProperException");
                }
            }

            return NameSelector(memberSelector.Body);
        }

    }
    internal class SubstExpressionVisitor : ExpressionVisitor
    {
        public Dictionary<Expression, Expression> Subst { get; } = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (Subst.TryGetValue(node, out var newValue))
                return newValue;
            return node;
        }
    }
}
