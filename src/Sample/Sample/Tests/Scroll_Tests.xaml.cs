// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sample.Shared.Tests;

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
	public string? Name { get; set; }
}
