using System;
using System.Diagnostics.Contracts;

namespace Rikrop.Core.Framework.Exceptions
{
    public class BusinessException : Exception
    {
        private readonly object _details;

        public object Details
        {
            get { return _details; }
        }

        public BusinessException(object details)
            : this(details, null)
        {
        }

        public BusinessException(object details, Exception innerException)
            : base(null, innerException)
        {
            Contract.Requires<ArgumentNullException>(details != null);

            _details = details;
        }
    }

    public class BusinessException<TDetails> : BusinessException
    {
        public BusinessException(TDetails details)
            : base(details)
        {
        }

        public BusinessException(TDetails details, Exception innerException)
            : base(details, innerException)
        {
        }
    }
}