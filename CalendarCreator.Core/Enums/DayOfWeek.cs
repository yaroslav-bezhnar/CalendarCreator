using CalendarCreator.Core.Attributes;

namespace CalendarCreator.Core
{
    /// <summary>
    ///     Specifies the day of the week.
    /// </summary>
    public enum DayOfWeek
    {
        /// <summary>
        ///     Represents a Sunday.
        /// </summary>
        [ShortName( "Sun" )]
        Sunday = 1,

        /// <summary>
        ///     Represents a Monday.
        /// </summary>
        [ShortName( "Mon" )]
        Monday = 2,

        /// <summary>
        ///     Represents a Tuesday.
        /// </summary>
        [ShortName( "Tue" )]
        Tuesday = 3,

        /// <summary>
        ///     Represents a Wednesday.
        /// </summary>
        [ShortName( "Wed" )]
        Wednesday = 4,

        /// <summary>
        ///     Represents a Thursday.
        /// </summary>
        [ShortName( "Thu" )]
        Thursday = 5,

        /// <summary>
        ///     Represents a Friday.
        /// </summary>
        [ShortName( "Fri" )]
        Friday = 6,

        /// <summary>
        ///     Represents a Saturday.
        /// </summary>
        [ShortName( "Sat" )]
        Saturday = 7
    }
}
