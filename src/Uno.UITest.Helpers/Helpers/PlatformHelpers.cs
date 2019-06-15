using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uno.UITest.Helpers
{
	public static class PlatformHelpers
	{
		public static void On(Action iOS, Action Android)
		{
			switch (Queries.Helpers.Platform)
			{
				case global::Xamarin.UITest.Platform.Android:
					Android();
					break;
				case global::Xamarin.UITest.Platform.iOS:
					iOS();
					break;
				default: throw new ArgumentOutOfRangeException();
			}
		}

		public static T On<T>(Func<T> iOS, Func<T> Android)
		{
			switch (Queries.Helpers.Platform)
			{
				case global::Xamarin.UITest.Platform.Android:
					return Android();
				case global::Xamarin.UITest.Platform.iOS:
					return iOS();
				default: throw new ArgumentOutOfRangeException();
			}
		}
	}
}
