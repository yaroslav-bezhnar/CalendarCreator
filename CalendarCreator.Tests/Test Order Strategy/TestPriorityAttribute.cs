using System;

namespace CalendarCreator.Tests
{
    [AttributeUsage( AttributeTargets.Method, AllowMultiple = false )]
    public class TestPriorityAttribute : Attribute
    {
        #region Constructors

        public TestPriorityAttribute( int priority ) => Priority = priority;

        #endregion

        #region Properties

        public int Priority
        {
            get;
            private set;
        }

        #endregion
    }
}
