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
        options.RegisterEffect(WindowsEffectKinds.None, NoneWindowsEffect.Instance);
        options.RegisterEffect(WindowsEffectKinds.Forward, new SlideHorizontalEffect(fromRight: true));
        options.RegisterEffect(WindowsEffectKinds.Back, new SlideHorizontalEffect(fromRight: false));
        options.RegisterEffect(WindowsEffectKinds.Push, new SlideVerticalEffect(fromBottom: true));
        options.RegisterEffect(WindowsEffectKinds.Pop, new SlideVerticalEffect(fromBottom: false));
        options.RegisterEffect(WindowsEffectKinds.Fade, new FadeEffect());

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
        options.RegisterEffect(WindowsEffectKinds.None, NoneWindowsEffect.Instance);
        options.RegisterEffect(WindowsEffectKinds.Forward, new SlideHorizontalEffect(fromRight: true));
        options.RegisterEffect(WindowsEffectKinds.Back, new SlideHorizontalEffect(fromRight: false));
        options.RegisterEffect(WindowsEffectKinds.Push, new SlideVerticalEffect(fromBottom: true));
        options.RegisterEffect(WindowsEffectKinds.Pop, new SlideVerticalEffect(fromBottom: false));
        options.RegisterEffect(WindowsEffectKinds.Fade, new FadeEffect());

        setupAction(options);

        config.Configure(static c =>
        {
            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));
        });

        return config.UseProvider(new WindowsNavigationProvider(new ContainerResolver(container), options));
    }
}
