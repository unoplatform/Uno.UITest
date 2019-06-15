using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using Uno.UITest;
using Uno.UITest.Puppeteer;

namespace Sample.UITests
{
	public class SimpleFixture
	{
		IApp _app;

		[SetUp]
		public void StartBrowser()
		{
			_app = ConfigureApp.WebAssembly
				.Uri(new Uri("http://calculator-wasm-staging.azurewebsites.net/"))
				.ChromeDriverLocation(@"C:\s\ChromeDriver\74.0.3729.6")
				.ScreenShotsPath(TestContext.CurrentContext.TestDirectory)
				.StartApp();
		}

		[Test]
		public async Task BasicTest()
		{
			IAppQuery num4Button(IAppQuery q) => q.Marked("num4Button");
			IAppQuery num2Button(IAppQuery q) => q.Marked("num2Button");
			IAppQuery normalOutput(IAppQuery q) => q.Marked("NormalOutput");

			var num4 = _app.WaitForElement(num4Button);
			var num2 = _app.WaitForElement(num2Button);

			_app.Tap(num4Button);
			_app.Tap(num2Button);

			var output = _app.WaitForElement(normalOutput);
			Assert.AreEqual("42", output.First().Text);

			var screenshot = _app.Screenshot("Test");
			Assert.AreEqual(Path.Combine(TestContext.CurrentContext.TestDirectory, "Test.png"), screenshot.FullName);
		}

		[TearDown]
		public void CloseBrowser()
		{
			_app.Dispose();
		}
	}
}
