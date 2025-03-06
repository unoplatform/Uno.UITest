// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sample.Shared.Tests;

public sealed partial class AppDataMode_Tests : UserControl
{
	public AppDataMode_Tests()
	{
		this.InitializeComponent();
	}

	private void OnSetLocalSettingClick(object sender, RoutedEventArgs e)
	{
		ApplicationData.Current.LocalSettings.Values["MySetting"] = "MyValue";
	}

	private void OnGetLocalSettingClick(object sender, RoutedEventArgs e)
	{
		if(ApplicationData.Current.LocalSettings.Values.TryGetValue("MySetting", out var value))
		{
			LocalSettingValueTextBlock.Text = value.ToString();
		}
		else
		{
			LocalSettingValueTextBlock.Text = "<NOT_SET>";
		}
	}
}
