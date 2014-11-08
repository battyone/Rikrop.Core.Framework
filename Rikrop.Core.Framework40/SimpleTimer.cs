using System;
using System.Timers;

namespace Rikrop.Core.Framework
{
    public class SimpleTimer : ITimer
    {
        private readonly System.Timers.Timer _realTimer;
        public event EventHandler Tick;

        public bool IsEnabled
        {
            get { return _realTimer.Enabled; }
            set { _realTimer.Enabled = value; }
        }

        public TimeSpan Interval
        {
            get { return TimeSpan.FromMilliseconds(_realTimer.Interval); }
            set { _realTimer.Interval = value.TotalMilliseconds; }
        }

        public bool AutoReset
        {
            get { return _realTimer.AutoReset; }
            set { _realTimer.AutoReset = value; }
        }

        public SimpleTimer()
        {
            _realTimer = new System.Timers.Timer();
            _realTimer.Elapsed += OnRealTimerElapsed;
        }

        public void Start()
        {
            _realTimer.Start();
        }

        public void Stop()
        {
            _realTimer.Stop();
        }

        private void OnRealTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var handler = Tick;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}