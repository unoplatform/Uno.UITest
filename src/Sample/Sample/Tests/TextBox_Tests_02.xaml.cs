using Windows.System;
using Microsoft.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sample.Shared.Tests;

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
