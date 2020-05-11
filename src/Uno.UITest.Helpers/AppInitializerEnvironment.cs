using Uno.UITest.Helpers.Queries;

namespace Uno.UITests.Helpers
{
	/// <summary>
	/// Environment default values for <see cref="AppInitializer"/>
	/// </summary>
	public class AppInitializerEnvironment
	{
		internal AppInitializerEnvironment()
		{
		}

		/// <summary>
		/// Defines the iOS Device name or ID. Default value for <see cref="AppInitializer.UITEST_IOSDEVICE_ID"/>
		/// </summary>
		public string iOSDeviceNameOrId { get; set; }

		/// <summary>
		/// Defines the Application bundle ID to use. Default value for <see cref="AppInitializer.UITEST_IOSBUNDLE_PATH"/>
		/// </summary>
		public string iOSAppName { get; set; }

		/// <summary>
		///Defines the Uri to use for WebAssembly tests
		/// </summary>
		public string WebAssemblyDefaultUri { get; set; }

		/// <summary>
		/// Defines the current tested platform. Defaut value for <see cref="AppInitializer.UNO_UITEST_PLATFORM"/>
		/// </summary>
		public Platform CurrentPlatform { get; set; }

		/// <summary>
		/// Defines the currently tested app name. Default value when <see cref="AppInitializer.UITEST_ANDROIDAPK_PATH"/> is not set.
		/// </summary>
		public string AndroidAppName { get; set; }

		/// <summary>
		/// Defines the location of chrome driver.
		/// </summary>
		/// <remarks>
		/// If not defined, the test engine will select the version based on
		/// the currently installed Chrome version.
		/// </remarks>
		public string ChromeDriverPath { get; set; }

		/// <summary>
		/// Defines the location of selenium driver.
		/// </summary>
		/// <remarks>
		/// If not defined, the test engine will select the version based on
		/// the currently installed browsers version.
		/// </remarks>
		public string SeleniumDriverPath { get; set; }

		/// <summary>
		/// Defines if the browser tests are running in chrome without a window.
		/// </summary>
		public bool WebAssemblyHeadless { get; set; } = true;

		/// <summary>
		/// Defines the browser to use for the Web platform. Cf. remarks about compatibility
		/// </summary>
		/// <remarks>
		/// Note that all browser does not supports all options defined here. For instance Edge does support only the <see cref="SeleniumDriverPath"/>.
		/// </remarks>
		public Browser WebAssemblyBrowser { get; set; } = Browser.Chrome;
	}
}
