namespace Rikrop.Core.Framework.Monitoring
{
    public interface ICounterValueCalculator<out T>
    {
        T CalculateValue();
        T CalculateValueAndReset();
        T GetLastCalculatedValue();
    }
}