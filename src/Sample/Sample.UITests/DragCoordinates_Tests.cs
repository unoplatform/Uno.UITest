using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Uno.UITest.Helpers.Queries;
using Uno.UITests.Helpers;
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;
using StringQuery = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppTypedSelector<string>>;

namespace Sample.UITests
{
	public class DragCoordinates_Tests : TestBase
	{
		[Test]
		public void DragBorder01()
		{
			App.Screenshot("home screen");

			Query testSelector = q => q.Marked("DragCoordinates 01");

			Query rootCanvas = q => q.Marked("rootCanvas");
			Query myBorder = q => q.Marked("myBorder");
			Query topValue = q => q.Marked("borderPositionTop");
			Query leftValue = q => q.Marked("borderPositionLeft");

			Query movedCountValue = q => q.Marked("movedCount");

			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			App.WaitForElement(rootCanvas);

			App.Screenshot("tb01 - Initial");

			App.WaitForDependencyPropertyValue(topValue, "Text", "0");
			App.WaitForDependencyPropertyValue(leftValue, "Text", "0");

			App.Screenshot("DragBorder01 - Step 1");

			var topBorderRect = App.Query(myBorder).First().Rect;

			App.DragCoordinates(topBorderRect.CenterX, topBorderRect.CenterY, topBorderRect.CenterX + 50, topBorderRect.CenterY + 50);

			App.Screenshot("DragBorder01 - Step 2");

			if(Xamarin.UITest.TestEnvironment.Platform == Xamarin.UITest.TestPlatform.TestCloudAndroid
				|| AppInitializer.GetLocalPlatform() == Platform.Android
				|| Xamarin.UITest.TestEnvironment.Platform == Xamarin.UITest.TestPlatform.TestCloudiOS
				|| AppInitializer.GetLocalPlatform() == Platform.iOS)
			{
				// PointerEvents don't fire properly on Android, causing this test to fail https://github.com/unoplatform/uno/issues/1257
				// PointerEvents are reporting an off by one value. May be fixed by https://github.com/unoplatform/uno/issues/1256
				return;
			}

			var value = App.Query(q => topValue(q).GetDependencyPropertyValue("Text").Value<string>()).First();
			
			App.WaitForDependencyPropertyValue(topValue, "Text", "50");
			App.WaitForDependencyPropertyValue(leftValue, "Text", "50");

			var movedCountText = App.Query(q => movedCountValue(q).GetDependencyPropertyValue("Text").Value<string>()).First();

			var movedCount = int.Parse(movedCountText);

			Assert.Greater(movedCount, 1);
		}
	}
}
