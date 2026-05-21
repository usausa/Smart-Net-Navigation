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
        options.RegisterEffect(AvaloniaEffectKinds.None, NoneAvaloniaEffect.Instance);
        options.RegisterEffect(AvaloniaEffectKinds.Forward, new SlideHorizontalEffect(fromRight: true));
        options.RegisterEffect(AvaloniaEffectKinds.Back, new SlideHorizontalEffect(fromRight: false));
        options.RegisterEffect(AvaloniaEffectKinds.Push, new SlideVerticalEffect(fromBottom: true));
        options.RegisterEffect(AvaloniaEffectKinds.Pop, new SlideVerticalEffect(fromBottom: false));
        options.RegisterEffect(AvaloniaEffectKinds.Fade, new FadeEffect());

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
        options.RegisterEffect(AvaloniaEffectKinds.None, NoneAvaloniaEffect.Instance);
        options.RegisterEffect(AvaloniaEffectKinds.Forward, new SlideHorizontalEffect(fromRight: true));
        options.RegisterEffect(AvaloniaEffectKinds.Back, new SlideHorizontalEffect(fromRight: false));
        options.RegisterEffect(AvaloniaEffectKinds.Push, new SlideVerticalEffect(fromBottom: true));
        options.RegisterEffect(AvaloniaEffectKinds.Pop, new SlideVerticalEffect(fromBottom: false));
        options.RegisterEffect(AvaloniaEffectKinds.Fade, new FadeEffect());

        setupAction(options);

        config.Configure(static c =>
        {
            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));
        });

        return config.UseProvider(new AvaloniaNavigationProvider(new ContainerResolver(container), options));
    }
}
