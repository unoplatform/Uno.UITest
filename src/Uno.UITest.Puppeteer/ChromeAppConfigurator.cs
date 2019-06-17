using System;

namespace Uno.UITest.Selenium
{
	public class ChromeAppConfigurator
	{
		internal string ChromeDriverPath { get; private set; }
		internal Uri SiteUri { get; private set; }
		internal string InternalScreenShotsPath { get; private set; } = "";
		internal bool InternalHeadless { get; private set; } = true;
		internal int InternalWindowWidth { get; private set; } = 1024;
		internal int InternalWindowHeight { get; private set; } = 768;

		public ChromeAppConfigurator()
		{
		}

		public ChromeAppConfigurator Uri(Uri uri) { SiteUri = uri; return this; }

		public ChromeAppConfigurator ChromeDriverLocation(string chromeDriverPath) { ChromeDriverPath = chromeDriverPath; return this; }

		public ChromeAppConfigurator ScreenShotsPath(string path) { InternalScreenShotsPath = path; return this; }

		/// <summary>
		/// Runs the browser as headless. Defaults to true.
		/// </summary>
		/// <param name="isHeadless"></param>
		/// <returns></returns>
		public ChromeAppConfigurator Headless(bool isHeadless) { InternalHeadless = isHeadless; return this; }


		/// <summary>
		/// Sets the window size. Defaults to 1024x768
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public ChromeAppConfigurator WindowSize(int width, int height)
		{
			InternalWindowWidth = width;
			InternalWindowHeight = height;
			return this;
		}

		public IApp StartApp() => new SeleniumApp(this);
	}
}
