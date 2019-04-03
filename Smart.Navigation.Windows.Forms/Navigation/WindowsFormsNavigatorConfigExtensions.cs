namespace Smart.Navigation
{
    using System;
    using System.Windows.Forms;

    using Smart.Navigation.Mappers;

    public static class WindowsFormsNavigatorConfigExtensions
    {
        public static NavigatorConfig UseControlNavigationProvider(this NavigatorConfig config, Control container)
        {
            return config.UseControlNavigationProvider(container, action => { });
        }

        public static NavigatorConfig UseControlNavigationProvider(this NavigatorConfig config, Control container, Action<WindowsFormsNavigationProviderOptions> setupAction)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (setupAction is null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new WindowsFormsNavigationProviderOptions();
            setupAction(options);

            config.Configure(c =>
            {
                c.RemoveAll<ITypeConstraint>();
                c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));
            });

            return config.UseProvider(new WindowsFormsNavigationProvider(container, options));
        }
    }
}
