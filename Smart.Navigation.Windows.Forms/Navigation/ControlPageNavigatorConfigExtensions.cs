namespace Smart.Navigation
{
    using System.Windows.Forms;

    public static class ControlPageNavigatorConfigExtensions
    {
        public static NavigatorConfig UseControlPageProvider(this NavigatorConfig config, Control container)
        {
            return config.UseProvider(new ControlPageProvider(container));
        }
    }
}
