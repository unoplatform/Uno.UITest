using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
	public sealed partial class TextBox_Tests_02 : UserControl
	{
		public TextBox_Tests_02()
		{
			this.InitializeComponent();

			tb01.KeyUp += OnTb1KeyUp;
		}

		private void OnTb1KeyUp(object sender, KeyRoutedEventArgs args)
		{
			switch(args.Key)
			{
				case VirtualKey.Enter:
					tb01Events.Text += "Enter-Up;";
					break;
			}
		}
	}
}
