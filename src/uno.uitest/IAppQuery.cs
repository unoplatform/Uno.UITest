using System;
using System.Collections.Generic;
using System.Text;

namespace Uno.UITest
{
	public interface IAppQuery
	{
		IAppQuery All(string className = null);
		IAppQuery Button(string marked = null);
		IAppQuery Child(string className = null);
		IAppQuery Child(int index);
		IAppQuery Class(string className);
		IAppQuery ClassFull(string className);
		IAppQuery Descendant(int index);
		IAppQuery Descendant(string className = null);
		IAppQuery Frame(string cssSelector);
		IAppQuery Id(string id);
		IAppQuery Id(int id);
		IAppQuery Index(int index);
		IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2, object arg3, object arg4);
		IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5);
		IAppTypedSelector<object> Invoke(string methodName);
		IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2, object arg3);
		IAppTypedSelector<object> Invoke(string methodName, object arg1);
		IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2);
		IInvokeJSAppQuery InvokeJS(string javascript);
		IAppQuery Marked(string text);
		IAppQuery Parent(string className = null);
		IAppQuery Parent(int index);
		IPropertyAppQuery Property(string propertyName);
		IAppQuery Property(string propertyName, string value);
		IAppQuery Property(string propertyName, bool value);
		IAppQuery Property(string propertyName, int value);
		IAppTypedSelector<string> Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5);
		IAppTypedSelector<string> Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6);
		IAppTypedSelector<string> Raw(string calabashQuery, object arg1, object arg2, object arg3);
		IAppTypedSelector<string> Raw(string calabashQuery, object arg1, object arg2);
		IAppQuery Raw(string calabashQuery);
		IAppTypedSelector<string> Raw(string calabashQuery, object arg1, object arg2, object arg3, object arg4);
		IAppTypedSelector<string> Raw(string calabashQuery, object arg1);
		IAppQuery Sibling(int index);
		IAppQuery Sibling(string className = null);
		IAppQuery Switch(string marked = null);
		IAppQuery Text(string text);
		IAppQuery TextField(string marked = null);
		IAppQuery WebView();
		IAppQuery WebView(int index);
		IAppWebQuery XPath(string xPathSelector);
	}
}
