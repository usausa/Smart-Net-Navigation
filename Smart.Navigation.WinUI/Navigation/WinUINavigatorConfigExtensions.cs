namespace Smart.Navigation;

using Microsoft.UI.Xaml.Controls;

using Smart.Navigation.Effects;
using Smart.Navigation.Mappers;

public static class WinUINavigatorConfigExtensions
{
    public static NavigatorConfig UseWinUINavigationProvider(this NavigatorConfig config)
    {
        return config.UseWinUINavigationProvider(static _ => { });
    }

    public static NavigatorConfig UseWinUINavigationProvider(this NavigatorConfig config, Action<WinUINavigationProviderOptions> setupAction)
    {
        var options = new WinUINavigationProviderOptions();

        options.RegisterEffect(WinUIEffect.None, NoneWinUIEffect.Instance);
        options.RegisterEffect<ForwardEffect>(WinUIEffect.Forward);
        options.RegisterEffect<BackEffect>(WinUIEffect.Back);
        options.RegisterEffect<PushEffect>(WinUIEffect.Push);
        options.RegisterEffect<PopEffect>(WinUIEffect.Pop);
        options.RegisterEffect<FadeEffect>(WinUIEffect.Fade);

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

        return config.UseProvider<WinUINavigationProvider>();
    }

    public static NavigatorConfig UseWinUINavigationProvider(this NavigatorConfig config, Grid container)
    {
        return config.UseWinUINavigationProvider(container, static _ => { });
    }

    public static NavigatorConfig UseWinUINavigationProvider(this NavigatorConfig config, Grid container, Action<WinUINavigationProviderOptions> setupAction)
    {
        var options = new WinUINavigationProviderOptions();

        options.RegisterEffect(WinUIEffect.None, NoneWinUIEffect.Instance);
        options.RegisterEffect<ForwardEffect>(WinUIEffect.Forward);
        options.RegisterEffect<BackEffect>(WinUIEffect.Back);
        options.RegisterEffect<PushEffect>(WinUIEffect.Push);
        options.RegisterEffect<PopEffect>(WinUIEffect.Pop);
        options.RegisterEffect<FadeEffect>(WinUIEffect.Fade);

        setupAction(options);

        config.Configure(c =>
        {
            c.RemoveAll<IContainerResolver>();
            c.Add<IContainerResolver>(new ContainerResolver(container));

            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));

            c.Add(options);
        });

        return config.UseProvider<WinUINavigationProvider>();
    }
}
