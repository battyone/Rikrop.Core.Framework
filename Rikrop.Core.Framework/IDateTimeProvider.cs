using System;

namespace Rikrop.Core.Framework
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        DateTime Today { get; }

        /// <summary>
        /// ����������� ����������� ������ ������� � ���������� �� ������� dayStartHour
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="dayStartHour">���, � �������� ���������� ����� ���������� �����</param>
        /// <returns></returns>
        DateRange GetLogicalDateRange(DateTime dateFrom, DateTime dateTo, int dayStartHour);

        /// <summary>
        /// ����������� ���������� ������ ������� � ����������� �� ������� dayStartHour
        /// </summary>
        /// <param name="logicDateFrom"></param>
        /// <param name="logicDateTo"></param>
        /// <param name="dayStartHour">���, � �������� ���������� ����� ���������� �����</param>
        /// <returns></returns>
        DateRange GetFactDateRange(DateTime logicDateFrom, DateTime logicDateTo, int dayStartHour);

        /// <summary>
        /// ������������ ��������
        /// </summary>
        DateRange MaxRange();

        /// <summary>
        /// n-������� ����� �� �������� �������
        /// </summary>
        DateRange GetMonthAgo(int monthCount = -1);
        
        /// <summary>
        /// ������� ����� (� 1 ����� �� ����� ������)
        /// </summary>
        DateRange GetCurrentMonth();
        
        /// <summary>
        /// ������ ����������� ����
        /// </summary>
        DateRange GetToday();
        
        /// <summary>
        /// ������ ��������� ����
        /// </summary>
        DateRange GetYesterday();

        /// <summary>
        /// ������� ������ (��-��)
        /// </summary>
        DateRange GetCurrentWeek();
        
        /// <summary>
        /// ���������� ������ (��-��)
        /// </summary>
        DateRange GetLastWeek();

        /// <summary>
        /// ��� ������ �����, �� �������� ���
        /// </summary>
        DateRange GetTwoWeeksAgo();

        /// <summary>
        /// ���������� �����
        /// </summary>
        DateRange GetLastMonth();
    }
}