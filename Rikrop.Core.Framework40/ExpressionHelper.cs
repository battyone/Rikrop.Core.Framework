using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Rikrop.Core.Framework
{
    public static class ExpressionHelper
    {
        public static string GetName<TObject, TKey>(this Expression<Func<TObject, TKey>> property)
        {
            if (property == null)
            {
                return null;
            }

            return GetName(property.Body);
        }

        public static string GetName<TObject>(this Expression<Func<TObject, object>> property)
        {
            if (property == null)
            {
                return null;
            }

            return GetName(property.Body);
        }

        public static string GetName<TObject>(this Expression<Action<TObject>> property)
        {
            if (property == null)
            {
                return null;
            }

            return GetName(property.Body);
        }

        public static object GetValue<TObject>(this Expression<Action<TObject>> property)
        {
            if (property == null)
                return null;

            return GetValue(property.Body);
        }

        public static object GetValue(this Expression<Action> property)
        {
            if (property == null)
                return null;

            return GetValue(property.Body);
        }

        public static string GetName(this Expression<Func<object, object>> property)
        {
            if (property == null)
            {
                return null;
            }

            return GetName(property.Body);
        }

        public static string GetName(this Expression<Func<object>> property)
        {
            if (property == null)
            {
                return null;
            }

            return GetName(property.Body);
        }

        public static string GetName(this Expression<Action> property)
        {
            if (property == null)
            {
                return null;
            }

            return GetName(property.Body);
        }

        private static string GetName(Expression expression)
        {
            if (expression == null)
                return null;

            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
                return GetName(unaryExpression.Operand);


            var memberExpression = expression as MemberExpression;
            if (memberExpression != null)
                return GetName(memberExpression);

            var methodCallExpression = expression as MethodCallExpression;
            if (methodCallExpression != null)
                return methodCallExpression.Method.Name;

            throw new InvalidOperationException("Unknown Expression type: " + expression.GetType());
        }

        private static string GetName(MemberExpression memberExpression)
        {
            var members = new List<string>();
            do
            {
                members.Add(memberExpression.Member.Name);
                memberExpression = memberExpression.Expression as MemberExpression;
            } while (memberExpression != null);

            members.Reverse();
            return string.Join(".", members);
        }

        private static object GetValue(Expression expression)
        {
            if (expression == null)
                return null;

            var unaryExpression = expression as UnaryExpression;
            if (unaryExpression != null)
                return GetValue(unaryExpression.Operand);

            var methodCallExpression = expression as MethodCallExpression;
            if (methodCallExpression != null)
            {
                var arguments = methodCallExpression.Arguments;
                if (arguments.Count != 0)
                {
                    var memberExression = arguments[0] as MemberExpression;
                    if (memberExression != null)
                    {
                        var constantExpression = memberExression.Expression as ConstantExpression;
                        if (constantExpression != null)
                        {
                            var fields = constantExpression.Value.GetType()
                                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                                .Where(o => !o.Name.EndsWith("this"));
                            return fields.First().GetValue(constantExpression.Value);
                        }

                        throw new InvalidOperationException("Unknown Expression type: " + memberExression.Expression.GetType());
                    }

                    throw new InvalidOperationException("Unknown Expression type: " + arguments[0].GetType());
                }

                throw new InvalidOperationException("Method doesn't have parameters");
            }

            throw new InvalidOperationException("Unknown Expression type: " + expression.GetType());
        }

        public static Expression GreaterThanOrEqual(Expression e1, Expression e2)
        {
            if (e1.Type.IsNullable() && !e2.Type.IsNullable())
                e2 = Expression.Convert(e2, e1.Type);
            else if (!e1.Type.IsNullable() && e2.Type.IsNullable())
                e1 = Expression.Convert(e1, e2.Type);
            return Expression.GreaterThanOrEqual(e1, e2);
        }

        public static Expression LessThanOrEqual(Expression e1, Expression e2)
        {
            if (e1.Type.IsNullable() && !e2.Type.IsNullable())
                e2 = Expression.Convert(e2, e1.Type);
            else if (!e1.Type.IsNullable() && e2.Type.IsNullable())
                e1 = Expression.Convert(e1, e2.Type);
            return Expression.LessThanOrEqual(e1, e2);
        }
    }
}