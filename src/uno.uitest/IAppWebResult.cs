namespace Uno.UITest
{
	public interface IAppWebResult
	{
		string Id { get; set; }

		string NodeType { get; set; }

		string NodeName { get; set; }

		string Class { get; set; }

		string Html { get; set; }

		string Value { get; set; }

		string WebView { get; set; }

		string TextContent { get; set; }

		IAppWebRect Rect { get; set; }
	}
}
