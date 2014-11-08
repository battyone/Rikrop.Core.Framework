using Rikrop.Core.Framework.Logging;

namespace ConsoleApplication1
{
    /// <summary>
    /// ������, ������� ���������� ������ ������ <see cref="ILogger"/>, ���������� ����� ������ Log � try/catch � ����������� ��� ����������.
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