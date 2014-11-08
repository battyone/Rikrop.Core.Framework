using System;

namespace Rikrop.Core.Framework
{
    public interface ITimer
    {
        bool IsEnabled { get; set; }
        TimeSpan Interval { get; set; }

        void Start();
        void Stop();

        event EventHandler Tick;
    }
}