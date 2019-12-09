using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Uno.UITest.Helpers.Queries;
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;
using StringQuery = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppTypedSelector<string>>;

namespace Sample.UITests
{
	public class TextBox_Tests : TestBase
	{
		[Test]
		public void TextBox01()
		{
			Query testSelector = q => q.Marked("TextBox 01");
			Query tb01 = q => q.Marked("tb01");
			StringQuery textSelector = q =>
				tb01(q).GetDependencyPropertyValue("Text").Value<string>();

			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			App.WaitForElement(tb01);

			App.Screenshot("tb01 - Initial");

			App.WaitForDependencyPropertyValue(tb01, "Text", "");

			App.Tap(tb01);
			App.Screenshot("tb01 - Step 1");

			App.EnterText(tb01, "Hello Uno!");

			App.WaitForDependencyPropertyValue(tb01, "Text", "Hello Uno!");

			App.Screenshot("tb01 - with text");

			App.ClearText(tb01);

			App.WaitForDependencyPropertyValue(tb01, "Text", "");

			App.Screenshot("tb01 - cleared");
		}
	}
}
