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
	public class Scroll_Tests : TestBase
	{
		[Test]
		public void When_ScrollDownTo()
		{
			Query testSelector = q => q.Marked("ScrollViewer01");

			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			Query sv = q => q.Marked("TargetScrollViewer");

			App.WaitForElement(sv);

			Query scp = q => q.Marked("TargetScrollViewer")
				.Descendant(className: "ScrollContentPresenter");

			App.WaitForElement(scp);

			Query resultTextBlock = q => q.Marked("ResultTextBlock");

			App.ScrollDownTo(
				resultTextBlock,
				sv
			);

			Query button = q => q.Marked("TestButton");
			App.Tap(button);
			App.WaitForDependencyPropertyValue(resultTextBlock, "Text", "Success");

		}
		[Test]
		public void When_ScrollDown()
		{
			Query testSelector = q => q.Marked("ScrollViewer01");

			App.WaitForElement(testSelector);
			App.Tap(testSelector);

			Query sv = q => q.Marked("TargetScrollViewer");

			App.WaitForElement(sv);

			for (var i = 0; i < 3; i++)
			{
				App.ScrollDown(sv);
			}

			Query resultTextBlock = q => q.Marked("ResultTextBlock");

			Query button = q => q.Marked("TestButton");
			App.Tap(button);
			App.WaitForDependencyPropertyValue(resultTextBlock, "Text", "Success");
		}
	}
}
