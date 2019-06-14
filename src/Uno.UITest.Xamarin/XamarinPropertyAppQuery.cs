using Xamarin.UITest.Queries;
using Uno.UITest.Xamarin.Extensions;

namespace Uno.UITest.Xamarin
{
	internal class XamarinPropertyAppQuery : IPropertyQuery
	{
		private PropertyAppQuery _source;

		public XamarinPropertyAppQuery(PropertyAppQuery query) => this._source = query;

		QueryPlatform IPropertyQuery.QueryPlatform => (QueryPlatform)_source.QueryPlatform;

		IAppQuery IPropertyQuery.Contains(string text) => _source.Contains(text).AsGenericAppQuery();

		IAppQuery IPropertyQuery.EndsWith(string text) => _source.EndsWith(text).AsGenericAppQuery();

		IAppQuery IPropertyQuery.Like(string text) => _source.Like(text).AsGenericAppQuery();

		IAppQuery IPropertyQuery.Predicate(string predicate, string text) => _source.Predicate(predicate, text).AsGenericAppQuery();

		IAppQuery IPropertyQuery.StartsWith(string text) => _source.StartsWith(text).AsGenericAppQuery();

		IAppTypedSelector<T> IPropertyQuery.Value<T>() => _source.Value<T>().AsGenericAppTypedSelector();
	}
}
