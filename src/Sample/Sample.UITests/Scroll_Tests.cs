using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Uno.UITest.Helpers.Queries;
using Uno.UITests.Helpers;
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;


namespace Sample.UITests
{
	public class Scroll_Tests : TestBase
	{
		private bool IsXamarin => Xamarin.UITest.TestEnvironment.Platform == Xamarin.UITest.TestPlatform.TestCloudAndroid
				|| AppInitializer.GetLocalPlatform() == Platform.Android
				|| Xamarin.UITest.TestEnvironment.Platform == Xamarin.UITest.TestPlatform.TestCloudiOS
				|| AppInitializer.GetLocalPlatform() == Platform.iOS;

		[Test]
		public void When_ScrollDownTo()
		{
			if (!IsXamarin)
			{
				// Scroll test methods aren't yet implemented for WASM 
				return;
			}

			Query testSelector = q => q.Marked("ScrollViewer01");

			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			Query sv = q => q.Marked("TargetScrollViewer");

			App.WaitForElement(sv);

			Query scp = q => q.Marked("TargetScrollViewer")
				.Descendant(className: "ScrollContentPresenter");

			App.WaitForElement(scp);

			Query resultTextBlock = q => q.Marked("ResultTextBlock");

			App.ScrollDownTo(
				resultTextBlock,
				sv
			);

			Query button = q => q.Marked("TestButton");
			App.Tap(button);
			App.WaitForDependencyPropertyValue(resultTextBlock, "Text", "Success");

		}
		[Test]
		public void When_ScrollDown()
		{
			if (!IsXamarin)
			{
				// Scroll test methods aren't yet implemented for WASM 
				return;
			}

			Query testSelector = q => q.Marked("ScrollViewer01");

			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			Query sv = q => q.Marked("TargetScrollViewer");

			App.WaitForElement(sv);

			for (var i = 0; i < 3; i++)
			{
				App.ScrollDown(sv);
			}

			Query resultTextBlock = q => q.Marked("ResultTextBlock");

			Query button = q => q.Marked("TestButton");
			App.Tap(button);
			App.WaitForDependencyPropertyValue(resultTextBlock, "Text", "Success");
		}
	}
}
