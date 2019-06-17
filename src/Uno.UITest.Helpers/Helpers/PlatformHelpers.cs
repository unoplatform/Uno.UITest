using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uno.UITest.Helpers.Queries;

namespace Uno.UITest.Helpers
{
	public static class PlatformHelpers
	{
		public static void On(Action iOS, Action Android, Action Browser = null)
		{
			switch (Queries.Helpers.Platform)
			{
				case Platform.Android:
					Android();
					break;
				case Platform.iOS:
					iOS();
					break;
				case Platform.Browser:
					Browser?.Invoke();
					break;
				default: throw new ArgumentOutOfRangeException();
			}
		}

		public static T On<T>(Func<T> iOS, Func<T> Android, Func<T> Browser = null)
		{
			switch (Queries.Helpers.Platform)
			{
				case Platform.Android:
					return Android();
				case Platform.iOS:
					return iOS();
				case Platform.Browser:
					if(Browser == null)
					{
						throw new InvalidOperationException($"Current platform is Browser but no handler has been provided.");
					}
					return Browser();
				default: throw new ArgumentOutOfRangeException();
			}
		}
	}
}
