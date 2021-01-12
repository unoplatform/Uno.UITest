using System;
using System.Runtime.CompilerServices;
using Uno.UITest.Helpers.Queries;
using Uno.UITest.Xamarin.Extensions;

namespace Uno.UITest.Helpers.Queries
{
	public class QueryEx
	{
		public static readonly QueryEx Any = new QueryEx((IAppQuery x) => x.All(null));

		public static readonly QueryEx Visible = new QueryEx((IAppQuery x) => x);

		public static readonly QueryEx Button = QueryEx.Visible.Compose((IAppQuery x) => x.Button(null));

		public static readonly QueryEx Switch = QueryEx.Visible.Compose((IAppQuery x) => x.Switch(null));

		public static readonly QueryEx WebView = QueryEx.Visible.Compose((IAppQuery x) => x.WebView());

		public readonly Func<IAppQuery, IAppQuery> Unwrap;

		public static QueryEx Label
		{
			get
			{
				return Helpers.On<QueryEx>(QueryEx.Visible[".UILabel"], QueryEx.Visible[".android.widget.TextView"]);
			}
		}

		[Obsolete("Use Entry instead.")]
		public static QueryEx TextField
		{
			get
			{
				return QueryEx.Entry;
			}
		}

		public static QueryEx Entry
		{
			get
			{
				return QueryEx.Visible.Compose((IAppQuery x) => x.TextField(null));
			}
		}

		public static QueryEx Table
		{
			get
			{
				return Helpers.On<QueryEx>(QueryEx.Visible[".UITableView"], QueryEx.Visible[".android.widget.ListView"]);
			}
		}

		public static QueryEx Cell
		{
			get
			{
				return Helpers.On<QueryEx>(QueryEx.Table.Descendant("UITableViewCell"), QueryEx.Table.Child);
			}
		}

		public QueryEx Sibling
		{
			get
			{
				return this.Compose((IAppQuery x) => x.Sibling(null));
			}
		}

		public QueryEx Child
		{
			get
			{
				return this.Compose((IAppQuery x) => x.Child(null));
			}
		}

		public QueryEx Parent
		{
			get
			{
				return this.Compose((IAppQuery x) => x.Parent(null));
			}
		}

		public QueryEx this[string marked]
		{
			get
			{
				return this.MarkedOrPrefixedOverloaded(marked);
			}
		}

		public QueryEx this[int index]
		{
			get
			{
				return this.AtIndex(index);
			}
		}

		public QueryEx(Func<IAppQuery, IAppQuery> query)
		{
			this.Unwrap = query;
		}

		public static implicit operator QueryEx(string mark)
		{
			return QueryEx.Visible[mark];
		}

		public static implicit operator Func<IAppQuery, IAppQuery>(QueryEx query)
		{
			return query.Unwrap;
		}

		private QueryEx Compose(Func<IAppQuery, IAppQuery> q)
		{
			return new QueryEx((IAppQuery x) => q(this.Unwrap(x)));
		}

		public QueryEx WithClass(string @class)
		{
			return this.Compose((IAppQuery x) => x.Class(
				x is XamarinAppQuery
					? @class.Replace(".", "_")
					: @class));
		}

		public QueryEx Marked(string mark)
		{
			return this.Compose((IAppQuery x) => x.Marked(mark));
		}

		public QueryEx WithId(string id)
		{
			return this.Compose((IAppQuery x) => x.Id(id));
		}

		public QueryEx Descendant(string s)
		{
			return this.Compose((IAppQuery x) => x.Descendant(s));
		}

		public QueryEx Descendant(int i)
		{
			return this.Compose((IAppQuery x) => x.Descendant(i));
		}

		public QueryEx Descendant()
		{
			return this.Compose((IAppQuery x) => x.Descendant(null));
		}

		public QueryEx Raw(string raw)
		{
			return this.Compose((IAppQuery x) => x.Raw(raw));
		}

		public QueryEx WithPlaceholder(string placeholder)
		{
			return Helpers.On<QueryEx>(this.Raw($"* {{placeholder LIKE '{placeholder}'}}"), this.Raw(
				$"* {{hint LIKE '{placeholder}'}}"));
		}

		public QueryEx WithExactText(string text)
		{
			return this.Compose((IAppQuery x) => x.Text(text));
		}

		public QueryEx WithText(string text)
		{
			string arg = text.Replace("'", "\\'");
			return this.Descendant().Raw($"* {{text contains '{arg}'}}");
		}

		public QueryEx AtIndex(int i)
		{
			return this.Compose((IAppQuery x) => x.Index(i));
		}

		private QueryEx MarkedOrPrefixedOverloaded(string marked)
		{
			if (string.IsNullOrEmpty(marked))
			{
				throw new ArgumentException("Empty or null string", "marked");
			}
			if (marked.Length == 1)
			{
				return this.Marked(marked);
			}
			string text = marked.Substring(0, 1);
			string text2 = marked.Substring(1).Trim();
			if (text != null)
			{
				if (text == "#")
				{
					return this.WithId(text2);
				}
				if (text == ".")
				{
					return this.WithClass(text2);
				}
				if (text == "~")
				{
					return this.WithText(text2);
				}
			}
			return this.Marked(marked);
		}
	}
}
