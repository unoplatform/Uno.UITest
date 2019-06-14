using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace Sample.UITests
{
	public class Class1
	{
		RemoteWebDriver driver;
		private Actions _actions;

		[SetUp]
		public void StartBrowser()
		{
			driver = new ChromeDriver(@"C:\s\ChromeDriver\74.0.3729.6");
			_actions = new OpenQA.Selenium.Interactions.Actions(driver);

		}

		[Test]
		public void Test()
		{
			driver.Url = "http://calculator-wasm-staging.azurewebsites.net/";

			var num4 = WaitForXamlElement("num4Button");
			var num2 = WaitForXamlElement("num2Button");

			var r = driver.ExecuteScript("return config.vfs_prefix;");

			_actions
				.Click(num4)
				.Click(num2)
				.Perform();

			var output = WaitForXamlElement("NormalOutput");
			Assert.AreEqual("42", output.Text);
		}

		private IWebElement WaitForXamlElement(string xamlName)
		{
			IWebElement we = null;

			while((we = driver.FindElements(By.XPath($"//*[@xamlname='{xamlName}']")).FirstOrDefault()) == null)
			{
				Thread.Sleep(100);
			}

			return we;
		}

		[TearDown]
		public void CloseBrowser()
		{
			// driver.Close();
		}
	}
}
