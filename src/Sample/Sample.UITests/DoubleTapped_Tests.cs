using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Uno.UITest.Helpers.Queries;
using Uno.UITests.Helpers;

namespace Sample.UITests
{
	public class DoubleTapped_Tests : TestBase
	{
		[Test]
		[Ignore("DoubleTapped is currently broken in Uno ...")]
		public void DoubleTap()
		{
			App.WaitForElement("DoubleTapped 01");
			App.Tap("DoubleTapped 01");

			App.WaitForElement("TouchTarget");
			App.DoubleTap("TouchTarget");

			App.WaitForDependencyPropertyValue(q => q.Marked("Result"), "Text",  "Double tapped!");

			// Sanity in case of timeout without exception!
			var result = App.Query(q => q.Marked("Result").GetDependencyPropertyValue("Text").Value<string>()).Single();
			ClassicAssert.AreEqual("Double tapped!", result);
		}
	}
}
