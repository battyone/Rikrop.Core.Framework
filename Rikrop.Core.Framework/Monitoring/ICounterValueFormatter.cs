using System;
using System.Diagnostics.Contracts;
using Rikrop.Core.Framework.Monitoring.Contracts;

namespace Rikrop.Core.Framework.Monitoring
{
    [ContractClass(typeof(ContractICounterValueFormatter<>))]
    public interface ICounterValueFormatter<in T>
    {
        string Format(T value);
    }

    namespace Contracts
    {
        [ContractClassFor(typeof(ICounterValueFormatter<>))]
        public abstract class ContractICounterValueFormatter<T> : ICounterValueFormatter<T>
        {
            public string Format(T value)
            {
                Contract.Requires<ArgumentNullException>(!Equals(value, null));
                throw new NotSupportedException();
            }
        }
    }
}