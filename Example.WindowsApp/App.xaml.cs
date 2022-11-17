namespace Example.WindowsApp;

using System.Windows;

using Example.WindowsApp.Modules;

using Smart.Navigation;
using Smart.Resolver;
using Smart.Windows.Resolver;

public partial class App
{
    private SmartResolver resolver = default!;

    private Navigator navigator = default!;

    protected override void OnStartup(StartupEventArgs e)
    {
        // Config Resolver
        resolver = CreateResolver();
        ResolveProvider.Default.Provider = resolver;

        // Config Navigator
        navigator = new NavigatorConfig()
            .UseWindowsNavigationProvider()
            .UseServiceProvider(resolver)
            .AddResolverPlugin()
            .ToNavigator();
        navigator.Navigated += (_, args) =>
        {
            // for debug
            System.Diagnostics.Debug.WriteLine(
                $"Navigated: [{args.Context.FromId}]->[{args.Context.ToId}] : stacked=[{navigator.StackedCount}]");
        };

        // Show MainWindow
        MainWindow = resolver.Get<MainWindow>();
        MainWindow.Show();

        navigator.Forward(typeof(MenuView));
    }

    private SmartResolver CreateResolver()
    {
        var config = new ResolverConfig()
            .UseAutoBinding()
            .UseArrayBinding()
            .UseAssignableBinding()
            .UsePropertyInjector();

        config.Bind<INavigator>().ToMethod(_ => navigator).InSingletonScope();

        return config.ToResolver();
    }
}
