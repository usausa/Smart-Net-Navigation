namespace Smart.Navigation
{
    using System;
    using System.Windows.Controls;

    using Smart.Navigation.Mappers;

    public static class WindowsNavigatorConfigExtensions
    {
        public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config)
        {
            config.Configure(c =>
            {
                c.RemoveAll<IContainerResolver>();
                c.RemoveAll<IUpdateContainer>();

                var resolver = new ContainerResolver();
                c.Add<IContainerResolver>(resolver);
                c.Add<IUpdateContainer>(resolver);

                c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));

                c.Add<WindowsNavigationProviderOptions>();
            });

            return config.UseProvider<WindowsNavigationProvider>();
        }

        public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, Action<WindowsNavigationProviderOptions> setupAction)
        {
            if (setupAction == null)
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

                c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));

                c.Add(options);
            });

            return config.UseProvider<WindowsNavigationProvider>();
        }

        public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, ContentControl container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return config.UseProvider(new WindowsNavigationProvider(new ContainerResolver(container), new WindowsNavigationProviderOptions()));
        }

        public static NavigatorConfig UseWindowsNavigationProvider(this NavigatorConfig config, ContentControl container, Action<WindowsNavigationProviderOptions> setupAction)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new WindowsNavigationProviderOptions();
            setupAction(options);

            return config.UseProvider(new WindowsNavigationProvider(new ContainerResolver(container), options));
        }
    }
}