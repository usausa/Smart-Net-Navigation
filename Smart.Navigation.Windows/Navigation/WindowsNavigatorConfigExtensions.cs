namespace Smart.Navigation
{
    using System;
    using System.Windows.Controls;

    using Smart.Navigation.Mappers;

    public static class WindowsNavigatorConfigExtensions
    {
        public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config)
        {
            return config.UseWindowsNavigationProvider(action => { });
        }

        public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, Action<WindowsNavigationProviderOptions> setupAction)
        {
            if (setupAction is null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new WindowsNavigationProviderOptions();
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
            return config.UseWindowsNavigationProvider(container, action => { });
        }

        public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, Canvas container, Action<WindowsNavigationProviderOptions> setupAction)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (setupAction is null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new WindowsNavigationProviderOptions();
            setupAction(options);

            config.Configure(c =>
            {
                c.RemoveAll<ITypeConstraint>();
                c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));
            });

            return config.UseProvider(new WindowsNavigationProvider(new ContainerResolver(container), options));
        }
    }
}