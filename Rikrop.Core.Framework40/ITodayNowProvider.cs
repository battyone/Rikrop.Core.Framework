using System;

namespace Rikrop.Core.Framework
{
    public interface ITodayNowProvider
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        DateTime Today { get; }
    }
}