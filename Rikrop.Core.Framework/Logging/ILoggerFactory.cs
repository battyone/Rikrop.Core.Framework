using System.Diagnostics.Contracts;
using Rikrop.Core.Framework.Logging.Contracts;

namespace Rikrop.Core.Framework.Logging
{
    [ContractClass(typeof(ContractILoggerFactory))]
    public interface ILoggerFactory
    {
        ILogger CreateForSource(string logSource);
    }

    namespace Contracts
    {
        [ContractClassFor(typeof(ILoggerFactory))]
        public abstract class ContractILoggerFactory : ILoggerFactory
        {
            public ILogger CreateForSource(string logSource)
            {
                Contract.Ensures(Contract.Result<ILogger>() != null);
                return default(ILogger);
            }
        }
    }
}