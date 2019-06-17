using System;
using NUnit.Framework;
using Uno.UITest;
using Uno.UITest.Selenium;

namespace Sample.UITests
{
	public class TestBase
	{
		protected IApp App { get; private set; }

		[SetUp]
		public void StartBrowser()
		{
			App = ConfigureApp.WebAssembly
				.Uri(new Uri(GetEnvironmentVariable("UNO_UITEST_TARGETURI", Constants.DefaultUri)))
				.ChromeDriverLocation(GetEnvironmentVariable("UNO_UITEST_DRIVERPATH_CHROME", Constants.ChromeDriver))
				.ScreenShotsPath(GetEnvironmentVariable("UNO_UITEST_SCREENSHOT_PATH", TestContext.CurrentContext.TestDirectory))
#if DEBUG
				.Headless(false)
#endif
				.StartApp();

			Uno.UITest.Helpers.Queries.Helpers.App = App;
		}

		[TearDown]
		public void CloseBrowser()
		{
			App.Dispose();
		}

		private string GetEnvironmentVariable(string variableName, string defaultValue)
		{
			var value = Environment.GetEnvironmentVariable(variableName);
			return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
		}
	}
}
