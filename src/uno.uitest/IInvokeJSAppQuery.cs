namespace Uno.UITest
{
	public interface IInvokeJSAppQuery
	{
		string Javascript { get; }
		IAppQuery AppQuery { get; }
	}
}
