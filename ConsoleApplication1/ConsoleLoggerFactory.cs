using System;
using System.Diagnostics.Contracts;
using Rikrop.Core.Framework.Logging;

namespace ConsoleApplication1
{
    public class ConsoleLoggerFactory : ILoggerFactory
    {
        private readonly ILogRecordFormatter _logRecordFormatter;

        public ConsoleLoggerFactory(ILogRecordFormatter logRecordFormatter)
        {
            Contract.Requires<ArgumentNullException>(logRecordFormatter != null);

            _logRecordFormatter = logRecordFormatter;
        }

        public ILogger CreateForSource(string logSource)
        {
            if (string.IsNullOrWhiteSpace(logSource))
            {
                return new ConsoleLogger(_logRecordFormatter);
            }
            return new ConsoleLogger(new DecoratedLogRecordFormatter(logSource, _logRecordFormatter));
        }
    }
}