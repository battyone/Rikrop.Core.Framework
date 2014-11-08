using System;

namespace Rikrop.Core.Framework
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        DateTime Today { get; }

        /// <summary>
        /// преобразует фактический период времени в логический со сдвигом dayStartHour
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="dayStartHour">час, с которого начинаютс€ новые логические сутки</param>
        /// <returns></returns>
        DateRange GetLogicalDateRange(DateTime dateFrom, DateTime dateTo, int dayStartHour);

        /// <summary>
        /// преобразует логический период времени в фактический со сдвигом dayStartHour
        /// </summary>
        /// <param name="logicDateFrom"></param>
        /// <param name="logicDateTo"></param>
        /// <param name="dayStartHour">час, с которого начинаютс€ новые логические сутки</param>
        /// <returns></returns>
        DateRange GetFactDateRange(DateTime logicDateFrom, DateTime logicDateTo, int dayStartHour);

        /// <summary>
        /// ћаксимальный диапазон
        /// </summary>
        DateRange MaxRange();

        /// <summary>
        /// n-мес€цев назад от текущего момента
        /// </summary>
        DateRange GetMonthAgo(int monthCount = -1);
        
        /// <summary>
        /// “екущий мес€ц (с 1 числа до конца мес€ца)
        /// </summary>
        DateRange GetCurrentMonth();
        
        /// <summary>
        /// ѕолный сегодн€шний день
        /// </summary>
        DateRange GetToday();
        
        /// <summary>
        /// ѕолный вчерашний день
        /// </summary>
        DateRange GetYesterday();

        /// <summary>
        /// “екуща€ недел€ (пн-вс)
        /// </summary>
        DateRange GetCurrentWeek();
        
        /// <summary>
        /// ѕредыдуща€ недел€ (пн-вс)
        /// </summary>
        DateRange GetLastWeek();

        /// <summary>
        /// ƒве недели назад, от текущего дн€
        /// </summary>
        DateRange GetTwoWeeksAgo();

        /// <summary>
        /// ѕредыдущий мес€ц
        /// </summary>
        DateRange GetLastMonth();
    }
}