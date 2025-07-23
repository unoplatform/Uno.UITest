using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Uno.UITest.Helpers.Queries;
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;

namespace Sample.UITests
{
	public class CheckBox_Tests : TestBase
	{
		[Test]
		public void CheckBox01()
		{
			Query checkBoxSelector = q => q.Marked("CheckBox 1");
			App.WaitForElement(checkBoxSelector);
			App.Tap(checkBoxSelector);

			Query cb1 = q => q.Marked("cb1");
			Query cb2 = q => q.Marked("cb2");
			App.WaitForElement(cb1);
			App.WaitForElement(cb2);

			var value = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			ClassicAssert.IsFalse(value);

			var value2 = App.Query(q => cb2(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			ClassicAssert.IsFalse(value2);

			App.WaitForNoElement("rect1");
			App.WaitForNoElement("rect2");

			App.Tap(cb1);

			var value3 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			ClassicAssert.IsTrue(value3);

			App.WaitForElement("rect1");

			App.Tap(cb2);

			var value4 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			ClassicAssert.IsTrue(value4);

			App.WaitForElement("rect2");

			App.Tap(cb1);
			App.Tap(cb2);

			App.WaitForNoElement("rect1");
			App.WaitForNoElement("rect2");
		}

		[Test]
		public void CheckBox01_RawQuery()
		{
			Query checkBoxSelector = q => q.Raw("* marked:'CheckBox 1'");
			App.WaitForElement(checkBoxSelector);

			App.Tap(checkBoxSelector);

			Query cb1 = q => q.Raw("* marked:'cb1'");
			App.WaitForElement(cb1);

			App.Tap(cb1);

			var value3 = App.Query(q => cb1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First();
			ClassicAssert.IsTrue(value3);
		}
	}
}
