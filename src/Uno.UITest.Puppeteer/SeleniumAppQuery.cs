using System;
using System.Collections.Generic;

namespace Uno.UITest.Puppeteer
{
	internal class SeleniumAppQuery : IAppQuery
	{
		private SeleniumApp seleniumApp;

		private readonly List<QueryItem> _queryItems = new List<QueryItem>();

		private class QueryItem { }

		private class SearchQueryItem : QueryItem
		{
			public SearchQueryItem(string query)
			{
				Query = query;
			}

			public string Query { get; }
		}

		private IAppQuery Apply(Action a)
		{
			a();
			return this;
		}

		public SeleniumAppQuery(SeleniumApp seleniumApp) => this.seleniumApp = seleniumApp;

		IAppQuery IAppQuery.All(string className)
			=> Apply(() => _queryItems.Add(new SearchQueryItem("//*[@xamlname='{xamlName}']")));

		IAppQuery IAppQuery.Button(string marked) => throw new System.NotImplementedException();

		IAppQuery IAppQuery.Child(string className) => throw new System.NotImplementedException();

		IAppQuery IAppQuery.Child(int index) => throw new System.NotImplementedException();

		IAppQuery IAppQuery.Class(string className) => throw new System.NotImplementedException();

		IAppQuery IAppQuery.ClassFull(string className) => throw new System.NotImplementedException();

		IAppQuery IAppQuery.Descendant(int index) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Descendant(string className) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Frame(string cssSelector) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Id(string id) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Id(int id) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Index(int index) => throw new System.NotImplementedException();
		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4) => throw new System.NotImplementedException();
		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5) => throw new System.NotImplementedException();
		IAppTypedSelector<object> IAppQuery.Invoke(string methodName) => throw new System.NotImplementedException();
		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3) => throw new System.NotImplementedException();
		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1) => throw new System.NotImplementedException();
		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2) => throw new System.NotImplementedException();
		IInvokeJSAppQuery IAppQuery.InvokeJS(string javascript) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Marked(string text) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Parent(string className) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Parent(int index) => throw new System.NotImplementedException();
		IPropertyQuery IAppQuery.Property(string propertyName) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Property(string propertyName, string value) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Property(string propertyName, bool value) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Property(string propertyName, int value) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Raw(string calabashQuery) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Sibling(int index) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Sibling(string className) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Switch(string marked) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Text(string text) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.TextField(string marked) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.WebView() => throw new System.NotImplementedException();
		IAppQuery IAppQuery.WebView(int index) => throw new System.NotImplementedException();
		IAppWebQuery IAppQuery.XPath(string xPathSelector) => throw new System.NotImplementedException();
	}
}
