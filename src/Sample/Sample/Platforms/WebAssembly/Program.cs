namespace Sample;

public class Program
{
    private static App? _app;

    public static int Main(string[] args)
    {
		FeatureConfiguration.UIElement.AssignDOMXamlName = true;

		App.InitializeLogging();

        Microsoft.UI.Xaml.Application.Start(_ => _app = new App());

        return 0;
    }
}
