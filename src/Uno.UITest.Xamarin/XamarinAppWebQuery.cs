using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin
{
	internal class XamarinAppWebQuery : IAppWebQuery
	{
		private AppWebQuery _source;

		public XamarinAppWebQuery(AppWebQuery query) => _source = query;

		public AppWebQuery Source => _source;

		IAppWebQuery IAppWebQuery.Index(int index) => new XamarinAppWebQuery(Source.Index(index));

		string IAppWebQuery.ToString() => Source.ToString();
	}
}
