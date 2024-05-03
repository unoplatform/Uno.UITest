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
	public class SetPropertyValue_Tests : TestBase
	{
		[Test]
		public void SetValue01()
		{
			Query menuSelector = q => q.Marked("SetPropertyValue 01");
			App.WaitForElement(menuSelector);
			App.Tap(menuSelector);

			Query tb1 = q => q.Marked("tb1");
			Query tb2 = q => q.Marked("tb2");
			App.WaitForElement(tb1);
			App.WaitForElement(tb2);

			var tb1Value = App.Query(q => tb1(q).GetDependencyPropertyValue("Text").Value<string>()).First();
			ClassicAssert.AreEqual("None", tb1Value);

			var tb2Value = App.Query(q => tb2(q).GetDependencyPropertyValue("Text").Value<string>()).First();
			ClassicAssert.AreEqual("None", tb2Value);

			var assignedValue = App.Query(q => tb1(q).SetDependencyPropertyValue("Text", "test value").Value<string>()).First();
			tb2Value = App.Query(q => tb2(q).GetDependencyPropertyValue("Text").Value<string>()).First();

			ClassicAssert.AreEqual("test value", assignedValue);
			ClassicAssert.AreEqual("test value", tb2Value);
		}
	}
}
