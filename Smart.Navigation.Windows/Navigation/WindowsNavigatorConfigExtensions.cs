namespace Smart.Navigation;

using System.Windows.Controls;

using Smart.Navigation.Effects;
using Smart.Navigation.Mappers;

public static class WindowsNavigatorConfigExtensions
{
    public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config)
    {
        return config.UseWindowsNavigationProvider(static _ => { });
    }

    public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, Action<WindowsNavigationProviderOptions> setupAction)
    {
        var options = new WindowsNavigationProviderOptions();

        // Register standard effects
        options.RegisterEffect(WindowsEffect.None, NoneWindowsEffect.Instance);
        options.RegisterEffect<ForwardEffect>(WindowsEffect.Forward);
        options.RegisterEffect<BackEffect>(WindowsEffect.Back);
        options.RegisterEffect<PushEffect>(WindowsEffect.Push);
        options.RegisterEffect<PopEffect>(WindowsEffect.Pop);
        options.RegisterEffect<FadeEffect>(WindowsEffect.Fade);

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

        return config.UseProvider<WindowsNavigationProvider>();
    }

    public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, Canvas container)
    {
        return config.UseWindowsNavigationProvider(container, static _ => { });
    }

    public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, Canvas container, Action<WindowsNavigationProviderOptions> setupAction)
    {
        var options = new WindowsNavigationProviderOptions();

        // Register standard effects
        options.RegisterEffect(WindowsEffect.None, NoneWindowsEffect.Instance);
        options.RegisterEffect<ForwardEffect>(WindowsEffect.Forward);
        options.RegisterEffect<BackEffect>(WindowsEffect.Back);
        options.RegisterEffect<PushEffect>(WindowsEffect.Push);
        options.RegisterEffect<PopEffect>(WindowsEffect.Pop);
        options.RegisterEffect<FadeEffect>(WindowsEffect.Fade);

        setupAction(options);

        config.Configure(c =>
        {
            c.RemoveAll<IContainerResolver>();
            c.Add<IContainerResolver>(new ContainerResolver(container));

            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));

            c.Add(options);
        });

        return config.UseProvider<WindowsNavigationProvider>();
    }
}
