namespace Rikrop.Core.Framework.Monitoring
{
    public class ToStringCounterValueFormatter<T> : ICounterValueFormatter<T>
    {
        public string Format(T value)
        {
            return value.ToString();
        }
    }
}