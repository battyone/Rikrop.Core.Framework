namespace Rikrop.Core.Framework.Logging
{
    public struct LogRecordDataValue
    {
        private readonly string _key;
        private readonly string _type;
        private readonly string _value;
        private readonly LogRecordDataValue[] _values;

        public string Type
        {
            get { return _type; }
        }

        public string Value
        {
            get { return _value; }
        }

        public string Key
        {
            get { return _key; }
        }

        public LogRecordDataValue[] Values
        {
            get { return _values; }
        }

        public bool HasValues
        {
            get { return _values != null && _values.Length > 0; }
        }

        public LogRecordDataValue(string key, string type, string value, LogRecordDataValue[] values)
        {
            _key = key;
            _type = type;
            _value = value;
            _values = values;
        }

        public static LogRecordDataValue CreateSimple(string key, string value, params LogRecordDataValue[] children)
        {
            return new LogRecordDataValue(key, LogRecordDataTypes.Simple, value, children);
        }

        public static LogRecordDataValue CreateLarge(string key, string value, params LogRecordDataValue[] children)
        {
            return new LogRecordDataValue(key, LogRecordDataTypes.Large, value, children);
        }

        public static LogRecordDataValue CreateStackTrace(string key, string value, params LogRecordDataValue[] children)
        {
            return new LogRecordDataValue(key, LogRecordDataTypes.StackTrace, value, children);
        }

        public static LogRecordDataValue CreateException(string exceptionType, string exceptionMessage, params LogRecordDataValue[] children)
        {
            return new LogRecordDataValue(exceptionType, LogRecordDataTypes.Exception, exceptionMessage, children);
        }
    }
}