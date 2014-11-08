using System;

namespace Rikrop.Core.Framework
{
    public static class GenericExtensions
    {
        /// <summary>
        ///   Конвертирует Nullable значение при помощи заданных методов, предоставляя методы для случаев когда есть значение и когда его нет
        /// </summary>
        /// <param name="value"> Исходное значение </param>
        /// <param name="onHasValue"> Конвертор для NotNull значения </param>
        /// <param name="onHasNotValue"> Конвертор для Null значения </param>
        /// <returns> Сконвертированное значение </returns>
        public static TRes ConvertTo<TArg, TRes>(this TArg? value, Func<TArg, TRes> onHasValue, Func<TRes> onHasNotValue = null) where TArg : struct
        {
            if (value.HasValue)
            {
                return onHasValue(value.Value);
            }

            return onHasNotValue != null
                       ? onHasNotValue()
                       : default(TRes);
        }

        /// <summary>
        ///   Конвертирует Nullable значение при помощи заданных методов, предоставляя методы для случаев когда есть значение и когда его нет
        /// </summary>
        /// <param name="value"> Исходное значение </param>
        /// <param name="onHasValue"> Конвертор для NotNull значения </param>
        /// <param name="onHasNotValue"> Результат для Null значения </param>
        /// <returns> Сконвертированное значение </returns>
        public static TRes ConvertTo<TArg, TRes>(this TArg? value, Func<TArg, TRes> onHasValue, TRes onHasNotValue) where TArg : struct
        {
            return value.ConvertTo(onHasValue, () => onHasNotValue);
        }

        public static TRes ConvertTo<TArg, TRes>(this TArg value, Func<TArg, TRes> onHasValue, Func<TRes> onHasNotValue = null) where TArg : class
        {
            if (value != null)
            {
                return onHasValue(value);
            }

            return onHasNotValue != null
                       ? onHasNotValue()
                       : default(TRes);
        }

        public static TRes ConvertTo<TArg, TRes>(this TArg value, Func<TArg, TRes> onHasValue, TRes onHasNotValue) where TArg : class
        {
            return value.ConvertTo(onHasValue, () => onHasNotValue);
        }

        public static bool IsNullable(this Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static DateTime? ToDate(this DateTime? dateTime)
        {
            return dateTime.ConvertTo(a => a.Date);
        }
    }
}