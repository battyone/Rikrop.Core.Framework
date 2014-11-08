using Rikrop.Core.Framework.Logging;

namespace ConsoleApplication1
{
    /// <summary>
    /// Логгер, который декарирует другой объект <see cref="ILogger"/>, оборачивая вызов метода Log в try/catch и проглатывая все исключения.
    /// </summary>
    public class SafeLogger : ILogger
    {
        private readonly ILogger _logger;

        public SafeLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Log<TRecord>(TRecord record) where TRecord : ILogRecord
        {
            try
            {
                _logger.Log(record);
            }
            catch
            {
            }
        }
    }
}