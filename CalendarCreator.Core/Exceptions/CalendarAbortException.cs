using System;

namespace CalendarCreator.Core.Exceptions
{
    /// <summary>
    ///     Represents errors that occur when creating a calendar.
    /// </summary>
    public class CalendarAbortException : Exception
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CalendarAbortException" /> class.
        /// </summary>
        public CalendarAbortException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CalendarAbortException" /> class
        ///     with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CalendarAbortException( string message ) : base( message )
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CalendarAbortException" /> class
        ///     with a specified error message and a reference to the inner exception.
        /// </summary>
        /// <param name="message">The message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CalendarAbortException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        #endregion
    }
}