using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class AppTypedSelectorExtensions
	{
		public static IAppTypedSelector<T> AsGenericAppTypedSelector<T>(this AppTypedSelector<object> selector)
			=> new XamarinAppTypedSelector<T>(selector);

		public static IAppTypedSelector<string> AsGenericAppTypedSelector(this AppTypedSelector<string> selector)
			=> new XamarinAppTypedSelector<string>(selector.Value<object>());

		public static AppTypedSelector<object> ToXamarinAppTypedSelector<T>(this IAppTypedSelector<T> selector)
		{
			if(selector is XamarinAppTypedSelector<T> tso)
			{
				return tso.Source;
			}
			else if(selector is XamarinAppTypedSelector<string> tss)
			{
				return tss.Source;
			}
			else
			{
				throw new InvalidOperationException();
			}
		}
	}
}
