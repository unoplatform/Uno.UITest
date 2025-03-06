// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sample.Shared.Tests;

public sealed partial class RadioButton_Tests_01 : UserControl
{
	public RadioButton_Tests_01()
	{
		this.InitializeComponent();
	}

	private void Radio_Checked(object sender, RoutedEventArgs e)
	{
		if(sender is RadioButton rb)
		{
			result.Text = rb.Content?.ToString();
		}
	}
}
