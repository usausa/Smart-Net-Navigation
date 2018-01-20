namespace Smart.Navigation
{
    using System.Windows.Forms;

    public static class ControlNavigatorConfigExtensions
    {
        public static NavigatorConfig UseControlNavigationProvider(this NavigatorConfig config, Control container)
        {
            return config.UseProvider(new ControlNavigationProvider(container));
        }
    }
}
