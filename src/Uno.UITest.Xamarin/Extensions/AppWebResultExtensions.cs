using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class AppWebResultExtensions
	{
		public static IAppWebResult ToUnoAppWebResult(this AppWebResult result)
			=> new XamarinAppWebResult(result);
	}
}
