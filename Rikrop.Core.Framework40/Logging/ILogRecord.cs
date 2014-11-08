namespace Rikrop.Core.Framework.Logging
{
    public interface ILogRecord
    {
        LogRecordLevel LogLevel { get; }
        string Message { get; }
        LogRecordDataValue[] DataValues { get; }
        bool HasDataValues { get; }
    }
}