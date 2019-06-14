namespace Uno.UITest
{
	public interface IPropertyQuery
	{
		QueryPlatform QueryPlatform { get; }

		IAppQuery Contains(string text);

		IAppQuery EndsWith(string text);

		IAppQuery Like(string text);

		IAppQuery Predicate(string predicate, string text);

		IAppQuery StartsWith(string text);

		IAppTypedSelector<T> Value<T>();
	}
}
