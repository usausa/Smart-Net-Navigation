namespace Smart.Navigation
{
    using System;

    using Smart.Navigation.Mappers;

    using Xamarin.Forms;

    public static class FormsNavigatiorConfigExtensions
    {
        public static NavigatorConfig UseFormsNavigationProvider(this NavigatorConfig config)
        {
            return config.UseFormsNavigationProvider(action => { });
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

                c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(View)));

                c.Add(options);
            });

            return config.UseProvider<FormsNavigationProvider>();
        }

        public static NavigatorConfig UseFormsNavigationProvider(this NavigatorConfig config, ContentView container)
        {
            return config.UseFormsNavigationProvider(container, action => { });
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

            config.Configure(c =>
            {
                c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(View)));
            });

            return config.UseProvider(new FormsNavigationProvider(new ContainerResolver(container), options));
        }
    }
}
