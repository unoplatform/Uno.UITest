using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Uno.UITest.Selenium
{
	internal class SeleniumAppQuery : IAppQuery
	{
		private SeleniumApp seleniumApp;

		private readonly List<QueryItem> _queryItems = new List<QueryItem>();

		internal List<QueryItem> QueryItems => _queryItems;

		internal class QueryItem { }

		internal class SearchQueryItem : QueryItem
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

		public SeleniumAppQuery(SeleniumApp seleniumApp)
			=> this.seleniumApp = seleniumApp;

		IAppQuery IAppQuery.All(string className)
			=> string.IsNullOrEmpty(className)
				? Apply(() => { })
				: Apply(() => _queryItems.Add(new SearchQueryItem($"//*[ends-with(@xamltype, '{className}')]")));

		IAppQuery IAppQuery.Button(string marked)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"//*[@xamltype='Windows.UI.Xaml.Controls.Button' and (xamlname='{marked}') or @xuid='{marked}')]")));

		IAppQuery IAppQuery.Child(string className)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"//*[ends-with(@xamltype, '{className}')]")));

		IAppQuery IAppQuery.Child(int index)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"/[{index}]")));

		IAppQuery IAppQuery.Class(string className)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"//*[ends-with(@xamltype, '{className}')]")));

		IAppQuery IAppQuery.ClassFull(string className)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"//*[@xamltype='{className}']")));

		IAppQuery IAppQuery.Descendant(int index) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Descendant(string className)
			=> string.IsNullOrEmpty(className)
				? Apply(() => { })
				: Apply(() => _queryItems.Add(new SearchQueryItem($"//*[ends-with(@xamltype, '{className}')]")));

		IAppQuery IAppQuery.Frame(string cssSelector) => throw new System.NotImplementedException();

		IAppQuery IAppQuery.Id(string id)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"//*[id='{id}')]")));

		IAppQuery IAppQuery.Id(int id)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"//*[id='{id}')]")));

		IAppQuery IAppQuery.Index(int index) => throw new System.NotImplementedException();

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName)
		{
			var selector = new SeleniumAppTypedSelector<object>(this);
			selector.Invoke(methodName);
			return selector;
		}

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1)
		{
			var selector = new SeleniumAppTypedSelector<object>(this);
			selector.Invoke(methodName, arg1);

			return selector;
		}

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2)
		{
			var selector = new SeleniumAppTypedSelector<object>(this);
			selector.Invoke(methodName, arg1, arg2);
			return selector;
		}

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3)
		{
			var selector = new SeleniumAppTypedSelector<object>(this);
			selector.Invoke(methodName, arg1, arg2, arg3);
			return selector;
		}

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4)
		{
			var selector = new SeleniumAppTypedSelector<object>(this);
			selector.Invoke(methodName, arg1, arg2, arg3, arg4);
			return selector;
		}

		IAppTypedSelector<object> IAppQuery.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
		{
			var selector = new SeleniumAppTypedSelector<object>(this);
			selector.Invoke(methodName, arg1, arg2, arg3, arg4, arg5);
			return selector;
		}

		IInvokeJSAppQuery IAppQuery.InvokeJS(string javascript) => throw new System.NotImplementedException();

		IAppQuery IAppQuery.Marked(string text)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"//*[@xamlname='{text}' or @xuid='{text}' or @xamlautomationid='{text}']")));

		IAppQuery IAppQuery.Parent(string className)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"./ancestor::*[ends-with(@xamltype, {className})]")));

		IAppQuery IAppQuery.Parent(int index)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"./ancestor::*[position()={index}]")));

		IPropertyAppQuery IAppQuery.Property(string propertyName) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Property(string propertyName, string value) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Property(string propertyName, bool value) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Property(string propertyName, int value) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Sibling(int index) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Sibling(string className) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Switch(string marked) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Text(string text)
			=> Apply(() => _queryItems.Add(new SearchQueryItem($"//*[text()='{text}']")));

		IAppQuery IAppQuery.TextField(string marked) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.WebView() => throw new System.NotImplementedException();
		IAppQuery IAppQuery.WebView(int index) => throw new System.NotImplementedException();
		IAppWebQuery IAppQuery.XPath(string xPathSelector) => throw new System.NotImplementedException();


		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2) => throw new System.NotImplementedException();
		IAppQuery IAppQuery.Raw(string calabashQuery)
		{
			var match = Regex.Match(calabashQuery, @"(.*)\s(marked|text):'((.|\n)*)'");

			var controlType = match.Groups[1].Captures[0].Value;
			var type = match.Groups[2].Captures[0].Value;
			var value = match.Groups[3].Captures[0].Value;

			if(controlType == "*")
			{
				switch(type)
				{
					case "marked":
						return ((IAppQuery)this).Marked(value);
					case "text":
						return ((IAppQuery)this).Text(value);
				}
			}

			throw new NotSupportedException($"Raw Query is not supported [{calabashQuery}]");
		}

		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4) => throw new System.NotImplementedException();
		IAppTypedSelector<string> IAppQuery.Raw(string calabashQuery, object arg1) => throw new System.NotImplementedException();
	}
}
