using Xamarin.UITest.Queries;
using Uno.UITest.Xamarin.Extensions;

namespace Uno.UITest.Xamarin
{
	internal class XamarinPropertyAppQuery : IPropertyAppQuery
	{
		private PropertyAppQuery _source;

		public XamarinPropertyAppQuery(PropertyAppQuery query) => this._source = query;

		QueryPlatform IPropertyAppQuery.QueryPlatform => (QueryPlatform)_source.QueryPlatform;

		IAppQuery IPropertyAppQuery.Contains(string text) => _source.Contains(text).AsGenericAppQuery();

		IAppQuery IPropertyAppQuery.EndsWith(string text) => _source.EndsWith(text).AsGenericAppQuery();

		IAppQuery IPropertyAppQuery.Like(string text) => _source.Like(text).AsGenericAppQuery();

		IAppQuery IPropertyAppQuery.Predicate(string predicate, string text) => _source.Predicate(predicate, text).AsGenericAppQuery();

		IAppQuery IPropertyAppQuery.StartsWith(string text) => _source.StartsWith(text).AsGenericAppQuery();

		IAppTypedSelector<T> IPropertyAppQuery.Value<T>() => _source.Value<T>().AsGenericAppTypedSelector();
	}
}
