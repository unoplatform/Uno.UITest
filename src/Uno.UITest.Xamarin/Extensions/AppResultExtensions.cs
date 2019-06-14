using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class AppResultExtensions
	{
		public static IAppResult ToUnoAppResult(this AppResult result)
			=> new XamarinAppResult(result);
	}
}
