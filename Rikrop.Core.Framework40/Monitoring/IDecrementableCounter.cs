namespace Rikrop.Core.Framework.Monitoring
{
    public interface IDecrementableCounter : ICounter
    {
        void Decrement();
    }

    public interface IDecrementableCounter<in T> : ICounter<T>
    {
        void DecrementBy(T item);
    }
}