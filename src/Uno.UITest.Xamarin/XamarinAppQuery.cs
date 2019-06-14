using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	internal class XamarinAppQuery : IAppQuery
	{
		private AppQuery _sourceQuery;

		public XamarinAppQuery(AppQuery q) => SourceQuery = q;

		public AppQuery SourceQuery { get => _sourceQuery; set => _sourceQuery = value; }

		IAppQuery IAppQuery.All(string className)
			=> SourceQuery.All(className).AsGenericAppQuery();

		IAppQuery IAppQuery.Button(string marked)
			=> SourceQuery.Button(marked).AsGenericAppQuery();

		IAppQuery IAppQuery.Child(string className)
			=> SourceQuery.Child(className).AsGenericAppQuery();

		IAppQuery IAppQuery.Child(int index)
			=> SourceQuery.Child(index).AsGenericAppQuery();

		IAppQuery IAppQuery.Class(string className)
			=> SourceQuery.Class(className).AsGenericAppQuery();

		IAppQuery IAppQuery.ClassFull(string className)
			=> SourceQuery.ClassFull(className).AsGenericAppQuery();

		IAppQuery IAppQuery.Descendant(int index)
			=> SourceQuery.Descendant(index).AsGenericAppQuery();

		IAppQuery IAppQuery.Descendant(string className)
			=> SourceQuery.Descendant(className).AsGenericAppQuery();

		IAppQuery IAppQuery.Frame(string cssSelector)
			=> SourceQuery.Frame(cssSelector).AsGenericAppQuery();

		IAppQuery IAppQuery.Id(string id)
			=> SourceQuery.Id(id).AsGenericAppQuery();

		IAppQuery IAppQuery.Id(int id)
			=> SourceQuery.Id(id).AsGenericAppQuery();

		IAppQuery IAppQuery.Index(int index)
			=> SourceQuery.Index(index).AsGenericAppQuery();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName)
			=> SourceQuery.Invoke(methodName).AsGenericAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1)
			=> SourceQuery.Invoke(methodName, arg1).AsGenericAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2)
			=> SourceQuery.Invoke(methodName, arg1, arg2).AsGenericAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3)
			=> SourceQuery.Invoke(methodName, arg1, arg2, arg3).AsGenericAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4)
			=> SourceQuery.Invoke(methodName, arg1, arg2, arg3, arg4).AsGenericAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
			=> SourceQuery.Invoke(methodName, arg1, arg2, arg3, arg4, arg5).AsGenericAppTypedSelector();

		IInvokeJSAppQuery IAppQuery.InvokeJS(string javascript)
			=> SourceQuery.InvokeJS(javascript).AsGenericInvokeJSAppQuery();

		IAppQuery IAppQuery.Marked(string text)
			=> SourceQuery.Marked(text).AsGenericAppQuery();

		IAppQuery IAppQuery.Parent(string className)
			=> SourceQuery.Parent(className).AsGenericAppQuery();

		IAppQuery IAppQuery.Parent(int index)
			=> SourceQuery.Parent(index).AsGenericAppQuery();

		IPropertyQuery IAppQuery.Property(string propertyName)
			=> SourceQuery.Property(propertyName).AsGenericPropertyQuery();

		IAppQuery IAppQuery.Property(string propertyName, string value)
			=> SourceQuery.Property(propertyName, value).AsGenericAppQuery();

		IAppQuery IAppQuery.Property(string propertyName, bool value)
			=> SourceQuery.Property(propertyName, value).AsGenericAppQuery();

		IAppQuery IAppQuery.Property(string propertyName, int value)
			=> SourceQuery.Property(propertyName, value).AsGenericAppQuery();

		IAppQuery IAppQuery.Raw(string calabashQuery)
			=> SourceQuery.Raw(calabashQuery).AsGenericAppQuery();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1)
			=> SourceQuery.Raw(calabashQuery, arg1).AsGenericAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2).AsGenericAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2, arg3).AsGenericAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2, arg3, arg4).AsGenericAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2, arg3, arg4, arg5).AsGenericAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2, arg3, arg4, arg5, arg6).AsGenericAppTypedSelector();

		IAppQuery IAppQuery.Sibling(int index)
			=> SourceQuery.Sibling(index).AsGenericAppQuery();

		IAppQuery IAppQuery.Sibling(string className)
			=> SourceQuery.Sibling(className).AsGenericAppQuery();

		IAppQuery IAppQuery.Switch(string marked)
			=> SourceQuery.Switch(marked).AsGenericAppQuery();

		IAppQuery IAppQuery.Text(string text)
			=> SourceQuery.Text(text).AsGenericAppQuery();

		IAppQuery IAppQuery.TextField(string marked)
			=> SourceQuery.TextField(marked).AsGenericAppQuery();

		IAppQuery IAppQuery.WebView()
			=> SourceQuery.WebView().AsGenericAppQuery();

		IAppQuery IAppQuery.WebView(int index)
			=> SourceQuery.WebView(index).AsGenericAppQuery();

		IAppWebQuery IAppQuery.XPath(string xPathSelector)
			=> SourceQuery.XPath(xPathSelector).ToGenericAppWebQuery();
	}
}
