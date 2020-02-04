using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalendarCreator.Core.Attributes;
using CalendarCreator.Core.Helpers;

namespace CalendarCreator.Core
{
    public class Calendar
    {
        private readonly IDictionary<Month, (int days, string calendar)> _calendarDictionary;

        public Calendar( int year, DayOfWeek firstDayOfYear )
        {
            Year = year;
            FirstDayOfYear = firstDayOfYear;

            _calendarDictionary = new Dictionary<Month, (int days, string calendar)>();

            produceCalendar( year );
        }

        public int Year
        {
            get;
        }

        public DayOfWeek FirstDayOfYear
        {
            get;
        }

        public bool IsReady => _calendarDictionary.Any();

        public string GetAnnualCalendar() => 
            string.Concat( $"            {Year}{Environment.NewLine}",
                          string.Join( $"{Environment.NewLine}{Environment.NewLine}",
                                      _calendarDictionary.Values.Select( el => el.calendar ) ) );

        public string GetMonthCalendar( Month month ) => _calendarDictionary[month].calendar;

        private void produceCalendar( int year )
        {
            var shortDayNames = getShortDayNames();
            var days = string.Join( "   ", shortDayNames );
            var currentDay = FirstDayOfYear;

            foreach ( var (month, daysInMonth) in getMonthsWithDays( year ) )
            {
                var monthCalendar = new StringBuilder();
                monthCalendar.AppendLine( $"        {month}" );
                monthCalendar.AppendLine( days );

                var currentDayInt = (int) currentDay;

                for ( var day = 1; day <= daysInMonth; day++ )
                {
                    while ( currentDayInt != 1 && day != currentDayInt )
                    {
                        monthCalendar.Append( "      " );
                        currentDayInt--;
                    }

                    monthCalendar.Append( string.Format( day <= 9 ? "  {0}   " : "  {0}  ", day ) );

                    if ( currentDay == DayOfWeek.Saturday )
                    {
                        monthCalendar.AppendLine();
                        currentDay = 0;
                    }

                    currentDay++;
                }

                _calendarDictionary.Add( month, ( daysInMonth, monthCalendar.ToString() ) );
            }
        }

        private static IEnumerable<string> getShortDayNames() =>
            Enum.GetValues( typeof( DayOfWeek ) ).Cast<DayOfWeek>()
                .Select( dayOfWeek => dayOfWeek.GetEnumAttribute<ShortNameAttribute>() )
                .Select( attribute => attribute.ShortName ).ToList();

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
    }
}