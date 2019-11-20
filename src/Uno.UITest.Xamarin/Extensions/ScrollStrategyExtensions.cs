using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class ScrollStrategyExtensions
	{
		public static global::Xamarin.UITest.ScrollStrategy ToXamarinScrollStrategy(this Uno.UITest.ScrollStrategy unoScrollStrategy)
		{
			switch (unoScrollStrategy)
			{
				case ScrollStrategy.Auto:
					return global::Xamarin.UITest.ScrollStrategy.Auto;
				case ScrollStrategy.Gesture:
					return global::Xamarin.UITest.ScrollStrategy.Gesture;
				case ScrollStrategy.Programmatically:
					return global::Xamarin.UITest.ScrollStrategy.Programmatically;
			}

			throw new ArgumentOutOfRangeException(nameof(unoScrollStrategy));
		}
	}
}
