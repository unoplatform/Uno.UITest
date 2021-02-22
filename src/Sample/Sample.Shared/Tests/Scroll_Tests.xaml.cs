using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sample.Shared.Tests
{
	public sealed partial class Scroll_Tests : UserControl
	{
		public Scroll_Tests()
		{
			this.InitializeComponent();
		}

		private IEnumerable<MyData> GetData()
		{
			for (int i = 0; i < 100; i++)
			{
				yield return new MyData { Name = $"Data_Item_{i + 1}" };
			}
		}
	}

	public class MyData
	{
		public string Name { get; set; }
	}
}
