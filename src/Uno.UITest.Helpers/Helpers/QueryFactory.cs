using Uno.UITest.Helpers.Queries;

namespace Uno.UITest.Helpers
{
	internal static class QueryExFactory
	{
		public static QueryEx BlankQuery()
		{
			return new QueryEx(q => q);
		}
	}
}
