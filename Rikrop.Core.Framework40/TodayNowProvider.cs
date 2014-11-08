using System;

namespace Rikrop.Core.Framework
{
    public class TodayNowProvider : ITodayNowProvider
    {
        public DateTime Now { get { return DateTime.Now; } }
        public DateTime UtcNow { get { return DateTime.UtcNow; } }
        public DateTime Today { get { return DateTime.Today; } }
    }
}