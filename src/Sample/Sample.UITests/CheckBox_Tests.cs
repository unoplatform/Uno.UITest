using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Uno.UITest.Helpers.Queries;
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;

namespace Sample.UITests
{
	public class CheckBox_Tests : TestBase
	{
		[Test]
		public void BasicTest()
		{
			Query checkBoxSelector = q => q.Text("CheckBox 1");
			App.WaitForElement(checkBoxSelector);
			App.Tap(checkBoxSelector);

			Query cb1 = q => q.Marked("cb1");
			App.WaitForElement(cb1);

			var value = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			Assert.IsFalse(value);

			App.Tap(cb1);

			var value2 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			Assert.IsTrue(value2);
		}
	}
}
