using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Sample.UITests;
using Uno.UITest;
using Uno.UITest.Helpers.Queries;
using Uno.UITest.Xamarin.Extensions;

namespace SamplesApp.UITests
{
	public class AppInitializer
	{
		public const string UITestPlatform = "UNO_UITEST_PLATFORM";
		public const string UITEST_IOSBUNDLE_PATH = "UNO_UITEST_IOSBUNDLE_PATH";
		public const string UITEST_IOSDEVICE_ID = "UITEST_IOSDEVICE_ID";
		public const string UITEST_ANDROIDAPK_PATH = "UNO_UITEST_ANDROIDAPK_PATH";
		public const string UITEST_SCREENSHOT_PATH = "UNO_UITEST_SCREENSHOT_PATH";

		private const string DriverPath = @"..\..\..\..\..\..\build\node_modules\chromedriver\lib\chromedriver";
		private static IApp _currentApp;

		public static IApp ColdStartApp()
			=> Xamarin.UITest.TestEnvironment.Platform == Xamarin.UITest.TestPlatform.Local
				? StartApp(alreadyRunningApp: false)
				: null;

		public static IApp AttachToApp()
			// If the retry count is set, the test already failed. Retry the test with restarting the app.
			=> StartApp(alreadyRunningApp: TestContext.CurrentContext.CurrentRepeatCount == 0);

		private static IApp StartApp(bool alreadyRunningApp)
		{
			Console.WriteLine($"Starting app ({alreadyRunningApp})");

			switch (Xamarin.UITest.TestEnvironment.Platform)
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
					switch (GetLocalPlatform())
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
				var app = _currentApp = Uno.UITest.Selenium.ConfigureApp
					.WebAssembly
					.Uri(new Uri(Constants.DefaultUri))
					.ChromeDriverLocation(Path.Combine(TestContext.CurrentContext.TestDirectory,
						DriverPath.Replace('\\', Path.DirectorySeparatorChar)))
					.ScreenShotsPath(TestContext.CurrentContext.TestDirectory)
#if DEBUG
					.Headless(false)
					.SeleniumArgument("--remote-debugging-port=9222")
#endif
					.StartApp();

				return app;
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
				// To set in case of Xamarin.UITest errors
				//
				Environment.SetEnvironmentVariable("ANDROID_HOME", @"C:\Program Files (x86)\Android\android-sdk");
				Environment.SetEnvironmentVariable("JAVA_HOME", @"C:\Program Files\Android\Jdk\microsoft_dist_openjdk_1.8.0.25");
			}

			var androidConfig = Xamarin.UITest.ConfigureApp
				.Android
				.Debug()
				.EnableLocalScreenshots();

			if(GetAndroidApkPath() is string bundlePath)
			{
				androidConfig = androidConfig.ApkFile(bundlePath);
			}
			else
			{
				androidConfig = androidConfig.InstalledApp(Constants.AndroidAppName);
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
				Console.WriteLine($"Using Installed App: {Constants.iOSAppName}");
				iOSConfig = iOSConfig.InstalledApp(Constants.iOSAppName);
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
				var deviceName = !string.IsNullOrEmpty(environmentDeviceId) ? environmentDeviceId : Constants.iOSDeviceNameOrId;

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

		public static Platform GetLocalPlatform()
		{
			var uitestPlatform = Environment.GetEnvironmentVariable(UITestPlatform);
			if (!Enum.TryParse(uitestPlatform, out Platform retVal))
			{
				retVal = Constants.CurrentPlatform;
			}

			return retVal;
		}
	}
}
