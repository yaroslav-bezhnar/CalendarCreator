using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CalendarCreator.Tests
{
    public class OrderPriority : ITestCaseOrderer
    {
        #region ITestCaseOrderer Members

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>( IEnumerable<TTestCase> testCases )
            where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach ( var testCase in testCases )
            {
                var priority = 0;
                foreach ( var attr in testCase.TestMethod.Method.GetCustomAttributes( typeof( TestPriorityAttribute ).AssemblyQualifiedName ) )
                {
                    priority = attr.GetNamedArgument<int>( nameof( TestPriorityAttribute.Priority ) );
                }

                getOrCreate( sortedMethods, priority ).Add( testCase );
            }

            foreach ( var list in sortedMethods.Keys.Select( priority => sortedMethods[priority] ) )
            {
                list.Sort( ( x, y ) => StringComparer.OrdinalIgnoreCase.Compare( x.TestMethod.Method.Name, y.TestMethod.Method.Name ) );

                foreach ( var testCase in list )
                {
                    yield return testCase;
                }
            }
        }

        #endregion

        #region Private Methods

        private static TValue getOrCreate<TKey, TValue>( IDictionary<TKey, TValue> dictionary, TKey key )
            where TValue : new()
        {
            if ( dictionary.TryGetValue( key, out var result ) )
            {
                return result;
            }

            result = new TValue();
            dictionary[key] = result;

            return result;
        }

        #endregion
    }
}
