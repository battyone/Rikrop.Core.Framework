namespace Rikrop.Core.Framework.Logging
{
    public interface ILogger
    {
        void Log<TRecord>(TRecord record) where TRecord : ILogRecord;
    }
}