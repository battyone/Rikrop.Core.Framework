using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Rikrop.Core.Framework.Logging
{
    public struct ExceptionLogRecord : ILogRecord
    {
        private readonly string _message;
        private readonly LogRecordLevel _logLevel;
        private readonly Exception _exception;
        private readonly LogRecordDataValue[] _additionalDataValues;
        private LogRecordDataValue[] _dataValues;

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
            get
            {
                if (_dataValues == null)
                {
                    var additionalDataLength = _additionalDataValues == null
                                        ? 0
                                        : _additionalDataValues.Length;
                    _dataValues = new LogRecordDataValue[additionalDataLength + 1];

                    if (additionalDataLength > 0 && _additionalDataValues != null)
                    {
                        Array.Copy(_additionalDataValues, _dataValues, _additionalDataValues.Length);
                    }

                    var exceptionDataValue = CreateDataValue(_exception);
                    _dataValues[additionalDataLength] = exceptionDataValue;
                }
                return _dataValues;
            }
        }

        public bool HasDataValues
        {
            get { return true; }
        }

        public ExceptionLogRecord(string message, LogRecordLevel logLevel, Exception exception, LogRecordDataValue[] dataValues)
        {
            Contract.Requires<ArgumentNullException>(exception != null);

            _message = message;
            _logLevel = logLevel;
            _exception = exception;
            _additionalDataValues = dataValues;
            _dataValues = null;
        }

        private static LogRecordDataValue CreateDataValue(Exception exception)
        {
            if (exception.InnerException == null)
            {
                return LogRecordDataValue.CreateException(exception.GetType().ToString(),
                                                          exception.Message,
                                                          LogRecordDataValue.CreateSimple("ExceptionSource", exception.Source),
                                                          LogRecordDataValue.CreateStackTrace("ExceptionStackTrace", exception.StackTrace));
            }

            var inner = exception;
            var exceptions = new List<Exception>();
            do
            {
                exceptions.Add(inner);
                inner = inner.InnerException;
            } while (inner != null);


            var lastException = exceptions[exceptions.Count - 1];
            var lastDataValue = LogRecordDataValue.CreateException(lastException.GetType().ToString(),
                                                                   lastException.Message,
                                                                   LogRecordDataValue.CreateSimple("ExceptionSource", lastException.Source),
                                                                   LogRecordDataValue.CreateStackTrace("ExceptionStackTrace", lastException.StackTrace));
            for (int i = exceptions.Count - 2; i >= 0; i--)
            {
                var ex = exceptions[i];
                var dataValue = LogRecordDataValue.CreateException(ex.GetType().ToString(),
                                                                   ex.Message,
                                                                   LogRecordDataValue.CreateSimple("ExceptionSource", ex.Source),
                                                                   LogRecordDataValue.CreateStackTrace("ExceptionStackTrace", ex.StackTrace),
                                                                   lastDataValue);
                lastDataValue = dataValue;
            }

            return lastDataValue;
        }
    }
}