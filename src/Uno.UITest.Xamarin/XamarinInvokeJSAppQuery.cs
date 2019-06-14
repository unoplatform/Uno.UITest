using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin
{
	internal class XamarinInvokeJSAppQuery : IInvokeJSAppQuery
	{
		private InvokeJSAppQuery _source;

		public XamarinInvokeJSAppQuery(InvokeJSAppQuery query) => this._source = query;

		public InvokeJSAppQuery Source => _source;
	}
}
