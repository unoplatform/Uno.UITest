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
	public class RadioButton_Tests : TestBase
	{
		[Test]
		public void RadioButton01()
		{
			Query testSelector = q => q.Marked("RadioButton 01");
			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			Query radio1 = q => q.Marked("radio1");
			Query radio2 = q => q.Marked("radio2");
			Query radio3 = q => q.Marked("radio3");
			Query results = q => q.Marked("result");

			App.WaitForElement(radio1);
			App.WaitForElement(radio2);
			App.WaitForElement(radio3);
			App.WaitForElement(results);

			App.Screenshot("RadioButton01 - Initial");

			ClassicAssert.IsFalse(App.Query(q => radio1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.IsFalse(App.Query(q => radio2(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.IsFalse(App.Query(q => radio3(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.AreEqual("", App.Query(q => results(q).GetDependencyPropertyValue("Text").Value<string>()).First());

			App.Tap(radio1);
			App.Screenshot("RadioButton01 - Step 1");

			ClassicAssert.IsTrue(App.Query(q => radio1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.IsFalse(App.Query(q => radio2(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.IsFalse(App.Query(q => radio3(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.AreEqual("Radio 01", App.Query(q => results(q).GetDependencyPropertyValue("Text").Value<string>()).First());

			App.Tap(radio2);
			App.Screenshot("RadioButton01 - Step 2");

			ClassicAssert.IsFalse(App.Query(q => radio1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.IsTrue(App.Query(q => radio2(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.IsFalse(App.Query(q => radio3(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.AreEqual("Radio 02", App.Query(q => results(q).GetDependencyPropertyValue("Text").Value<string>()).First());

			App.Tap(radio3);
			App.Screenshot("RadioButton01 - Step 3");

			ClassicAssert.IsFalse(App.Query(q => radio1(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.IsFalse(App.Query(q => radio2(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.IsTrue(App.Query(q => radio3(q).GetDependencyPropertyValue("IsChecked").Value<bool>()).First());
			ClassicAssert.AreEqual("Radio 03", App.Query(q => results(q).GetDependencyPropertyValue("Text").Value<string>()).First());
		}
	}
}
