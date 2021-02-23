using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class ScrollStrategyExtensions
	{
		public static global::Xamarin.UITest.ScrollStrategy ToXamarinScrollStrategy(this ScrollStrategy strategy)
			=> (global::Xamarin.UITest.ScrollStrategy)(int)strategy;
	}
}
