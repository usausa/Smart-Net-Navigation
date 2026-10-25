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
        options.RegisterEffect(MauiEffect.None, NoneMauiEffect.Instance);
        options.RegisterEffect<ForwardEffect>(MauiEffect.Forward);
        options.RegisterEffect<BackEffect>(MauiEffect.Back);
        options.RegisterEffect<PushEffect>(MauiEffect.Push);
        options.RegisterEffect<PopEffect>(MauiEffect.Pop);
        options.RegisterEffect<FadeEffect>(MauiEffect.Fade);

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
        options.RegisterEffect(MauiEffect.None, NoneMauiEffect.Instance);
        options.RegisterEffect<ForwardEffect>(MauiEffect.Forward);
        options.RegisterEffect<BackEffect>(MauiEffect.Back);
        options.RegisterEffect<PushEffect>(MauiEffect.Push);
        options.RegisterEffect<PopEffect>(MauiEffect.Pop);
        options.RegisterEffect<FadeEffect>(MauiEffect.Fade);

        setupAction(options);

        config.Configure(c =>
        {
            c.RemoveAll<IContainerResolver>();
            c.Add<IContainerResolver>(new ContainerResolver(container));

            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(IView)));

            c.Add(options);
        });

        return config.UseProvider<MauiNavigationProvider>();
    }
}
