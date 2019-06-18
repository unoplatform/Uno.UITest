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
				.Uri(new Uri(Constants.DefaultUri))
				.ChromeDriverLocation(Constants.ChromeDriver)
				.ScreenShotsPath(TestContext.CurrentContext.TestDirectory)
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
	}
}
