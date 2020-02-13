using CalendarCreator.Core;
using System.Collections.Generic;
using Xunit;
using DayOfWeek = CalendarCreator.Core.DayOfWeek;

namespace CalendarCreator.Tests
{
    [TestCaseOrderer("CalendarCreator.Tests.OrderPriority", "CalendarCreator.Tests")]
    public class CalendarTests : IClassFixture<CalendarTestsFixture>
    {
        private readonly Calendar _calendar;

        public CalendarTests(CalendarTestsFixture fixture)
        {
            _calendar = fixture.Calendar;
        }

        [Fact, TestPriority(10)]
        public void CalendarYear_IsRight()
        {
            Assert.Equal(2008, _calendar.Year);
        }

        [Fact, TestPriority(20)]
        public void CalendarInitializesSuccessfully_ShouldReturnTrue()
        {
            Assert.NotNull(_calendar);
        }

        [Theory]
        [TestPriority(30)]
        [InlineData(1808)]
        [InlineData(1904)]
        [InlineData(2000)]
        [InlineData(2012)]
        [InlineData(2128)]
        public void LeapYear_ShouldReturnTrue(int year)
        {
            Assert.True(Calendar.IsLeapYear(year));
        }

        [Theory]
        [TestPriority(40)]
        [InlineData(1800)]
        [InlineData(1991)]
        [InlineData(2027)]
        [InlineData(2100)]
        [InlineData(2345)]
        public void LeapYear_ShouldReturnFalse(int year)
        {
            Assert.False(Calendar.IsLeapYear(year));
        }

        [Theory]
        [TestPriority(50)]
        [InlineData(DayOfWeek.Sunday, System.DayOfWeek.Sunday)]
        [InlineData(DayOfWeek.Monday, System.DayOfWeek.Monday)]
        [InlineData(DayOfWeek.Tuesday, System.DayOfWeek.Tuesday)]
        [InlineData(DayOfWeek.Wednesday, System.DayOfWeek.Wednesday)]
        [InlineData(DayOfWeek.Thursday, System.DayOfWeek.Thursday)]
        [InlineData(DayOfWeek.Friday, System.DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Saturday, System.DayOfWeek.Saturday)]
        public void CalendarTransformDays_ShouldReturnTrue(DayOfWeek actualDay, System.DayOfWeek expectedDay)
        {
            Assert.Equal(actualDay, Calendar.Transform(expectedDay));
        }

        [Theory]
        [TestPriority(51)]
        [InlineData(DayOfWeek.Monday, System.DayOfWeek.Sunday)]
        [InlineData(DayOfWeek.Saturday, System.DayOfWeek.Wednesday)]
        [InlineData(DayOfWeek.Wednesday, System.DayOfWeek.Saturday)]
        public void CalendarTransformDays_ShouldReturnFalse(DayOfWeek actualDay, System.DayOfWeek expectedDay)
        {
            Assert.NotEqual(actualDay, Calendar.Transform(expectedDay));
        }

        [Fact]
        [TestPriority(60)]
        public void CalendarNotCreated_ShouldReturnFalse()
        {
            var isCalendarCreated = _calendar.IsCalendarCreated;

            Assert.False(isCalendarCreated);
        }

        [Fact]
        [TestPriority(70)]
        public void CalendarNotCreated_ShouldReturnTrue()
        {
            Assert.Empty(_calendar.GetAnnualCalendar());
        }

        [Fact]
        [TestPriority(80)]
        public void MonthCalendarNotAvailable_ShouldThrow()
        {
            Assert.Throws<KeyNotFoundException>(delegate { _calendar.GetMonthCalendar(Month.April); });
        }

        [Fact]
        [TestPriority(90)]
        public void CalendarNotCreated_ShouldReturnOnlyYear()
        {
            var annualCalendar = _calendar.GetAnnualCalendarAsString();
            annualCalendar = annualCalendar.Trim();

            Assert.Equal("2008", annualCalendar);
        }

        [Fact]
        [TestPriority(100)]
        public void CalendarCreated_ShouldReturnTrue()
        {
            _calendar.Create();

            var isCalendarCreated = _calendar.IsCalendarCreated;

            Assert.True(isCalendarCreated);
        }

        [Fact]
        [TestPriority(110)]
        public void Calendar_CheckAugust24Day()
        {
            var august24 = 24;

            var augustCalendar = _calendar.GetMonthCalendar(Month.August);
            var day = augustCalendar[august24 - 1];

            Assert.Equal(DayOfWeek.Sunday, day);

            augustCalendar = _calendar.GetAnnualCalendar()[Month.August];
            day = augustCalendar[august24 - 1];

            Assert.Equal(DayOfWeek.Sunday, day);
        }
    }
}
