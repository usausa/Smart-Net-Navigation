namespace Example.WindowsApp;

using System.Windows;

using Example.WindowsApp.Animation;
using Example.WindowsApp.Modules;

using Smart.Mvvm.Resolver;
using Smart.Navigation;
using Smart.Resolver;

public sealed partial class App
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
            .UseWindowsNavigationProvider(static options =>
            {
                options.RegisterAnimation(ExampleAnimationKinds.Dialog, new DialogAnimation());
                options.RegisterAnimation(ExampleAnimationKinds.Zoom, new ZoomAnimation());
                options.RegisterAnimation(ExampleAnimationKinds.Drop, new DropAnimation());
                options.RegisterAnimation(ExampleAnimationKinds.Flip, new FlipAnimation());
                options.RegisterAnimation(ExampleAnimationKinds.Rotate, new RotateAnimation());
            })
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
