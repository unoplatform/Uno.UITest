namespace Uno.UITest.Selenium
{
	internal class AppTypedSelectorMethodInvocation
	{
		private readonly string _methodName;
		private readonly object[] _args;

		public AppTypedSelectorMethodInvocation(string methodName, params object[] args)
		{
			_methodName = methodName;
			_args = args;
		}

		public string MethodName => _methodName;
		public object[] Args => _args;
	}
}
