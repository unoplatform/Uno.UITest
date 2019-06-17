using System;
using System.Collections.Generic;

namespace Uno.UITest.Selenium
{
	internal class SeleniumAppTypedSelector<T> : SeleniumAppTypedSelector, IAppTypedSelector<T>
	{
		private readonly IAppQuery _parent;
		private readonly Type _selectorValueType;
		private readonly List<AppTypedSelectorMethodInvocation> _invocations
			= new List<AppTypedSelectorMethodInvocation>();

		public SeleniumAppTypedSelector(IAppQuery parent)
		{
			_parent = parent;
		}

		public SeleniumAppTypedSelector(IAppQuery parent, Type selectorValueType, IEnumerable<AppTypedSelectorMethodInvocation> invocations)
		{
			_parent = parent;
			_selectorValueType = selectorValueType;
			_invocations.AddRange(invocations);
		}

		internal IEnumerable<AppTypedSelectorMethodInvocation> Invocations { get => _invocations; }

		public IAppTypedSelector<object> Invoke(string methodName)
		{
			_invocations.Add(new AppTypedSelectorMethodInvocation(methodName));
			return new SeleniumAppTypedSelector<object>(_parent, null, _invocations);
		}

		public IAppTypedSelector<object> Invoke(string methodName, object arg1)
		{
			_invocations.Add(new AppTypedSelectorMethodInvocation(methodName, arg1));
			return new SeleniumAppTypedSelector<object>(_parent, null, _invocations);
		}

		public IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2)
		{
			_invocations.Add(new AppTypedSelectorMethodInvocation(methodName, arg1, arg2));
			return new SeleniumAppTypedSelector<object>(_parent, null, _invocations);
		}

		public IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2, object arg3)
		{
			_invocations.Add(new AppTypedSelectorMethodInvocation(methodName, arg1, arg2, arg3));
			return new SeleniumAppTypedSelector<object>(_parent, null, _invocations);
		}

		public IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2, object arg3, object arg4)
		{
			_invocations.Add(new AppTypedSelectorMethodInvocation(methodName, arg1, arg2, arg3, arg4));
			return new SeleniumAppTypedSelector<object>(_parent, null, _invocations);
		}

		public IAppTypedSelector<object> Invoke(string methodName, object arg1, object arg2, object arg3, object arg4, object arg5)
		{
			_invocations.Add(new AppTypedSelectorMethodInvocation(methodName, arg1, arg2, arg3, arg4, arg5));
			return new SeleniumAppTypedSelector<object>(_parent, null, _invocations);
		}

		public IAppTypedSelector<TResult> Value<TResult>()
			=> new SeleniumAppTypedSelector<TResult>(_parent, typeof(TResult), Invocations);

		public override Type SelectorValueType => _selectorValueType;

		public IAppQuery Parent => _parent;
	}
}
