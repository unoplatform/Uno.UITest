using System;
using System.Runtime.CompilerServices;

namespace Uno.UITest.Helpers.Queries
{
	public class BackdoorProxy
	{
		private const string DefaultId = "Xamarin.UITest.Backdoor";

		private readonly string Id;

		public BackdoorProxy this[string id] => this.WithId(id);

		public BackdoorProxy(string id = "Xamarin.UITest.Backdoor")
		{
			this.Id = id;
		}

		public void Invoke(string method, string value = "")
		{
			QueryEx.Any.WithId(this.Id).Invoke("backdoorInvoke", string.Format("{0}:{1}", method, value));
		}

		public BackdoorProxy WithId(string id)
		{
			return new BackdoorProxy(id);
		}
	}
}
