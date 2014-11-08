using System;
using System.Diagnostics.Contracts;

namespace Rikrop.Core.Framework.Logging
{
    public static class LoggerFactoryExtensions
    {
        public static ILogger Create(this ILoggerFactory factory)
        {
            Contract.Requires<ArgumentNullException>(factory != null);
            return factory.CreateForSource(logSource: null);
        }
    }
}