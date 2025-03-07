﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Uno.UITest.Helpers;
using Uno.UITest.Helpers.Queries;
using Query = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppQuery>;
using StringQuery = System.Func<Uno.UITest.IAppQuery, Uno.UITest.IAppTypedSelector<string>>;

namespace Sample.UITests
{
	public class ElementSelection_Tests : TestBase
	{
		[Test]
		public void ElementSelection01()
		{
			Query testSelector = q => q.Marked("Element Selection 01");
			Query outer01 = q => q.Marked("outer01");
			Query outer02 = q => q.Marked("outer02");

			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			App.WaitForElement(outer01);

			Query inner01 = q => q.Marked("outer01").Descendant().Marked("innerElement");
			App.WaitForDependencyPropertyValue(inner01, "Text", "Text 1");

			Query inner02 = q => q.Marked("outer02").Descendant().Marked("innerElement");
			App.Tap(inner02);
			App.WaitForDependencyPropertyValue(inner02, "Text", "Text 2");
		}

		[Test]
		public void ElementSelection_WithClass()
		{
			//App.Repl();
			Query testSelector = q => q.Marked("Element Selection 01");

			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			App.WaitForElement(App.CreateQuery(
				q => q.WithClass("Microsoft.UI.Xaml.Controls.TextBlock")));

			App.WaitForElement(App.CreateQuery(
				q => q.WithClass("Microsoft")));

			App.WaitForElement(App.CreateQuery(
				q => q.WithClass("Microsoft.UI.Xaml.Controls.DatePicker")));

			if(Helpers.Platform != Platform.iOS)
			{
				App.WaitForElement(App.CreateQuery(
					q => q.WithText("MyTextBlock")));

				App.WaitForElement(App.CreateQuery(
					q => q.WithClass("Microsoft.UI.Xaml.Controls.TextBlock").WithText("MyTextBlock")));
			}
		}
	}
}
