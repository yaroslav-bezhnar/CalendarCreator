using System;
using CalendarCreator.Core;
using DayOfWeek = CalendarCreator.Core.DayOfWeek;

namespace CalendarCreator.Tests
{
    public class CalendarTestsFixture : IDisposable
    {
        #region Fields

        private readonly DayOfWeek _firstYearDay = DayOfWeek.Tuesday;
        private readonly int _year = 2008;

        #endregion

        #region Constructors

        public CalendarTestsFixture() => Calendar = new Calendar( _year, _firstYearDay );

        #endregion

        #region Properties

        public Calendar Calendar
        {
            get;
            private set;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Calendar = null;
        }

        #endregion
    }
}
