using System;
using System.Linq;
using System.Reflection;

namespace Rikrop.Core.Framework
{
    public static class TypeExtensions
    {
        public static T GetCustomAttribute<T>(this Type type)
            where T : Attribute
        {
            var customAttributes = type.GetCustomAttributes(typeof(T), true);
            var customAttribute = customAttributes.SingleOrDefault() as T;
            return customAttribute;
        }

        public static T GetCustomAttribute<T>(this MethodInfo methodInfo)
            where T : Attribute
        {
            var customAttributes = methodInfo.GetCustomAttributes(typeof(T), true);
            var customAttribute = customAttributes.SingleOrDefault() as T;
            return customAttribute;
        }
    }
}
