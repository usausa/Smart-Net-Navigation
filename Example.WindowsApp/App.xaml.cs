namespace Example.WindowsApp;

using System.Windows;

using Example.WindowsApp.Animation;
using Example.WindowsApp.Modules;
using Example.WindowsApp.Plugins;

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
                options.RegisterEffect(ExampleEffect.DialogOpen, new DialogOpenEffect());
                options.RegisterEffect(ExampleEffect.DialogClose, new DialogCloseEffect());
                options.RegisterEffect(ExampleEffect.Zoom, new ZoomEffect());
                options.RegisterEffect(ExampleEffect.Drop, new DropEffect());
                options.RegisterEffect(ExampleEffect.Flip, new FlipEffect());
                options.RegisterEffect(ExampleEffect.Rotate, new RotateEffect());
            })
            .UseServiceProvider(resolver)
            .AddResolverPlugin()
            .AddPlugin(new DialogEffectPlugin())
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
