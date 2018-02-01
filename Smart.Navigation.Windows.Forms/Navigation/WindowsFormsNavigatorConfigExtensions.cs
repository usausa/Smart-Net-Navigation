namespace Smart.Navigation
{
    using System;
    using System.Windows.Forms;

    public static class WindowsFormsNavigatorConfigExtensions
    {
        public static NavigatorConfig UseControlNavigationProvider(this NavigatorConfig config, Control container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return config.UseProvider(new WindowsFormsNavigationProvider(container, new WindowsFormsNavigationProviderOptions()));
        }

        public static NavigatorConfig UseControlNavigationProvider(this NavigatorConfig config, Control container, Action<WindowsFormsNavigationProviderOptions> setupAction)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var options = new WindowsFormsNavigationProviderOptions();
            setupAction(options);

            return config.UseProvider(new WindowsFormsNavigationProvider(container, options));
        }
    }
}
