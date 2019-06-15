using System;
using System.Collections.Generic;
using System.Linq;

namespace Uno.UITest.Helpers.Queries
{
	public static partial class QueryExtensions
	{
        /// <summary>
        /// Gets the DependencyProperty value of the DependencyObject return by the query.
        /// </summary>
        public static T GetDependencyPropertyValue<T>(this QueryEx query, string dependencyPropertyName) 
            => App.Query(q => query.Unwrap(q).InvokeGeneric("GetDependencyPropertyValue", dependencyPropertyName).Value<T>()).First();

        /// <summary>
        /// Gets the DependencyProperty value of the DependencyObject return by the query.
        /// </summary>
        public static object GetDependencyPropertyValue(this QueryEx query, string dependencyPropertyName) 
            => App.Query(q => query.Unwrap(q).InvokeGeneric("GetDependencyPropertyValue", dependencyPropertyName)).FirstOrDefault();

        /// <summary>
        /// Waits for a specific value on an element's dependency property
        /// </summary>
        public static void WaitForDependencyPropertyValue(this IApp app, QueryEx element, string dependencyPropertyName, string value)
            => app.WaitFor(() =>
            {
                string v = element.GetDependencyPropertyValue<string>(dependencyPropertyName);
                return value == v;
            }, timeoutMessage: $"Failed to find [{value}] on [{dependencyPropertyName}]"
        );
    }
}
