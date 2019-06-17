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
using Uno.UITest.Helpers;

namespace Sample.UITests
{
	public class SimpleFixture : TestBase
	{
		[Test]
		public void BasicTest()
		{
			IAppQuery num4Button(IAppQuery q) => q.Marked("num4Button");
			IAppQuery num2Button(IAppQuery q) => q.Marked("num2Button");
			IAppQuery normalOutput(IAppQuery q) => q.Marked("NormalOutput");

			var num4 = App.WaitForElement(num4Button);
			var num2 = App.WaitForElement(num2Button);

			App.Tap(num4Button);
			App.Tap(num2Button);

			var output = App.WaitForElement(normalOutput);
			Assert.AreEqual("42", output.First().Text);

			var screenshot = App.Screenshot("Test");
			Assert.AreEqual(Path.Combine(TestContext.CurrentContext.TestDirectory, "Test.png"), screenshot.FullName);
		}

		[TearDown]
		public void CloseBrowser()
		{
			App.Dispose();
		}
	}
}
