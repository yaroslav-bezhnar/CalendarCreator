namespace CalendarCreator.ConsoleApp
{
    internal static class Program
    {
        private static void Main( string[] args )
        {
            var app = new ConsoleCalendar();
            app.Start( args );
        }
    }
}
