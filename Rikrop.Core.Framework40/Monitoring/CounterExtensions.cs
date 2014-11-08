namespace Rikrop.Core.Framework.Monitoring
{
    public static class CounterExtensions
    {
        public static void SafeIncrement(this ICounter counter)
        {
            if (counter != null)
            {
                counter.Increment();
            }
        }

        public static void SafeDecrement(this IDecrementableCounter counter)
        {
            if (counter != null)
            {
                counter.Decrement();
            }
        }

        public static void SafeIncrementBy<T>(this ICounter<T> counter, T value)
        {
            if (counter != null)
            {
                counter.IncrementBy(value);
            }
        }

        public static void SafeDecrementBy<T>(this IDecrementableCounter<T> counter, T value)
        {
            if (counter != null)
            {
                counter.DecrementBy(value);
            }
        }

        public static void SafeIncrementSuccess<TCounter>(this SuccessFaultPairCounter<TCounter> counter)
            where TCounter : class, ICounter
        {
            if (counter != null && counter.Success != null)
            {
                counter.Success.Increment();
            }
        }

        public static void SafeIncrementFault<TCounter>(this SuccessFaultPairCounter<TCounter> counter)
            where TCounter : class, ICounter
        {
            if (counter != null && counter.Fault != null)
            {
                counter.Fault.Increment();
            }
        }

        public static void SafeDecrementSuccess<TCounter>(this SuccessFaultPairCounter<TCounter> counter)
            where TCounter : class, IDecrementableCounter
        {
            if (counter != null && counter.Success != null)
            {
                counter.Success.Decrement();
            }
        }

        public static void SafeDecrementFault<TCounter>(this SuccessFaultPairCounter<TCounter> counter)
            where TCounter : class, IDecrementableCounter
        {
            if (counter != null && counter.Fault != null)
            {
                counter.Fault.Decrement();
            }
        }

        public static void SafeIncrementSuccessBy<TCounter, TValue>(this SuccessFaultPairCounter<TCounter, TValue> counter, TValue value)
            where TCounter : class, ICounter<TValue>
        {
            if (counter != null && counter.Success != null)
            {
                counter.Success.IncrementBy(value);
            }
        }

        public static void SafeIncrementFaultBy<TCounter, TValue>(this SuccessFaultPairCounter<TCounter, TValue> counter, TValue value)
            where TCounter : class, ICounter<TValue>
        {
            if (counter != null && counter.Fault != null)
            {
                counter.Fault.IncrementBy(value);
            }
        }

        public static void SafeDecrementSuccessBy<TCounter, TValue>(this SuccessFaultPairCounter<TCounter, TValue> counter, TValue value)
            where TCounter : class, IDecrementableCounter<TValue>
        {
            if (counter != null && counter.Success != null)
            {
                counter.Success.DecrementBy(value);
            }
        }

        public static void SafeDecrementFaultBy<TCounter, TValue>(this SuccessFaultPairCounter<TCounter, TValue> counter, TValue value)
            where TCounter : class, IDecrementableCounter<TValue>
        {
            if (counter != null && counter.Fault != null)
            {
                counter.Fault.DecrementBy(value);
            }
        }
    }
}