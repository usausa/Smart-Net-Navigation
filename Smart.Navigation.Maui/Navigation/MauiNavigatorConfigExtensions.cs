namespace Smart.Navigation;

using Smart.Navigation.Effects;
using Smart.Navigation.Mappers;

public static class MauiNavigatorConfigExtensions
{
    public static NavigatorConfig UseMauiNavigationProvider(this NavigatorConfig config)
    {
        return config.UseMauiNavigationProvider(static _ => { });
    }

    public static NavigatorConfig UseMauiNavigationProvider(this NavigatorConfig config, Action<MauiNavigationProviderOptions> setupAction)
    {
        var options = new MauiNavigationProviderOptions();

        // Register standard effects
        options.RegisterEffect(MauiEffectKinds.None, NoneMauiEffect.Instance);
        options.RegisterEffect(MauiEffectKinds.Forward, new SlideHorizontalEffect(fromRight: true));
        options.RegisterEffect(MauiEffectKinds.Back, new SlideHorizontalEffect(fromRight: false));
        options.RegisterEffect(MauiEffectKinds.Push, new SlideVerticalEffect(fromBottom: true));
        options.RegisterEffect(MauiEffectKinds.Pop, new SlideVerticalEffect(fromBottom: false));
        options.RegisterEffect(MauiEffectKinds.Fade, new FadeEffect());

        setupAction(options);

        config.Configure(c =>
        {
            c.RemoveAll<IContainerResolver>();
            c.RemoveAll<IUpdateContainer>();

            var resolver = new ContainerResolver();
            c.Add<IContainerResolver>(resolver);
            c.Add<IUpdateContainer>(resolver);

            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(View)));

            c.Add(options);
        });

        return config.UseProvider<MauiNavigationProvider>();
    }

    public static NavigatorConfig UseMauiNavigationProvider(this NavigatorConfig config, AbsoluteLayout container)
    {
        return config.UseMauiNavigationProvider(container, static _ => { });
    }

    public static NavigatorConfig UseMauiNavigationProvider(this NavigatorConfig config, AbsoluteLayout container, Action<MauiNavigationProviderOptions> setupAction)
    {
        var options = new MauiNavigationProviderOptions();

        // Register standard effects
        options.RegisterEffect(MauiEffectKinds.None, NoneMauiEffect.Instance);
        options.RegisterEffect(MauiEffectKinds.Forward, new SlideHorizontalEffect(fromRight: true));
        options.RegisterEffect(MauiEffectKinds.Back, new SlideHorizontalEffect(fromRight: false));
        options.RegisterEffect(MauiEffectKinds.Push, new SlideVerticalEffect(fromBottom: true));
        options.RegisterEffect(MauiEffectKinds.Pop, new SlideVerticalEffect(fromBottom: false));
        options.RegisterEffect(MauiEffectKinds.Fade, new FadeEffect());

        setupAction(options);

        config.Configure(static c =>
        {
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(IView)));
        });

        return config.UseProvider(new MauiNavigationProvider(new ContainerResolver(container), options));
    }
}
