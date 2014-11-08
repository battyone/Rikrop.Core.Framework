using System;

namespace Rikrop.Core.Framework.Logging
{
    public static class LoggerExtensions
    {
        public static void LogWarning(this ILogger logger, string warning)
        {
            logger.Log(LogRecord.CreateWarning(warning, new LogRecordDataValue[0]));
        }

        public static void LogError(this ILogger logger, string error)
        {
            logger.Log(LogRecord.CreateError(error, new LogRecordDataValue[0]));
        }

        public static void LogError(this ILogger logger, string error, Exception exception)
        {
            logger.Log(new ExceptionLogRecord(error, LogRecordLevel.Error, exception, null));
        }

        public static void LogError(this ILogger logger, Exception exception)
        {
            logger.LogError(null, exception);
        }

        public static void LogInfo(this ILogger logger, string info)
        {
            logger.Log(LogRecord.CreateInfo(info, new LogRecordDataValue[0]));
        }

        public static void Log<TLogSource>(this ILogger logger, TLogSource logRecordSource) where TLogSource : struct, ILogRecordSource
        {
            logger.Log(logRecordSource.Convert());
        }
    }
}