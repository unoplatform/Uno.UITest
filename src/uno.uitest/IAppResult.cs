namespace Uno.UITest
{
	public interface IAppResult
	{
		string Id { get; set; }

		string Description { get; set; }

		IAppRect Rect { get; set; }

		string Label { get; set; }

		string Text { get; set; }

		string Class { get; set; }

		bool Enabled { get; set; }
	}
}
