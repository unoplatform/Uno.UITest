using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Uno.UITest;
using Uno.UITest.Helpers.Queries;
using Uno.UITest.Xamarin.Extensions;

namespace Uno.UITests.Helpers
{
	/// <summary>
	/// A Uno.UITest initializer
	/// </summary>
	public class AppInitializer
	{
		/// <summary>
		/// Name of the environment variable containing the running UI Test platform.
		/// </summary>
		public const string UNO_UITEST_PLATFORM = "UNO_UITEST_PLATFORM";

		/// <summary>
		/// Name of the environment variable containing the iOS App Bundle path
		/// </summary>
		public const string UITEST_IOSBUNDLE_PATH = "UNO_UITEST_IOSBUNDLE_PATH";

		/// <summary>
		/// Name of the environment variable containing the iOS Device ID or name to use to run the UI Tests.
		/// </summary>
		public const string UITEST_IOSDEVICE_ID = "UITEST_IOSDEVICE_ID";

		/// <summary>
		/// Name of the environment variable containing the path of the APK to use when running android tests.
		/// </summary>
		public const string UITEST_ANDROIDAPK_PATH = "UNO_UITEST_ANDROIDAPK_PATH";

		/// <summary>
		/// Name of the environment variable containing the path to use when creating screenshots.
		/// </summary>
		public const string UITEST_SCREENSHOT_PATH = "UNO_UITEST_SCREENSHOT_PATH";

		public static AppInitializerEnvironment TestEnvironment { get; } = new AppInitializerEnvironment();

		private static IApp _currentApp;

		/// <summary>
		/// Cold starts the registered app.
		/// </summary>
		/// <remarks>This method is generally called from the type constructor of a test assembly.</remarks>
		/// <returns>An <see cref="IApp"/> instance representing the running application.</returns>
		public static IApp ColdStartApp()
		{
			var app = Xamarin.UITest.TestEnvironment.Platform == Xamarin.UITest.TestPlatform.Local
						   ? StartApp(alreadyRunningApp: false)
						   : null;

			Uno.UITest.Helpers.Queries.Helpers.App = app;

			return app;
		}

		/// <summary>
		/// Attach to an already running application.
		/// </summary>
		/// <returns>An <see cref="IApp"/> instance representing the running application.</returns>
		public static IApp AttachToApp()
		{
			var app = StartApp(alreadyRunningApp: TestContext.CurrentContext.CurrentRepeatCount == 0);

			Uno.UITest.Helpers.Queries.Helpers.App = app;

			return app;
		}

		/// <summary>
		/// Provides the currently tested platform
		/// </summary>
		/// <returns></returns>
		public static Platform GetLocalPlatform()
		{
			var uitestPlatform = Environment.GetEnvironmentVariable(UNO_UITEST_PLATFORM);
			if(!Enum.TryParse(uitestPlatform, out Platform retVal))
			{
				retVal = TestEnvironment.CurrentPlatform;
			}

			return retVal;
		}

		private static IApp StartApp(bool alreadyRunningApp)
		{
			var retries = 3;

			do
			{
				try
				{
					Console.WriteLine($"Starting app (alreadyRunningApp: {alreadyRunningApp}, tries left: {retries})");

					switch(Xamarin.UITest.TestEnvironment.Platform)
					{
						case Xamarin.UITest.TestPlatform.TestCloudiOS:
							return Xamarin.UITest.ConfigureApp
								.iOS
								.StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear)
								.ToUnoApp();

						case Xamarin.UITest.TestPlatform.TestCloudAndroid:
							return Xamarin.UITest.ConfigureApp
								.Android
								.StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear)
								.ToUnoApp();

						default:
							var localPlatform = GetLocalPlatform();
							switch(GetLocalPlatform())
							{
								case Platform.Android:
									return CreateAndroidApp(alreadyRunningApp);

								case Platform.iOS:
									return CreateiOSApp(alreadyRunningApp);

								case Platform.Browser:
									if(alreadyRunningApp)
									{
										return CreateBrowserApp(alreadyRunningApp);
									}
									else
									{
										// Skip cold app start, there's no notion of reuse active browser yet.
										return null;
									}

								default:
									throw new Exception($"Platform {localPlatform} is not enabled.");
							}
					}
				}
				catch(Exception e)
				{
					if(--retries == 0)
					{
						throw;
					}
					else
					{
						alreadyRunningApp = false;

						Console.WriteLine($"App start failed, retrying in 2 seconds with complete app restart ({e.Message})");

#if DEBUG
						Console.WriteLine($"Exception: {e.Message}");
#endif
					}
				}
			}
			while(true);
		}

		private static IApp CreateBrowserApp(bool alreadyRunningApp)
		{
			if(_currentApp != null)
			{
				if(!alreadyRunningApp)
				{
					_currentApp.Dispose();
				}
				else
				{
					return _currentApp;
				}
			}

			try
			{
				var configurator = Uno.UITest.Selenium.ConfigureApp
					.WebAssembly
					.Uri(new Uri(TestEnvironment.WebAssemblyDefaultUri))
					.UsingBrowser(TestEnvironment.WebAssemblyBrowser.ToString());

				if(!string.IsNullOrEmpty(TestEnvironment.ChromeDriverPath))
				{
					var driverPath = Path.Combine(
						TestContext.CurrentContext.TestDirectory,
						TestEnvironment.ChromeDriverPath.Replace('\\', Path.DirectorySeparatorChar));
					configurator = configurator.DriverPath(driverPath);
				}
				else if(!string.IsNullOrEmpty(TestEnvironment.SeleniumDriverPath))
				{
					var driverPath = Path.Combine(
						TestContext.CurrentContext.TestDirectory,
						TestEnvironment.SeleniumDriverPath.Replace('\\', Path.DirectorySeparatorChar));
					configurator = configurator.DriverPath(driverPath);
				}

				if(TestEnvironment.WebAssemblyHeadless)
				{
					configurator = configurator
						.Headless(true);
				}
				else
				{
					configurator = configurator
						.Headless(false)
						.SeleniumArgument("--remote-debugging-port=9222");
				}

				_currentApp = configurator
					.ScreenShotsPath(TestContext.CurrentContext.TestDirectory)
					.StartApp();

				return _currentApp;
			}
			catch(Exception ex)
			{
				Console.Error.WriteLine(ex.Message);
				throw;
			}
		}

		private static IApp CreateAndroidApp(bool alreadyRunningApp)
		{
			if(string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ANDROID_HOME")))
			{
				Console.WriteLine("Providing default ANDROID_HOME and JAVA_HOME variables, because ANDROID_HOME could not be found.");

				// To set in case of Xamarin.UITest errors
				//
				Environment.SetEnvironmentVariable("ANDROID_HOME", @"C:\Program Files (x86)\Android\android-sdk");
				Environment.SetEnvironmentVariable("JAVA_HOME", @"C:\Program Files\Android\Jdk\microsoft_dist_openjdk_1.8.0.25");
			}

			var androidConfig = Xamarin.UITest.ConfigureApp
				.Android
				.Debug()
				.LogDirectory(Environment.GetEnvironmentVariable(UITEST_SCREENSHOT_PATH))
				.EnableLocalScreenshots();

			if(GetAndroidApkPath() is string bundlePath)
			{
				androidConfig = androidConfig.ApkFile(bundlePath);
			}
			else
			{
				androidConfig = androidConfig.InstalledApp(TestEnvironment.AndroidAppName);
			}

			var app = alreadyRunningApp
				? androidConfig.ConnectToApp()
				: androidConfig.StartApp();

			return app.ToUnoApp();
		}

		private static IApp CreateiOSApp(bool alreadyRunningApp)
		{
			var iOSConfig = Xamarin.UITest.ConfigureApp
				.iOS
				.Debug()
				.DeviceIdentifier(GetiOSDeviceId())
				.LogDirectory(Environment.GetEnvironmentVariable(UITEST_SCREENSHOT_PATH))
				.EnableLocalScreenshots();

			if(GetiOSAppBundlePath() is string bundlePath)
			{
				Console.WriteLine($"Using AppBundle Path: {bundlePath}");
				iOSConfig = iOSConfig.AppBundle(bundlePath);
			}
			else
			{
				Console.WriteLine($"Using Installed App: {TestEnvironment.iOSAppName}");
				iOSConfig = iOSConfig.InstalledApp(TestEnvironment.iOSAppName);
			}

			var app = alreadyRunningApp
				? iOSConfig.ConnectToApp()
				: iOSConfig.StartApp(Xamarin.UITest.Configuration.AppDataMode.DoNotClear);

			return app.ToUnoApp();
		}

		private static string GetiOSDeviceId()
		{
			var environmentDeviceId = Environment.GetEnvironmentVariable(UITEST_IOSDEVICE_ID) ?? "";

			if(!Guid.TryParse(environmentDeviceId, out var deviceId))
			{
				var deviceName = !string.IsNullOrEmpty(environmentDeviceId) ? environmentDeviceId : TestEnvironment.iOSDeviceNameOrId;

				Process process = new Process();
				process.StartInfo.FileName = "xcrun";
				process.StartInfo.Arguments = $"simctl list devices -j \"{deviceName}\" available";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.Start();

				var deviceList = JsonConvert.DeserializeObject(process.StandardOutput.ReadToEnd()) as JObject;

				if(deviceList != null
					&& deviceList["devices"] is JObject systems)
				{
					foreach(var system in systems.Values())
					{
						if(system is JArray devices)
						{
							foreach(var device in devices)
							{
								if(device is JObject dev)
								{
									return device["udid"].ToString();
								}
							}
						}
					}
				}

				process.WaitForExit();
			}

			return environmentDeviceId;
		}
		private static string GetAndroidApkPath()
		{
			var value = Environment.GetEnvironmentVariable(UITEST_ANDROIDAPK_PATH);
			return string.IsNullOrWhiteSpace(value) ? null : value;
		}

		private static string GetiOSAppBundlePath()
		{
			var value = Environment.GetEnvironmentVariable(UITEST_IOSBUNDLE_PATH);
			return string.IsNullOrWhiteSpace(value) ? null : value;
		}
	}
}
