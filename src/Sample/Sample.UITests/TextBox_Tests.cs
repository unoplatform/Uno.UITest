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

		[Test]
		public void TextBox02_Enter()
		{
			Query testSelector = q => q.Marked("TextBox 02");
			Query tb01 = q => q.Marked("tb01");
			Query tb01Events = q => q.Marked("tb01Events");
			Query tbBound = q => q.Marked("tbBound01");
			StringQuery textSelector = q =>
				tb01(q).GetDependencyPropertyValue("Text").Value<string>();

			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			App.WaitForElement(tb01);

			App.Screenshot("tb01 - Initial");

			App.WaitForDependencyPropertyValue(tb01, "Text", "");
			App.WaitForDependencyPropertyValue(tbBound, "Text", "Value 1: ");

			App.Tap(tb01);
			App.Screenshot("tb01 - Step 1");

			App.EnterText(tb01, "Hello Uno!");

			App.WaitForDependencyPropertyValue(tb01, "Text", "Hello Uno!");
			App.WaitForDependencyPropertyValue(tbBound, "Text", "Value 1: Hello Uno!");

			App.Screenshot("tb01 - with text");

			App.ClearText();
			App.EnterText(tb01, "Some other text");

			App.WaitForDependencyPropertyValue(tb01, "Text", "Some other text");
			App.WaitForDependencyPropertyValue(tbBound, "Text", "Value 1: Some other text");

			App.Screenshot("tb01 - with text");

			App.ClearText(tb01);

			App.WaitForDependencyPropertyValue(tb01, "Text", "");
			App.WaitForDependencyPropertyValue(tbBound, "Text", "Value 1: ");

			App.Screenshot("tb01 - cleared");

			App.EnterText(tb01, "Enter key test");
			App.WaitForDependencyPropertyValue(tb01, "Text", "Enter key test");
			App.WaitForDependencyPropertyValue(tb01Events, "Text", "");

			App.PressEnter();
			App.WaitForDependencyPropertyValue(tb01Events, "Text", "Enter-Up;");
		}

	}
}
