using System;
using Xamarin.UITest.Queries;

namespace Uno.UITest.Xamarin.Extensions
{
	internal class XamarinAppWebResult : IAppWebResult
	{
		private AppWebResult _source;

		public XamarinAppWebResult(AppWebResult result)
			=> this._source = result;

		string IAppWebResult.Id { get => _source.Id; set => _source.Id = value; }

		string IAppWebResult.NodeType { get => _source.NodeType; set => _source.NodeType = value; }

		string IAppWebResult.NodeName { get => _source.NodeName; set => _source.NodeName = value; }

		string IAppWebResult.Class { get => _source.Class; set => _source.Class = value; }

		string IAppWebResult.Html { get => _source.Html; set => _source.Html = value; }

		string IAppWebResult.Value { get => _source.Value; set => _source.Value = value; }

		string IAppWebResult.WebView { get => _source.WebView; set => _source.WebView = value; }

		string IAppWebResult.TextContent { get => _source.TextContent; set => _source.TextContent = value; }

		IAppWebRect IAppWebResult.Rect
		{
			get => new XamarinAppWebRect(_source.Rect);
			set => _source.Rect = value is XamarinAppWebRect wr ? wr.Source : throw new InvalidOperationException();
		}
	}
}
