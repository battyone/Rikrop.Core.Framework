using System;
using System.Diagnostics.Contracts;

namespace Rikrop.Core.Framework.Exceptions
{
    [ContractClass(typeof(ExceptionHandlerContract))]
    public interface IExceptionHandler
    {
        bool Handle(Exception exception);
    }

    [ContractClassFor(typeof(IExceptionHandler))]
    internal abstract class ExceptionHandlerContract : IExceptionHandler
    {
        public bool Handle(Exception exception)
        {
            Contract.Requires<ArgumentNullException>(exception != null);

            return default(bool);
        }
    }
}