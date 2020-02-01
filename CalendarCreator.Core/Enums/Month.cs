using CalendarCreator.Core.Attributes;

namespace CalendarCreator.Core
{
    /// <summary>
    ///     Specifies the month of the year.
    /// </summary>
    public enum Month
    {
        /// <summary>
        ///     Represents a January.
        /// </summary>
        [ShortName("Jan")]
        January = 1,

        /// <summary>
        ///     Represents a February.
        /// </summary>
        [ShortName("Feb")]
        February = 2,

        /// <summary>
        ///     Represents a March.
        /// </summary>
        [ShortName("Mar")]
        March = 3,

        /// <summary>
        ///     Represents a April.
        /// </summary>
        [ShortName("Apr")]
        April = 4,

        /// <summary>
        ///     Represents a May.
        /// </summary>
        [ShortName("May")]
        May = 5,

        /// <summary>
        ///     Represents a June.
        /// </summary>
        [ShortName("Jun")]
        June = 6,

        /// <summary>
        ///     Represents a July.
        /// </summary>
        [ShortName("Jul")]
        July = 7,

        /// <summary>
        ///     Represents a August.
        /// </summary>
        [ShortName("Aug")]
        August = 8,

        /// <summary>
        ///     Represents a September.
        /// </summary>
        [ShortName("Sep")]
        September = 9,

        /// <summary>
        ///     Represents a October.
        /// </summary>
        [ShortName("Oct")]
        October = 10,

        /// <summary>
        ///     Represents a November.
        /// </summary>
        [ShortName("Nov")]
        November = 11,

        /// <summary>
        ///     Represents a December.
        /// </summary>
        [ShortName("Dec")]
        December = 12
    }
}
