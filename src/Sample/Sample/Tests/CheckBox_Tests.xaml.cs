using Microsoft.UI.Xaml.Shapes;
using Microsoft.UI;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sample.Shared.Tests;

public sealed partial class CheckBox_Tests : UserControl
{
	public CheckBox_Tests()
	{
		this.InitializeComponent();

		cb1.Checked += (snd, evt) =>
			checked1.Child = new Rectangle {Name = "rect1", Fill = new SolidColorBrush(Colors.Red)};
		cb1.Unchecked += (snd, evt) =>
			checked1.Child = null;

		cb2.Checked += (snd, evt) =>
			checked2.Child = new Rectangle {Name = "rect2", Fill = new SolidColorBrush(Colors.Blue)};
		cb2.Unchecked += (snd, evt) =>
			checked2.Child = null;
	}
}
