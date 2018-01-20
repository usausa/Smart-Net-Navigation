namespace Smart.Navigation
{
    using System.Windows.Forms;

    public static class WindowsFormsNavigatorConfigExtensions
    {
        public static NavigatorConfig UseControlNavigationProvider(this NavigatorConfig config, Control container)
        {
            return config.UseProvider(new WindowsFormsNavigationProvider(container));
        }
    }
}
