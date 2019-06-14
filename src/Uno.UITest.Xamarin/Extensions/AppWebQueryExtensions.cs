using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class AppWebQueryExtensions
	{
		public static IAppWebQuery ToUnoQuery(this AppWebQuery query)
			=> new XamarinAppWebQuery(query);

		public static AppWebQuery ToXamarinQuery(this IAppWebQuery query)
			=> query is XamarinAppWebQuery xq ? xq.Source : throw new InvalidOperationException();
	}
}
