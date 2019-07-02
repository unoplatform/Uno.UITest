using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class XamarinAppExtensions
	{
		public static IApp ToUnoApp(this global::Xamarin.UITest.iOS.iOSApp app)
		{
			return new XamarinApp(app);
		}
		public static IApp ToUnoApp(this global::Xamarin.UITest.Android.AndroidApp app)
		{
			return new XamarinApp(app);
		}
	}
}
