using System;
using NUnit.Framework;
using SamplesApp.UITests;
using Uno.UITest;
using Uno.UITest.Selenium;

[TestFixture]
public class AssemblySetup
{

	[OneTimeSetUpAttribute]
	public void Setup()
	{
		// Start the app only once, so the tests runs don't restart it
		// and gain some time for the tests.
		AppInitializer.StartApp(alreadyRunningApp: false);
	}
}

namespace Sample.UITests
{
	public class TestBase
	{
		protected IApp App { get; private set; }

		[SetUp]
		public void StartApp()
		{
			App = AppInitializer.StartApp(alreadyRunningApp: true);
			Uno.UITest.Helpers.Queries.Helpers.App = App;
		}

		[TearDown]
		public void CloseApp()
		{
			App.Dispose();
		}
	}
}
