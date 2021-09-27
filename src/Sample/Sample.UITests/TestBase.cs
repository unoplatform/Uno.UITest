using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Uno.UITest;
using Uno.UITest.Selenium;
using Uno.UITests.Helpers;

namespace Sample.UITests
{
	public class TestBase
	{
		private IApp _app;
		private DateTime? _logsLastDate;

		static TestBase()
		{
			InitializeTestEnvrionment();

			// Start the app only once, so the tests runs don't restart it
			// and gain some time for the tests.
			AppInitializer.ColdStartApp();
		}

		protected IApp App
		{
			get => _app;
			private set
			{
				_app = value;
				Uno.UITest.Helpers.Queries.Helpers.App = value;
			}
		}

		public static void InitializeTestEnvrionment()
		{
			AppInitializer.TestEnvironment.AndroidAppName = Constants.AndroidAppName;
			AppInitializer.TestEnvironment.WebAssemblyDefaultUri = Constants.WebAssemblyDefaultUri;
			AppInitializer.TestEnvironment.iOSAppName = Constants.iOSAppName;
			AppInitializer.TestEnvironment.AndroidAppName = Constants.AndroidAppName;
			AppInitializer.TestEnvironment.iOSDeviceNameOrId = Constants.iOSDeviceNameOrId;
			AppInitializer.TestEnvironment.CurrentPlatform = Constants.CurrentPlatform;

#if DEBUG
			AppInitializer.TestEnvironment.WebAssemblyHeadless = false;
#endif
		}

		[SetUp]
		public void StartApp()
		{
			App = AppInitializer.AttachToApp();

			_logsLastDate = App?.GetSystemLogs().LastOrDefault()?.Timestamp;
		}

		[TearDown]
		public void CloseApp()
		{
			AttachSystemLog();
		}

		private void AttachSystemLog()
		{
			Console.WriteLine($"=== Browser Logs ===");

			foreach(var log in App.GetSystemLogs(_logsLastDate))
			{
				Console.WriteLine($"[{log.Timestamp}][{log.Level}] {log.Message}");
			}
		}
	}
}
