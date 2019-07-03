using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uno.UITest.Helpers.Queries;

namespace Sample.UITests
{
	public class Constants
	{
		public const string DefaultUri = "http://localhost:51669/";
		public const string ChromeDriver = @"C:\s\ChromeDriver\74.0.3729.6";
		public readonly static string iOSAppName;
		public readonly static string AndroidAppName = "uno.platform.uitestsample";

		public readonly static Platform CurrentPlatform = Platform.Android;
	}
}
