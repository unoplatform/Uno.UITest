using System;
using System.IO;
using System.Reflection;
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
		public const string UITEST_ANDROIDAPK_PATH = "UNO_UITEST_ANDROIDAPK_PATH";
		public const string UITEST_SCREENSHOT_PATH = "UNO_UITEST_SCREENSHOT_PATH";

		private const string DriverPath = @"..\..\..\..\SamplesApp.Wasm.UITests\node_modules\chromedriver\lib\chromedriver";

		public static IApp StartApp()
		{
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
							return CreateAndroidApp();

						case Platform.iOS:
							return CreateiOSApp();

						case Platform.Browser:
							return CreateBrowserApp();

						default:
							throw new Exception($"Platform {localPlatform} is not enabled.");
					}
			}
		}

		private static IApp CreateBrowserApp() => Uno.UITest.Selenium.ConfigureApp
			.WebAssembly
			.Uri(new Uri(Constants.DefaultUri))
			.ChromeDriverLocation(Path.Combine(TestContext.CurrentContext.TestDirectory, DriverPath.Replace('\\', Path.DirectorySeparatorChar)))
			.ScreenShotsPath(TestContext.CurrentContext.TestDirectory)
#if DEBUG
			.Headless(false)
#endif
			.StartApp();

		private static IApp CreateAndroidApp()
		{
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

			var app = androidConfig.StartApp()
				.ToUnoApp();

			ApplyScreenShotPath();

			return app;
		}

		private static void ApplyScreenShotPath()
		{
			var value = Environment.GetEnvironmentVariable(UITEST_SCREENSHOT_PATH);
			if(!string.IsNullOrWhiteSpace(value))
			{
				Environment.CurrentDirectory = value;
			}
		}

		private static IApp CreateiOSApp()
		{
			var iOSConfig = Xamarin.UITest.ConfigureApp
				.iOS
				.Debug()
				.EnableLocalScreenshots();

			if(GetiOSAppBundlePath() is string bundlePath)
			{
				iOSConfig = iOSConfig.AppBundle(bundlePath);
			}
			else
			{
				iOSConfig = iOSConfig.InstalledApp(Constants.AndroidAppName);
			}

			var app = iOSConfig
				.StartApp()
				.ToUnoApp();

			ApplyScreenShotPath();

			return app;
		}

		private static string GetAndroidApkPath()
		{
			var value = Environment.GetEnvironmentVariable(UITEST_ANDROIDAPK_PATH);
			return string.IsNullOrWhiteSpace(value) ? "" : value;
		}

		private static string GetiOSAppBundlePath()
		{
			var value = Environment.GetEnvironmentVariable(UITEST_IOSBUNDLE_PATH);
			return string.IsNullOrWhiteSpace(value) ? "" : value;
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
