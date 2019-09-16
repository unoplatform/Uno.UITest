using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin
{
	internal class XamarinAppTypedSelector<T> : IAppTypedSelector<T>
	{
		private AppTypedSelector<object> _source;

		public XamarinAppTypedSelector(AppTypedSelector<object> selector)
			=> _source = selector;

		public AppTypedSelector<object> Source => _source;
			
		IAppTypedSelector<object> IAppTypedSelector<T>.Invoke(string methodName)
			=> new XamarinAppTypedSelector<object>(_source.Invoke(methodName));

		IAppTypedSelector<object> IAppTypedSelector<T>.Invoke(string methodName, object arg1)
			=> new XamarinAppTypedSelector<object>(_source.Invoke(methodName, arg1));

		IAppTypedSelector<object> IAppTypedSelector<T>.Invoke(string methodName, object arg1, object arg2)
			=> new XamarinAppTypedSelector<object>(_source.Invoke(methodName, arg1, arg2));

		IAppTypedSelector<object> IAppTypedSelector<T>.Invoke(string methodName, object arg1, object arg2, object arg3)
			=> new XamarinAppTypedSelector<object>(_source.Invoke(methodName, arg1, arg2, arg3));

		IAppTypedSelector<object> IAppTypedSelector<T>.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4)
			=> new XamarinAppTypedSelector<object>(_source.Invoke(methodName, arg1, arg2, arg3, arg4));

		IAppTypedSelector<object> IAppTypedSelector<T>.Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
			=> new XamarinAppTypedSelector<object>(_source.Invoke(methodName, arg1, arg2, arg3, arg4, arg5));

		IAppTypedSelector<TResult> IAppTypedSelector<T>.Value<TResult>()
			=> new XamarinAppTypedSelector<TResult>(_source.Value<object>());
	}
}
