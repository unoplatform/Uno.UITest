namespace Uno.UITest
{
	public interface IAppResult
	{
		string Id { get; }

		string Description { get; }

		IAppRect Rect { get; }

		string Label { get; }

		string Text { get; }

		string Class { get; }

		bool Enabled { get; }
	}
}
