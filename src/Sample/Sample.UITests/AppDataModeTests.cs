using NUnit.Framework;
using NUnit.Framework.Legacy;
using Uno.UITest;
using Uno.UITest.Helpers;
using Uno.UITest.Helpers.Queries;
using Uno.UITests.Helpers;
using Xamarin.UITest.Configuration;
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;

namespace Sample.UITests
{
	public class AppDataModeTests // Intentionally doesn't inherit TestBase to customize ;)
	{
		[Test]
		public void AppDataModeTest()
		{
			TestBase.InitializeTestEnvrionment();

			if(AppInitializer.GetLocalPlatform() == Platform.Browser)
			{
				Assert.Ignore("Not supported in browser");
			}

			var app = OpenSample(AppDataMode.Clear);
			ClassicAssert.AreEqual("<INITIAL_VALUE>", app.Marked("LocalSettingValueTextBlock").GetDependencyPropertyValue<string>("Text"));
			app.Marked("SetLocalSettingButton").Tap();
			app.Marked("GetLocalSettingButton").Tap();
			ClassicAssert.AreEqual("MyValue", app.Marked("LocalSettingValueTextBlock").GetDependencyPropertyValue<string>("Text"));

			app = OpenSample(AppDataMode.DoNotClear);
			app.Marked("GetLocalSettingButton").Tap();
			ClassicAssert.AreEqual("MyValue", app.Marked("LocalSettingValueTextBlock").GetDependencyPropertyValue<string>("Text"));

			app = OpenSample(AppDataMode.Clear);
			app.Marked("GetLocalSettingButton").Tap();
			ClassicAssert.AreEqual("<NOT_SET>", app.Marked("LocalSettingValueTextBlock").GetDependencyPropertyValue<string>("Text"));
		}

		private static IApp OpenSample(AppDataMode mode = AppDataMode.Clear)
		{
			var app = AppInitializer.ColdStartApp(mode);

			Query selector = q => q.Marked("AppDataMode 1");
			app.WaitForElement(selector);
			app.Tap(selector);
			return app;
		}
	}
}
