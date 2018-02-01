namespace Smart.Navigation
{
    using System;
    using Xamarin.Forms;

    public static class FormsNavigatiorConfigExtensions
    {
        public static NavigatorConfig UseFormsNavigationProvider(this NavigatorConfig config)
        {
            config.Configure(c =>
            {
                c.RemoveAll<IContainerResolver>();
                c.RemoveAll<IUpdateContainer>();

                var resolver = new ContainerResolver();
                c.Add<IContainerResolver>(resolver);
                c.Add<IUpdateContainer>(resolver);

                c.Add<FormsNavigationProviderOptions>();
            });

            return config.UseProvider<FormsNavigationProvider>();
        }

        public static NavigatorConfig UseFormsNavigationProvider(this NavigatorConfig config, Action<FormsNavigationProviderOptions> setupAction)
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new FormsNavigationProviderOptions();
            setupAction(options);

            config.Configure(c =>
            {
                c.RemoveAll<IContainerResolver>();
                c.RemoveAll<IUpdateContainer>();

                var resolver = new ContainerResolver();
                c.Add<IContainerResolver>(resolver);
                c.Add<IUpdateContainer>(resolver);

                c.Add(options);
            });

            return config.UseProvider<FormsNavigationProvider>();
        }

        public static NavigatorConfig UseFormsNavigationProvider(this NavigatorConfig config, ContentView container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return config.UseProvider(new FormsNavigationProvider(new ContainerResolver(container), new FormsNavigationProviderOptions()));
        }

        public static NavigatorConfig UseFormsNavigationProvider(this NavigatorConfig config, ContentView container, Action<FormsNavigationProviderOptions> setupAction)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new FormsNavigationProviderOptions();
            setupAction(options);

            return config.UseProvider(new FormsNavigationProvider(new ContainerResolver(container), options));
        }
    }
}
