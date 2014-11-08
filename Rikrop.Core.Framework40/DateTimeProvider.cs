using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;

namespace Rikrop.Core.Framework
{
    public class DateTimeProvider : IDateTimeProvider
    {
        private const int DaysInAWeek = 7;
        private readonly ITodayNowProvider _todayNowProvider;

        public DateTime Now
        {
            get { return _todayNowProvider.Now; }
        }

        public DateTime UtcNow
        {
            get { return _todayNowProvider.UtcNow; }
        }

        public DateTime Today
        {
            get { return _todayNowProvider.Today; }
        }

        public DateTimeProvider()
            :this(new TodayNowProvider())
        {

        }

        public DateTimeProvider([NotNull] ITodayNowProvider todayNowProvider)
        {
            Contract.Requires<ArgumentNullException>(todayNowProvider != null);
            _todayNowProvider = todayNowProvider;
        }

        public DateRange GetLogicalDateRange(DateTime dateFrom, DateTime dateTo, int dayStartHour)
        {
            dateFrom = dateFrom.Date.AddHours(dayStartHour);
            dateTo = dateTo.Date.AddDays(1).AddHours(dayStartHour);
            return new DateRange(dateFrom, dateTo);
        }

        public DateRange GetFactDateRange(DateTime logicDateFrom, DateTime logicDateTo, int dayStartHour)
        {
            logicDateFrom = logicDateFrom.AddHours(-dayStartHour);
            logicDateTo = logicDateTo.AddHours(-dayStartHour).AddSeconds(-1);
            return new DateRange(logicDateFrom, logicDateTo);
        }

        public DateRange MaxRange()
        {
            return new DateRange(DateTime.MinValue, DateTime.MaxValue);
        }

        public DateRange GetMonthAgo(int monthCount = -1)
        {
            var today = Today;
            return new DateRange(today.AddMonths(monthCount), today);
        }

        public DateRange GetCurrentMonth()
        {
            var today = Today;

            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1);

            return new DateRange(startDate, endDate);
        }

        public DateRange GetToday()
        {
            var today = Today;
            return new DateRange(today, today.AddDays(1));
        }

        public DateRange GetYesterday()
        {
            var today = Today;
            return new DateRange(today.AddDays(-1), today);
        }

        public DateRange GetCurrentWeek()
        {
            var fdocw = GetFirstDayOfWeek(Today);
            return new DateRange(fdocw, fdocw.AddDays(DaysInAWeek));
        }


        public DateRange GetLastWeek()
        {
            var start = GetFirstDayOfWeek(Today.AddDays(-DaysInAWeek));
            var end = start.AddDays(DaysInAWeek);
            return new DateRange(start, end);
        }

        public DateRange GetTwoWeeksAgo()
        {
            var start = Today.AddDays(-DaysInAWeek*2);
            var end = start.AddDays(DaysInAWeek*2);
            return new DateRange(start, end);
        }

        public DateRange GetLastMonth()
        {
            var today = Today;
            var end = new DateTime(today.Year, today.Month, 1);
            var start = end.AddMonths(-1);
            return new DateRange(start, end);
        }


        private DateTime GetFirstDayOfWeek(DateTime current)
        {
            const DayOfWeek fdow = DayOfWeek.Monday;
            var offset = current.DayOfWeek - DayOfWeek.Monday < 0
                             ? DaysInAWeek
                             : 0;
            var numberOfDaysSinceBeginningOfTheWeek = current.DayOfWeek + offset - fdow;

            return current.AddDays(-numberOfDaysSinceBeginningOfTheWeek);
        }
    }
}