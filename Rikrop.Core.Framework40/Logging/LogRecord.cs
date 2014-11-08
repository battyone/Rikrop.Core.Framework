namespace Rikrop.Core.Framework.Logging
{
    public struct LogRecord : ILogRecord
    {
        private readonly string _message;
        private readonly LogRecordLevel _logLevel;
        private readonly LogRecordDataValue[] _dataValues;

        public LogRecordLevel LogLevel
        {
            get { return _logLevel; }
        }

        public string Message
        {
            get { return _message; }
        }

        public LogRecordDataValue[] DataValues
        {
            get { return _dataValues; }
        }

        public bool HasDataValues
        {
            get { return _dataValues != null && _dataValues.Length > 0; }
        }

        public LogRecord(string message, LogRecordLevel logLevel, LogRecordDataValue[] dataValues)
        {
            _message = message;
            _logLevel = logLevel;
            _dataValues = dataValues;
        }

        public static LogRecord CreateError(string message, params LogRecordDataValue[] dataValues)
        {
            return new LogRecord(message, LogRecordLevel.Error, dataValues);
        }

        public static LogRecord CreateWarning(string message, params LogRecordDataValue[] dataValues)
        {
            return new LogRecord(message, LogRecordLevel.Warning, dataValues);
        }

        public static LogRecord CreateInfo(string message, params LogRecordDataValue[] dataValues)
        {
            return new LogRecord(message, LogRecordLevel.Info, dataValues);
        }
    }
}