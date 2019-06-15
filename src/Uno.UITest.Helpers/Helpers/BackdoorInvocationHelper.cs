using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uno.UITest.Helpers
{
	public static class BackdoorInvocationHelper
	{
		/// <summary>
		/// Invokes a method on the object returned by the query, using the <see cref="IAppQuery.Invoke(string, object)"/> method. 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="methodName"></param>
		/// <param name="arg1"></param>
		/// <returns></returns>
		public static IAppTypedSelector<object> InvokeGeneric(this IAppQuery query, string methodName, object arg1)
			=> query.Invoke(FormatBackdoorMethodName(methodName), arg1);

		/// <summary>
		/// Invokes a method on the main app, using the <see cref="IApp.Invoke(string, object)"/> method. 
		/// The ":" is automatically appended for iOS.
		/// </summary>
		public static object InvokeGeneric(this IApp app, string methodName, object arg1)
			=> app.Invoke(FormatBackdoorMethodName(methodName) + (Queries.Helpers.Platform == global::Xamarin.UITest.Platform.iOS ? ":" : ""), arg1);

		public static string FormatBackdoorMethodName(string methodName)
		{
			return PlatformHelpers.On(iOS: () => FormatAsiOSMethodName(methodName), Android: () => methodName);
		}

		public static string FormatAsiOSMethodName(string methodName)
		{
			if (string.IsNullOrEmpty(methodName))
			{
				return methodName;
			}

			if (!char.IsLower(methodName, 0))
			{
				var sb = new StringBuilder(methodName);
				sb[0] = char.ToLower(sb[0]);
				methodName = sb.ToString();
			}

			return $"{methodName.TrimEnd(':')}";
		}
	}
}
