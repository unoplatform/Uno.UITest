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
            => App.Query(q => query.Unwrap(q).InvokeGeneric("browser:Uno.UI.WindowManager.current|GetDependencyPropertyValue", dependencyPropertyName).Value<T>()).First();

        /// <summary>
        /// Gets the DependencyProperty value of the DependencyObject return by the query.
        /// </summary>
        public static object GetDependencyPropertyValue(this QueryEx query, string dependencyPropertyName) 
            => App.Query(q => query.Unwrap(q).InvokeGeneric("browser:Uno.UI.WindowManager.current|GetDependencyPropertyValue", dependencyPropertyName)).FirstOrDefault();

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

		/// <summary>
		/// Waits for a specific value on an element's dependency property
		/// </summary>
		public static void WaitForDependencyPropertyValue<T>(
			this IApp app,
			System.Func<IAppQuery, IAppQuery> element,
			string dependencyPropertyName,
			T expectedValue)
			=> app.WaitFor(() =>
			{
				var values = app.Query(q => element(q).GetDependencyPropertyValue(dependencyPropertyName).Value<T>());

				if(values.Length == 1)
				{
					return object.Equals(values[0], expectedValue);
				}
				else
				{
					return false;
				}
			}, timeoutMessage: $"Failed to find [{expectedValue}] on [{dependencyPropertyName}]"
		);
	}
}
