using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class InvokeJSAppQueryExtensions
	{
		public static IInvokeJSAppQuery AsUnoInvokeJSAppQuery(this InvokeJSAppQuery query)
			=> new XamarinInvokeJSAppQuery(query);

		public static InvokeJSAppQuery ToXamarinQuery(this IInvokeJSAppQuery query)
			=> query is XamarinInvokeJSAppQuery xq ? xq.Source : throw new InvalidOperationException();
	}
}
