using System;
using NUnit.Framework;
using SamplesApp.UITests;
using Uno.UITest;
using Uno.UITest.Selenium;

namespace Sample.UITests
{
	public class TestBase
	{
		private IApp _app;

		static TestBase()
		{
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

		[SetUp]
		public void StartApp()
		{
			App = AppInitializer.AttachToApp();
		}

		[TearDown]
		public void CloseApp()
		{
		}
	}
}
