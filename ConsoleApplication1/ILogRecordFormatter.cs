using Rikrop.Core.Framework.Logging;

namespace ConsoleApplication1
{
    public interface ILogRecordFormatter
    {
        string GetString<T>(T record) where T : ILogRecord;
    }
}