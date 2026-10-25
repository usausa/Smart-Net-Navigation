namespace Smart.Navigation;

using Avalonia.Controls;

using Smart.Navigation.Effects;
using Smart.Navigation.Mappers;

public static class AvaloniaNavigatorConfigExtensions
{
    public static NavigatorConfig UseAvaloniaNavigationProvider(this NavigatorConfig config)
    {
        return config.UseAvaloniaNavigationProvider(static _ => { });
    }

    public static NavigatorConfig UseAvaloniaNavigationProvider(this NavigatorConfig config, Action<AvaloniaNavigationProviderOptions> setupAction)
    {
        var options = new AvaloniaNavigationProviderOptions();

        // Register standard effects
        options.RegisterEffect(AvaloniaEffect.None, NoneAvaloniaEffect.Instance);
        options.RegisterEffect<ForwardEffect>(AvaloniaEffect.Forward);
        options.RegisterEffect<BackEffect>(AvaloniaEffect.Back);
        options.RegisterEffect<PushEffect>(AvaloniaEffect.Push);
        options.RegisterEffect<PopEffect>(AvaloniaEffect.Pop);
        options.RegisterEffect<FadeEffect>(AvaloniaEffect.Fade);

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

    public static NavigatorConfig UseAvaloniaNavigationProvider(this NavigatorConfig config, Canvas container)
    {
        return config.UseAvaloniaNavigationProvider(container, static _ => { });
    }

    public static NavigatorConfig UseAvaloniaNavigationProvider(this NavigatorConfig config, Canvas container, Action<AvaloniaNavigationProviderOptions> setupAction)
    {
        var options = new AvaloniaNavigationProviderOptions();

        // Register standard effects
        options.RegisterEffect(AvaloniaEffect.None, NoneAvaloniaEffect.Instance);
        options.RegisterEffect<ForwardEffect>(AvaloniaEffect.Forward);
        options.RegisterEffect<BackEffect>(AvaloniaEffect.Back);
        options.RegisterEffect<PushEffect>(AvaloniaEffect.Push);
        options.RegisterEffect<PopEffect>(AvaloniaEffect.Pop);
        options.RegisterEffect<FadeEffect>(AvaloniaEffect.Fade);

        setupAction(options);

        config.Configure(c =>
        {
            c.RemoveAll<IContainerResolver>();
            c.Add<IContainerResolver>(new ContainerResolver(container));

            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));

            c.Add(options);
        });

        return config.UseProvider<AvaloniaNavigationProvider>();
    }
}
