namespace Smart.Navigation;

using System.Windows.Controls;

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

        // Register standard animations
        options.RegisterAnimation(WindowsAnimationKinds.None, NoneWindowsAnimation.Instance);
        options.RegisterAnimation(WindowsAnimationKinds.Forward, new SlideHorizontalAnimation(fromRight: true));
        options.RegisterAnimation(WindowsAnimationKinds.Back, new SlideHorizontalAnimation(fromRight: false));
        options.RegisterAnimation(WindowsAnimationKinds.Push, new SlideVerticalAnimation(fromBottom: true));
        options.RegisterAnimation(WindowsAnimationKinds.Pop, new SlideVerticalAnimation(fromBottom: false));
        options.RegisterAnimation(WindowsAnimationKinds.Fade, new FadeAnimation());

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

        // Register standard animations
        options.RegisterAnimation(WindowsAnimationKinds.None, NoneWindowsAnimation.Instance);
        options.RegisterAnimation(WindowsAnimationKinds.Forward, new SlideHorizontalAnimation(fromRight: true));
        options.RegisterAnimation(WindowsAnimationKinds.Back, new SlideHorizontalAnimation(fromRight: false));
        options.RegisterAnimation(WindowsAnimationKinds.Push, new SlideVerticalAnimation(fromBottom: true));
        options.RegisterAnimation(WindowsAnimationKinds.Pop, new SlideVerticalAnimation(fromBottom: false));
        options.RegisterAnimation(WindowsAnimationKinds.Fade, new FadeAnimation());

        setupAction(options);

        config.Configure(static c =>
        {
            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));
        });

        return config.UseProvider(new WindowsNavigationProvider(new ContainerResolver(container), options));
    }
}
