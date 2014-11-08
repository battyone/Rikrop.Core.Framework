using System;
using NUnit.Framework;
using Telerik.JustMock;

namespace Rikrop.Core.Framework.Test
{
    [TestFixture]
    public class DateTimeProviderTest
    {
        private DateTimeProvider CreateDateTimeProvider(DateTime now)
        {
            var fakeTodayNowProvider = CreateFakeTodayNowProvider(now);
            return new DateTimeProvider(fakeTodayNowProvider);
        }

        private ITodayNowProvider CreateFakeTodayNowProvider(DateTime now)
        {
            var todayNowProvider = Mock.Create<ITodayNowProvider>();
            Mock.Arrange(() => todayNowProvider.Today).Returns(now.Date);
            Mock.Arrange(() => todayNowProvider.Now).Returns(now);
            return todayNowProvider;
        }

        [Test]
        public void Get_current_month()
        {
            var expectedRange = new DateRange(new DateTime(2012, 1, 1), new DateTime(2012, 2, 1));
            var provider = CreateDateTimeProvider(new DateTime(2012, 1, 13));

            Assert.AreEqual(expectedRange, provider.GetCurrentMonth());
        }

        [Test]
        public void Get_current_week()
        {
            var expectedRange = new DateRange(new DateTime(2012, 1, 2), new DateTime(2012, 1, 9));
            var provider = CreateDateTimeProvider(new DateTime(2012, 1, 4));

            Assert.AreEqual(expectedRange, provider.GetCurrentWeek());
        }

        [Test]
        public void Get_last_month()
        {
            var expectedRange = new DateRange(new DateTime(2011, 12, 1), new DateTime(2012, 1, 1));
            var provider = CreateDateTimeProvider(new DateTime(2012, 1, 4));

            Assert.AreEqual(expectedRange, provider.GetLastMonth());
        }

        [Test]
        public void Get_last_week()
        {
            var expectedRange = new DateRange(new DateTime(2011, 12, 26), new DateTime(2012, 1, 2));
            var provider = CreateDateTimeProvider(new DateTime(2012, 1, 4));

            Assert.AreEqual(expectedRange, provider.GetLastWeek());
        }

        [Test]
        public void Get_max_range()
        {
            var expectedRange = new DateRange(DateTime.MinValue, DateTime.MaxValue);
            var provider = CreateDateTimeProvider(new DateTime(2012, 1, 3));

            Assert.AreEqual(expectedRange, provider.MaxRange());
        }

        [Test]
        public void Get_month_ago()
        {
            var expectedRange = new DateRange(new DateTime(2011, 12, 3), new DateTime(2012, 1, 3));
            var provider = CreateDateTimeProvider(new DateTime(2012, 1, 3));

            Assert.AreEqual(expectedRange, provider.GetMonthAgo());
        }

        [Test]
        public void Get_today()
        {
            var expectedRange = new DateRange(new DateTime(2012, 1, 3), new DateTime(2012, 1, 4));
            var provider = CreateDateTimeProvider(new DateTime(2012, 1, 3));

            Assert.AreEqual(expectedRange, provider.GetToday());
        }

        [Test]
        public void Get_two_weeks_ago()
        {
            var expectedRange = new DateRange(new DateTime(2011, 12, 20), new DateTime(2012, 1, 3));
            var provider = CreateDateTimeProvider(new DateTime(2012, 1, 3));

            Assert.AreEqual(expectedRange, provider.GetTwoWeeksAgo());
        }

        [Test]
        public void Get_yesterday()
        {
            var expectedRange = new DateRange(new DateTime(2012, 1, 2), new DateTime(2012, 1, 3));
            var provider = CreateDateTimeProvider(new DateTime(2012, 1, 3));

            Assert.AreEqual(expectedRange, provider.GetYesterday());
        }

        [Test]
        public void Now_must_be_returned_from_today_now_provider()
        {
            var expectedNow = new DateTime(2012, 1, 1, 14, 53, 41);
            var todayNowProvider = Mock.Create<ITodayNowProvider>();
            Mock.Arrange(() => todayNowProvider.Now).Returns(expectedNow).MustBeCalled();

            var provider = new DateTimeProvider(todayNowProvider);

            Assert.AreEqual(expectedNow, provider.Now);
            Mock.Assert(todayNowProvider);
        }

        [Test]
        public void Today_must_be_returned_from_today_now_provider()
        {
            var expectedToday = new DateTime(2012, 1, 1);
            var todayNowProvider = Mock.Create<ITodayNowProvider>();
            Mock.Arrange(() => todayNowProvider.Today).Returns(expectedToday).MustBeCalled();

            var provider = new DateTimeProvider(todayNowProvider);

            Assert.AreEqual(expectedToday, provider.Today);
            Mock.Assert(todayNowProvider);
        }
    }
}