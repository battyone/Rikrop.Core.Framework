using System;
using System.Diagnostics.Contracts;

namespace Rikrop.Core.Framework.Monitoring
{
    public class SuccessFaultPairCounter<TCounter>
        where TCounter : class, ICounter
    {
        private readonly TCounter _successCounter;
        private readonly TCounter _faultCounter;

        public TCounter Success
        {
            get { return _successCounter; }
        }

        public TCounter Fault
        {
            get { return _faultCounter; }
        }

        public SuccessFaultPairCounter(TCounter successCounter, TCounter faultCounter)
        {
            Contract.Requires<ArgumentNullException>(successCounter != null);
            Contract.Requires<ArgumentNullException>(faultCounter != null);

            _successCounter = successCounter;
            _faultCounter = faultCounter;
        }
    }

    public class SuccessFaultPairCounter<TCounter, TValue>
        where TCounter : class, ICounter<TValue>
    {
        private readonly TCounter _successCounter;
        private readonly TCounter _faultCounter;

        public TCounter Success
        {
            get { return _successCounter; }
        }

        public TCounter Fault
        {
            get { return _faultCounter; }
        }

        public SuccessFaultPairCounter(TCounter successCounter, TCounter faultCounter)
        {
            Contract.Requires<ArgumentNullException>(successCounter != null);
            Contract.Requires<ArgumentNullException>(faultCounter != null);

            _successCounter = successCounter;
            _faultCounter = faultCounter;
        }
    }
}