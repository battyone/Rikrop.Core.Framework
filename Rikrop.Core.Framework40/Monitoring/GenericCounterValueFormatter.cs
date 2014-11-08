using System;
using System.Diagnostics.Contracts;

namespace Rikrop.Core.Framework.Monitoring
{
    public class GenericCounterValueFormatter<T> : ICounterValueFormatter<T>
    {
        private readonly Func<T, string> _formatter;

        public GenericCounterValueFormatter(Func<T, string> formatter)
        {
            Contract.Requires<ArgumentNullException>(formatter != null);

            _formatter = formatter;
        }

        public string Format(T value)
        {
            return _formatter(value);
        }
    }
}