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
		public void CheckBox01()
		{
			Query checkBoxSelector = q => q.Marked("CheckBox01");
			App.WaitForElement(checkBoxSelector);
			App.Tap(checkBoxSelector);

			Query cb1 = q => q.Marked("cb1");
			Query cb2 = q => q.Marked("cb2");
			App.WaitForElement(cb1);
			App.WaitForElement(cb2);

			var value = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			Assert.IsFalse(value);

			var value2 = App.Query(q => cb2(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			Assert.IsFalse(value2);

			App.Tap(cb1);

			var value3 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			Assert.IsTrue(value3);

			App.Tap(cb2);

			var value4 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			Assert.IsTrue(value4);
		}
	}
}
