using System;
using NUnit.Framework;
using SamplesApp.UITests;
using Uno.UITest;
using Uno.UITest.Selenium;

namespace Sample.UITests
{
	public class TestBase
	{
		protected IApp App { get; private set; }

		[SetUp]
		public void StartApp()
		{
			App = AppInitializer.StartApp();
			Uno.UITest.Helpers.Queries.Helpers.App = App;
		}

		[TearDown]
		public void CloseApp()
		{
			App.Dispose();
		}
	}
}
