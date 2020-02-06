using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalendarCreator.Core.Attributes;
using CalendarCreator.Core.Helpers;

namespace CalendarCreator.Core
{
    /// <summary>
    ///     Represents a class to create a calendar.
    /// </summary>
    public class Calendar
    {
        #region Static Fields

        /// <summary>
        ///     Two new lines.
        /// </summary>
        private static readonly string _twoNewLines = $"{Environment.NewLine}{Environment.NewLine}";

        #endregion

        #region Fields

        /// <summary>
        ///     Calendar dictionary.
        /// </summary>
        private readonly IDictionary<Month, IList<DayOfWeek>> _calendarDictionary;

        /// <summary>
        ///     The ordered days of the week.
        /// </summary>
        private readonly IList<DayOfWeek> _orderedDaysOfWeek;

        /// <summary>
        ///     The first day of the week.
        /// </summary>
        private DayOfWeek _firstDayOfWeek;

        /// <summary>
        ///     The last day of the week.
        /// </summary>
        private DayOfWeek _lastDayOfWeek;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Calendar" /> class
        /// </summary>
        /// <param name="year">Year of the calendar.</param>
        /// <param name="firstDayOfYear">First day of the year.</param>
        /// <param name="firstDayOfWeek">First day of the week.</param>
        public Calendar( int year, DayOfWeek firstDayOfYear, DayOfWeek firstDayOfWeek = DayOfWeek.Sunday )
        {
            Year = year;
            FirstDayOfYear = firstDayOfYear;
            FirstDayOfWeek = firstDayOfWeek;

            _orderedDaysOfWeek = getOrderedDaysOfWeek();

            _calendarDictionary = new Dictionary<Month, IList<DayOfWeek>>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The year of the calendar.
        /// </summary>
        public int Year
        {
            get;
        }

        /// <summary>
        ///     The first day of the year.
        /// </summary>
        public DayOfWeek FirstDayOfYear
        {
            get;
        }

        /// <summary>
        ///     The first day of the week.
        /// </summary>
        public DayOfWeek FirstDayOfWeek
        {
            get => _firstDayOfWeek;
            private set
            {
                _firstDayOfWeek = value;
                _lastDayOfWeek = getLastDayOfWeek();
            }
        }

        /// <summary>
        ///     'True' if the calendar is ready (created); otherwise 'False'.
        /// </summary>
        public bool IsCalendarCreated => _calendarDictionary.Any();

        #endregion

        #region Public Methods

        /// <summary>
        ///     Create the calendar.
        /// </summary>
        public void Create()
        {
            produceCalendar();
        }

        /// <summary>
        ///     Gets annual calendar.
        /// </summary>
        /// <returns>Dictionary of <see cref="Month" /> as key and <see cref="IList{DayOfWeek}" /> as values.</returns>
        public IDictionary<Month, IList<DayOfWeek>> GetAnnualCalendar() => _calendarDictionary;

        /// <summary>
        ///     Gets a string representation of the annual calendar.
        /// </summary>
        /// <returns>The string representation of the annual calendar.</returns>
        public string GetAnnualCalendarAsString() =>
            string.Concat( $"            {Year}{_twoNewLines}",
                          string.Join( $"{_twoNewLines}",
                                      _calendarDictionary.Keys.Select( GetMonthCalendarAsString ) ) );

        /// <summary>
        ///     Gets calendar for the month.
        /// </summary>
        /// <param name="month">The month for which get a calendar.</param>
        /// <returns>List of the <see cref="DayOfWeek" /></returns>
        public IList<DayOfWeek> GetMonthCalendar( Month month ) => _calendarDictionary[month];

        /// <summary>
        ///     Gets a string representation of the calendar for the month.
        /// </summary>
        /// <param name="month">The month for which get a calendar.</param>
        /// <returns>The string representation of the calendar for the month.</returns>
        public string GetMonthCalendarAsString( Month month )
        {
            var shortDayNames = getShortDayNames();
            var days = string.Join( "   ", shortDayNames );

            var monthCalendar = new StringBuilder();
            monthCalendar.AppendLine( $"        {month}" );
            monthCalendar.AppendLine( days );

            var currentDay = _calendarDictionary[month].First();
            var currentDayInt = _orderedDaysOfWeek.IndexOf( currentDay );
            currentDayInt += 1;

            for ( var number = 1; number <= _calendarDictionary[month].Count; number++ )
            {
                while ( currentDayInt != 1 && number != currentDayInt )
                {
                    monthCalendar.Append( "      " );
                    currentDayInt--;
                }

                monthCalendar.Append( string.Format( number <= 9 ? "  {0}   " : "  {0}  ", number ) );

                currentDay = getNextDayOfWeek( currentDay );
                if ( currentDay == _lastDayOfWeek )
                {
                    monthCalendar.AppendLine();
                }
            }

            return monthCalendar.ToString().TrimEnd();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Creates the calendar.
        /// </summary>
        private void produceCalendar()
        {
            var currentDay = FirstDayOfYear;

            foreach ( var (month, daysInMonth) in getMonthsWithDays( Year ) )
            {
                var monthCalendar = new List<DayOfWeek>();

                for ( var day = 1; day <= daysInMonth; day++ )
                {
                    monthCalendar.Add( currentDay );
                    currentDay = getNextDayOfWeek( currentDay );
                }

                _calendarDictionary.Add( month, monthCalendar );
            }
        }

        /// <summary>
        ///     Gets a list of <see cref="DayOfWeek" /> ordered by the first day of the week.
        /// </summary>
        /// <returns>The list of <see cref="DayOfWeek" /> ordered by the first day of the week.</returns>
        private IList<DayOfWeek> getOrderedDaysOfWeek()
        {
            var defaultDaysOfWeek = Enum.GetValues( typeof( DayOfWeek ) ).Cast<DayOfWeek>().ToList();
            var orderedDays = defaultDaysOfWeek.FindAll( day => day >= _firstDayOfWeek );

            orderedDays.AddRange( defaultDaysOfWeek.Except( orderedDays ) );

            return orderedDays;
        }

        /// <summary>
        ///     Gets short names of the days of week.
        /// </summary>
        /// <returns>Short names of the days of week.</returns>
        private IEnumerable<string> getShortDayNames() =>
            _orderedDaysOfWeek.Select( day => day.GetEnumAttribute<ShortNameAttribute>()?.ShortName );

        /// <summary>
        ///     Gets the next day of the week.
        /// </summary>
        /// <param name="currentDay">Current day of the week.</param>
        /// <returns>Next day of the week.</returns>
        private DayOfWeek getNextDayOfWeek( DayOfWeek currentDay )
        {
            var currentIndex = _orderedDaysOfWeek.IndexOf( currentDay );
            var nextIndex = currentIndex + 1;

            if ( nextIndex > _orderedDaysOfWeek.Count - 1 )
            {
                nextIndex = 0;
            }

            return _orderedDaysOfWeek[nextIndex];
        }

        /// <summary>
        ///     Gets months with the number of days they have.
        /// </summary>
        /// <param name="year">The year to get months with the number of days.</param>
        /// <returns>Dictionary of the <see cref="Month" /> as key and the number of days as value.</returns>
        private static IDictionary<Month, int> getMonthsWithDays( int year ) =>
            new Dictionary<Month, int>
            {
                { Month.January, 31 },
                { Month.February, isLeapYear( year ) ? 29 : 28 },
                { Month.March, 31 },
                { Month.April, 30 },
                { Month.May, 31 },
                { Month.June, 30 },
                { Month.July, 31 },
                { Month.August, 31 },
                { Month.September, 30 },
                { Month.October, 31 },
                { Month.November, 30 },
                { Month.December, 31 }
            };

        /// <summary>
        ///     Gets the last day of the week.
        /// </summary>
        /// <returns>Last day of the week.</returns>
        private DayOfWeek getLastDayOfWeek()
        {
            var lastDay = (int) _firstDayOfWeek;
            return lastDay == 0 ? DayOfWeek.Saturday : (DayOfWeek) lastDay;
        }

        /// <summary>
        ///     Checks whether a given year is a leap year.
        /// </summary>
        /// <param name="year">The year to check.</param>
        /// <returns>'True' if year is a leap year; otherwise 'False'.</returns>
        private static bool isLeapYear( int year )
        {
            if ( year < 1 || year > 9999 )
                throw new ArgumentOutOfRangeException( nameof( year ) );

            if ( year % 4 != 0 )
                return false;

            if ( year % 100 == 0 )
                return year % 400 == 0;

            return true;
        }

        #endregion
    }
}