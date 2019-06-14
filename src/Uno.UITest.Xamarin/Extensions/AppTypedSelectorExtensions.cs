using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class AppTypedSelectorExtensions
	{
		public static IAppTypedSelector<T> AsUnoAppTypedSelector<T>(this AppTypedSelector<T> selector)
			=> new XamarinAppTypedSelector<T>(selector);

		public static AppTypedSelector<T> ToXamarinAppTypedSelector<T>(this IAppTypedSelector<T> selector)
			=> selector is XamarinAppTypedSelector<T> ts ? ts.Source : throw new InvalidOperationException();
	}
}
