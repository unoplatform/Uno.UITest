using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace Uno.UITest.Selenium
{
	public class SeleniumAppConfigurator
	{
		internal const string UNO_UITEST_DRIVER_PATH = "UNO_UITEST_DRIVER_PATH";

		private string _browser;
		private Uri SiteUri { get; set; }
		private string InternalScreenShotsPath { get; set; } = "";
		private string InternalDriverPath { get; set; }
		private string InternalBrowserPath { get; set; }
		private bool InternalHeadless { get; set; } = true;
		private int InternalWindowWidth { get; set; } = 1024;
		private int InternalWindowHeight { get; set; } = 768;
		private List<string> InternalSeleniumArgument = new List<string>();
		private bool InternalDetectDockerEnvironment = true;

		#region Fluent declaration
		public SeleniumAppConfigurator UsingBrowser(string browser)
		{
			_browser = browser;
			return this;
		}

		public SeleniumAppConfigurator Uri(Uri uri)
		{
			SiteUri = uri;
			return this;
		}

		public SeleniumAppConfigurator ScreenShotsPath(string path)
		{
			InternalScreenShotsPath = path;
			return this;
		}

		public SeleniumAppConfigurator BrowserBinaryPath(string path)
		{
			InternalBrowserPath = path;
			return this;
		}

		public SeleniumAppConfigurator BrowserPath(string path)
		{
			InternalBrowserPath = path;
			return this;
		}

		public SeleniumAppConfigurator DriverPath(string driverPath)
		{
			InternalDriverPath = driverPath;
			return this;
		}

		/// <summary>
		/// This parameters allows to provide a set of additional parameters to be provided to the WebDriver.
		/// </summary>
		public SeleniumAppConfigurator SeleniumArgument(string argument)
		{
			InternalSeleniumArgument.Add(argument);
			return this;
		}


		/// <summary>
		/// Enables the detection of the docker environment to configure the WebDriver accordingly. Enabled by default.
		/// </summary>
		public SeleniumAppConfigurator DetectDockerEnvironment(bool enabled)
		{
			InternalDetectDockerEnvironment = enabled;
			return this;
		}

		/// <summary>
		/// Runs the browser as headless. Defaults to true.
		/// </summary>
		/// <param name="isHeadless"></param>
		/// <returns></returns>
		public SeleniumAppConfigurator Headless(bool isHeadless)
		{
			InternalHeadless = isHeadless;
			return this;
		}

		/// <summary>
		/// Sets the window size. Defaults to 1024x768
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public SeleniumAppConfigurator WindowSize(int width, int height)
		{
			InternalWindowWidth = width;
			InternalWindowHeight = height;
			return this;
		}
		#endregion

		public IApp StartApp()
		{
			var targetUri = GetEnvironmentVariable("UNO_UITEST_TARGETURI", SiteUri.OriginalString);
			var screenShotPath = GetEnvironmentVariable("UNO_UITEST_SCREENSHOT_PATH", InternalScreenShotsPath);
			var browser = GetEnvironmentVariable("UNO_UITEST_BROWSER", _browser);
			var browserPath = GetEnvironmentVariable("UNO_UITEST_BROWSER_PATH", InternalBrowserPath);
			var driverPath = GetEnvironmentVariable(UNO_UITEST_DRIVER_PATH, InternalDriverPath);

			RemoteWebDriver driver;
			switch(browser?.ToUpperInvariant())
			{
				case "EDGE":
					driver = GetEdgeDriver(browserPath, driverPath);
					break;

				case "CHROME":
					driver = GetChromeDriver(browserPath, driverPath);
					break;

				default:
					if(browserPath?.Contains("edge") ?? driverPath?.Contains("edge") ?? false)
					{
						driver = GetEdgeDriver(browserPath, driverPath);
					}
					else
					{
						driver = GetChromeDriver(browserPath, driverPath);
					}
					break;
			}
			driver.Url = targetUri;

			return new SeleniumApp(driver, screenShotPath);
		}
		
		protected RemoteWebDriver GetChromeDriver(string browserPath = null, string driverPath = null)
		{
			// For backward compatibility, we give priority to the "CHROME" specific env. variables
			driverPath = GetEnvironmentVariable("UNO_UITEST_DRIVERPATH_CHROME", driverPath);
			browserPath = GetEnvironmentVariable("UNO_UITEST_CHROME_BINARY_PATH", browserPath);

			var options = new ChromeOptions();
			ApplyOptions(options, browserPath);

			var driver = string.IsNullOrEmpty(driverPath)
				? SeleniumDriverManager.Chrome.FromChromePath(browserPath, options)
				: SeleniumDriverManager.Chrome.FromDriverPath(driverPath, options);

			return driver;
		}

		protected RemoteWebDriver GetEdgeDriver(string browserPath = null, string driverPath = null)
		{
			var options = new EdgeOptions();
			ApplyOptions(options, browserPath);

			var driver = string.IsNullOrEmpty(driverPath)
				? SeleniumDriverManager.Edge.FromEdgePath(browserPath, options)
				: SeleniumDriverManager.Edge.FromDriverPath(driverPath, options);

			return driver;
		}

		private void ApplyOptions(ChromiumOptions options, string browserPath)
		{
			if(InternalHeadless)
			{
				options.AddArguments("--no-sandbox");
				options.AddArgument("headless");
			}

			options.AddArgument($"window-size={InternalWindowWidth}x{InternalWindowHeight}");

			if(InternalDetectDockerEnvironment)
			{
				if(File.Exists("/.dockerenv"))
				{
					// When running under docker, ports bindings may not work properly
					// as the current local host may not be detected properly by the web driver
					// causing errors like this one:
					//
					// [SEVERE]: bind() returned an error, errno=99: Cannot assign requested address (99)
					//
					// When InternalDetectDockerEnvironment is set, tell the daemon to listen on
					// all available interfaces
					Console.WriteLine($"Container mode enabled, adding whitelisted-ips");
					options.AddArguments("--whitelisted-ips");
				}
			}

			foreach(var arg in InternalSeleniumArgument)
			{
				options.AddArguments(arg);
			}

			if(!string.IsNullOrEmpty(browserPath))
			{
				options.BinaryLocation = browserPath;
			}
		}

		#region Helpers
		protected string GetEnvironmentVariable(string variableName, string defaultValue)
		{
			var value = Environment.GetEnvironmentVariable(variableName);
			var hasValue = !string.IsNullOrWhiteSpace(value);

			if(hasValue)
			{
				Console.WriteLine($"Overriding value with {variableName} = {value}");
			}

			return hasValue ? value : defaultValue;
		}

		protected bool GetEnvironmentVariable(string variableName, bool defaultValue)
		{
			var value = Environment.GetEnvironmentVariable(variableName);

			var hasValue = bool.TryParse(value, out var varValue);

			if(hasValue)
			{
				Console.WriteLine($"Overriding value with {variableName} = {value}");
			}

			return hasValue ? varValue : defaultValue;
		}
		#endregion
	}
}
