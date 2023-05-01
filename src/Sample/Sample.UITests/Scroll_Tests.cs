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
		private readonly TimeSpan _timeout = TimeSpan.FromSeconds(60);

		[Test]
		public void ScrollTo()
		{
			if(AppInitializer.GetLocalPlatform() == Platform.iOS)
			{
				Assert.Ignore("Failing under Uno 3.7");
			}

			Query scrollSelector = q => q.Marked("Scroll 1");
			App.WaitForElement(scrollSelector);
			App.Tap(scrollSelector);
			App.ScrollTo("Data_Item_67");

			App.WaitForElement("Data_Item_67");
		}

		[Test]
		public void ScrollUpTo()
		{
			if(AppInitializer.GetLocalPlatform() == Platform.iOS)
			{
				Assert.Ignore("Failing under Uno 3.7");
			}

			Query scrollSelector = q => q.Marked("Scroll 1");
			App.WaitForElement(scrollSelector);
			App.Tap(scrollSelector);

			App.ScrollTo("Data_Item_100");
			App.ScrollUpTo("Data_Item_70", timeout: _timeout);

			App.WaitForElement("Data_Item_70");
		}

		[Test]
		public void ScrollDownTo()
		{
			if(AppInitializer.GetLocalPlatform() == Platform.iOS)
			{
				Assert.Ignore("Failing under Uno 3.7");
			}

			Query scrollSelector = q => q.Marked("Scroll 1");
			App.WaitForElement(scrollSelector);
			App.Tap(scrollSelector);


			App.ScrollDownTo("Data_Item_20", timeout: _timeout);

			App.WaitForElement("Data_Item_20");
		}
	}
}
