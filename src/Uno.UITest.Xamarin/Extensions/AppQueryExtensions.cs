using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class AppQueryExtensions
	{
		public static IAppQuery AsUnoAppQuery(this AppQuery q)
			=> new XamarinAppQuery(q);

		public static AppQuery ToXamarinQuery(this IAppQuery q)
			=> q is XamarinAppQuery xq ? xq.SourceQuery : throw new InvalidOperationException();
	}
}
