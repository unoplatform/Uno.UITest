using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uno.UITest.Helpers.Queries;

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
			=> app.Invoke(FormatBackdoorMethodName(methodName) + (Queries.Helpers.Platform == Platform.iOS ? ":" : ""), arg1);

		public static string FormatBackdoorMethodName(string methodName)
		{
			return PlatformHelpers.On(
				iOS: () => FormatAsiOSMethodName(methodName),
				Android: () => methodName,
				Browser: () => BuildMethodName(Platform.Browser, methodName)
			);
		}

		public static string FormatAsiOSMethodName(string methodName)
		{
			methodName = BuildMethodName(Platform.iOS, methodName);

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

		private static string BuildMethodName(Platform platform, string methodName)
		{
			var parts = methodName.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

			if(parts.Length == 1)
			{
				return methodName;
			}
			else
			{
				var platforms = parts[0].Split(';');

				var q = from p in platforms
						let pair = p.Split(':')
						select new { Platform = pair[0], Prefix = pair[1] };

				var map = q.ToDictionary(
					p => (Platform)Enum.Parse(typeof(Platform), p.Platform, true),
					p => p.Prefix
				);

				if(map.TryGetValue(platform, out var prefix))
				{
					switch(platform)
					{
						case Platform.Browser:
							return prefix + "." + parts.Last();
					}
				}

				return methodName;
			}
		}
	}
}
