using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	internal class XamarinAppQuery : IAppQuery
	{
		private AppQuery _sourceQuery;

		public XamarinAppQuery(AppQuery q) => SourceQuery = q;

		public AppQuery SourceQuery { get => _sourceQuery; set => _sourceQuery = value; }

		IAppQuery IAppQuery.All(string className)
			=> SourceQuery.All(className).AsUnoAppQuery();

		IAppQuery IAppQuery.Button(string marked)
			=> SourceQuery.Button(marked).AsUnoAppQuery();

		IAppQuery IAppQuery.Child(string className)
			=> SourceQuery.Child(className).AsUnoAppQuery();

		IAppQuery IAppQuery.Child(int index)
			=> SourceQuery.Child(index).AsUnoAppQuery();

		IAppQuery IAppQuery.Class(string className)
			=> SourceQuery.Class(className).AsUnoAppQuery();

		IAppQuery IAppQuery.ClassFull(string className)
			=> SourceQuery.ClassFull(className).AsUnoAppQuery();

		IAppQuery IAppQuery.Descendant(int index)
			=> SourceQuery.Descendant(index).AsUnoAppQuery();

		IAppQuery IAppQuery.Descendant(string className)
			=> SourceQuery.Descendant(className).AsUnoAppQuery();

		IAppQuery IAppQuery.Frame(string cssSelector)
			=> SourceQuery.Frame(cssSelector).AsUnoAppQuery();

		IAppQuery IAppQuery.Id(string id)
			=> SourceQuery.Id(id).AsUnoAppQuery();

		IAppQuery IAppQuery.Id(int id)
			=> SourceQuery.Id(id).AsUnoAppQuery();

		IAppQuery IAppQuery.Index(int index)
			=> SourceQuery.Index(index).AsUnoAppQuery();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName)
			=> SourceQuery.Invoke(methodName).AsUnoAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1)
			=> SourceQuery.Invoke(methodName, arg1).AsUnoAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2)
			=> SourceQuery.Invoke(methodName, arg1, arg2).AsUnoAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3)
			=> SourceQuery.Invoke(methodName, arg1, arg2, arg3).AsUnoAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4)
			=> SourceQuery.Invoke(methodName, arg1, arg2, arg3, arg4).AsUnoAppTypedSelector();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
			=> SourceQuery.Invoke(methodName, arg1, arg2, arg3, arg4, arg5).AsUnoAppTypedSelector();

		IInvokeJSAppQuery IAppQuery.InvokeJS(string javascript)
			=> SourceQuery.InvokeJS(javascript).AsUnoInvokeJSAppQuery();

		IAppQuery IAppQuery.Marked(string text)
			=> SourceQuery.Marked(text).AsUnoAppQuery();

		IAppQuery IAppQuery.Parent(string className)
			=> SourceQuery.Parent(className).AsUnoAppQuery();

		IAppQuery IAppQuery.Parent(int index)
			=> SourceQuery.Parent(index).AsUnoAppQuery();

		IPropertyQuery IAppQuery.Property(string propertyName)
			=> SourceQuery.Property(propertyName).AsUnoPropertyQuery();

		IAppQuery IAppQuery.Property(string propertyName, string value)
			=> SourceQuery.Property(propertyName, value).AsUnoAppQuery();

		IAppQuery IAppQuery.Property(string propertyName, bool value)
			=> SourceQuery.Property(propertyName, value).AsUnoAppQuery();

		IAppQuery IAppQuery.Property(string propertyName, int value)
			=> SourceQuery.Property(propertyName, value).AsUnoAppQuery();

		IAppQuery IAppQuery.Raw(string calabashQuery)
			=> SourceQuery.Raw(calabashQuery).AsUnoAppQuery();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1)
			=> SourceQuery.Raw(calabashQuery, arg1).AsUnoAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2).AsUnoAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2, arg3).AsUnoAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2, arg3, arg4).AsUnoAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2, arg3, arg4, arg5).AsUnoAppTypedSelector();

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
			=> SourceQuery.Raw(calabashQuery, arg1, arg2, arg3, arg4, arg5, arg6).AsUnoAppTypedSelector();

		IAppQuery IAppQuery.Sibling(int index)
			=> SourceQuery.Sibling(index).AsUnoAppQuery();

		IAppQuery IAppQuery.Sibling(string className)
			=> SourceQuery.Sibling(className).AsUnoAppQuery();

		IAppQuery IAppQuery.Switch(string marked)
			=> SourceQuery.Switch(marked).AsUnoAppQuery();

		IAppQuery IAppQuery.Text(string text)
			=> SourceQuery.Text(text).AsUnoAppQuery();

		IAppQuery IAppQuery.TextField(string marked)
			=> SourceQuery.TextField(marked).AsUnoAppQuery();

		IAppQuery IAppQuery.WebView()
			=> SourceQuery.WebView().AsUnoAppQuery();

		IAppQuery IAppQuery.WebView(int index)
			=> SourceQuery.WebView(index).AsUnoAppQuery();

		IAppWebQuery IAppQuery.XPath(string xPathSelector)
			=> SourceQuery.XPath(xPathSelector).ToUnoQuery();
	}
}
