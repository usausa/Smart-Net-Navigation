namespace Smart.Navigation;

using Avalonia.Controls;

using Smart.Navigation.Mappers;

public static class AvaloniaNavigatorConfigExtensions
{
    public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config)
    {
        return config.UseWindowsNavigationProvider(static _ => { });
    }

    public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, Action<AvaloniaNavigationProviderOptions> setupAction)
    {
        var options = new AvaloniaNavigationProviderOptions();
        setupAction(options);

        config.Configure(c =>
        {
            c.RemoveAll<IContainerResolver>();
            c.RemoveAll<IUpdateContainer>();

            var resolver = new ContainerResolver();
            c.Add<IContainerResolver>(resolver);
            c.Add<IUpdateContainer>(resolver);

            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));

            c.Add(options);
        });

        return config.UseProvider<AvaloniaNavigationProvider>();
    }

    public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, Canvas container)
    {
        return config.UseWindowsNavigationProvider(container, static _ => { });
    }

    public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, Canvas container, Action<AvaloniaNavigationProviderOptions> setupAction)
    {
        var options = new AvaloniaNavigationProviderOptions();
        setupAction(options);

        config.Configure(static c =>
        {
            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));
        });

        return config.UseProvider(new AvaloniaNavigationProvider(new ContainerResolver(container), options));
    }
}
