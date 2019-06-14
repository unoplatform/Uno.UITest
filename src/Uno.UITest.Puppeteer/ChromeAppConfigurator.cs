using System;

namespace Uno.UITest.Puppeteer
{
	public class ChromeAppConfigurator
	{
		internal string ChromeDriverPath { get; set; }
		internal Uri SiteUri { get; set; }

		public ChromeAppConfigurator()
		{
		}

		public ChromeAppConfigurator Uri(Uri uri) { SiteUri = uri; return this; }

		public ChromeAppConfigurator ChromeDriverLocation(string chromeDriverPath) { ChromeDriverPath = chromeDriverPath; return this; }

		public IApp StartApp() => new SeleniumApp(this);
	}
}
