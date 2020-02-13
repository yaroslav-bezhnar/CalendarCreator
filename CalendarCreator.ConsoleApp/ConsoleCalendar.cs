using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using CalendarCreator.Core;
using DayOfWeek = CalendarCreator.Core.DayOfWeek;

namespace CalendarCreator.ConsoleApp
{
    public class ConsoleCalendar
    {
        #region Static Fields

        private const string PRINT_ONLY_MONTH_ARGUMENT = "-m";
        private const string TEXT_ERROR = "[Error!]";
        private const string TEXT_WARNING = "[Warning!]";

        #endregion

        #region Fields

        private readonly List<DateTime> _calendarsToPrint = new List<DateTime>();

        private bool _printOnlySelectedMonth;

        #endregion

        #region Public Methods

        public void Start( string[] args )
        {
            printWelcomeText();

            initialize( args );

            _calendarsToPrint.ForEach( printCalendar );
        }

        #endregion

        #region Private Methods

        private void initialize( string[] args )
        {
            if ( args == null || !args.Any() )
                args = readArgsFromCommandLine();

            _printOnlySelectedMonth = args.Contains( PRINT_ONLY_MONTH_ARGUMENT, StringComparer.OrdinalIgnoreCase );

            var argsList = new List<string>( args );
            argsList.ForEach( convertArgumentToDateTime );
        }

        private static string[] readArgsFromCommandLine()
        {
            Console.WriteLine( "Select action:" );
            Console.WriteLine( "\t1 - use current date" );
            Console.WriteLine( "\t2 - enter date(s)" );

            var key = Console.ReadKey( true );

            switch ( key.KeyChar )
            {
                case '1':
                    return new[] { DateTime.Now.ToShortDateString() };

                case '2':
                    Console.Write( "Input text: " );
                    var line = Console.ReadLine();
                    return commandLineToArgs( line );

                default:
                    Console.WriteLine( $"{TEXT_ERROR} Incorrect command selected." );
                    return new string[0];
            }
        }

        private void convertArgumentToDateTime( string argument )
        {
            if ( DateTime.TryParse( argument, out var date ) )
                _calendarsToPrint.Add( date );
            else if ( !argument.Equals( PRINT_ONLY_MONTH_ARGUMENT, StringComparison.OrdinalIgnoreCase ) )
                Console.WriteLine( $"{TEXT_WARNING} Wrong date format: '{argument}'" );
        }

        private void printCalendar( DateTime date )
        {
            var year = date.Year;
            var firstDayOfYear = Calendar.Transform( new DateTime( year, 1, 1 ).DayOfWeek );

            var calendar = new Calendar( year, firstDayOfYear, DayOfWeek.Monday );
            calendar.Create();

            if ( !calendar.IsCalendarCreated ) return;

            if ( !_printOnlySelectedMonth )
            {
                var annualCalendarStr = calendar.GetAnnualCalendarAsString();
                Console.WriteLine( annualCalendarStr );
            }

            Console.WriteLine();
            interactivePrint( " - - - Calendar for selected month - - - ", 80, true );

            var currentMonthCalendarStr = calendar.GetMonthCalendarAsString( (Month) date.Month );
            Console.WriteLine( currentMonthCalendarStr );
        }

        private static void printWelcomeText()
        {
            interactivePrint( $"Hi {Environment.UserName}!", 120 );
            interactivePrint( "Welcome to the Calendar Creator Console App :)", 200, true );
            interactivePrint( "Let's start . . .", 100, true );

            Console.WriteLine();
        }

        private static void interactivePrint( string text, int millisecondsTimeout, bool writeByWords = false )
        {
            var values = writeByWords
                             ? text.Split( ' ' ).Select( _ => (object) $"{_} " ).ToArray()
                             : text.Select( _ => (object) _ );

            void sleep() => Thread.Sleep( millisecondsTimeout );

            foreach ( var s in values )
            {
                Console.Write( s );
                sleep();
            }

            Console.WriteLine();
        }

        #endregion

        #region DLL Imports

        [DllImport( "shell32.dll", SetLastError = true )]
        private static extern IntPtr CommandLineToArgvW( [MarshalAs( UnmanagedType.LPWStr )] string lpCmdLine, out int pNumArgs );

        private static string[] commandLineToArgs( string commandLine )
        {
            if ( string.IsNullOrWhiteSpace( commandLine ) ) return new string[0];

            var argv = CommandLineToArgvW( commandLine, out var argc );
            if ( argv == IntPtr.Zero ) throw new Win32Exception();

            try
            {
                var args = new string[argc];
                for ( var i = 0; i < args.Length; i++ )
                {
                    var p = Marshal.ReadIntPtr( argv, i * IntPtr.Size );
                    args[i] = Marshal.PtrToStringUni( p );
                }

                return args;
            }
            finally
            {
                Marshal.FreeHGlobal( argv );
            }
        }

        #endregion
    }
}
