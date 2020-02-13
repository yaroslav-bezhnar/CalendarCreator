using System;
using CalendarCreator.Core;
using DayOfWeek = CalendarCreator.Core.DayOfWeek;

namespace CalendarCreator.Tests
{
    public class CalendarTestsFixture : IDisposable
    {
        private readonly int _year = 2008;
        private readonly DayOfWeek _firstYearDay = DayOfWeek.Tuesday;

        public CalendarTestsFixture()
        {
            Calendar = new Calendar(_year, _firstYearDay);
        }

        public Calendar Calendar
        {
            get;
            private set;
        }

        public void Dispose()
        {
            Calendar = null;
        }
    }
}
