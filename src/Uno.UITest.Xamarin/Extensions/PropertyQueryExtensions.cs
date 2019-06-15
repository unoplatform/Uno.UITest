using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	public static class PropertyQueryExtensions
	{
		public static IPropertyAppQuery AsGenericPropertyQuery(this PropertyAppQuery query)
			=> new XamarinPropertyAppQuery(query);
	}
}
