namespace Rikrop.Core.Framework.Monitoring
{
    public interface ICounter
    {
        void Increment();
    }

    public interface ICounter<in T>
    {
        void IncrementBy(T item);
    }
}