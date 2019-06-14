namespace Uno.UITest
{
	public interface IAppTypedSelector<T>
	{
		IAppTypedSelector<object> Invoke(string methodName);

		IAppTypedSelector<object> Invoke(string methodName, object arg1);

		IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2);

		IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2, object arg3);

		IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2, object arg3, object arg4);

		IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5);

		IAppTypedSelector<TResult> Value<TResult>();
	}
}
