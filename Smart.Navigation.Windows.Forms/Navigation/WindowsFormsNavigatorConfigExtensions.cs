namespace Smart.Navigation;

using System.Windows.Forms;

using Smart.Navigation.Mappers;

public static class WindowsFormsNavigatorConfigExtensions
{
    public static NavigatorConfig UseControlNavigationProvider(this NavigatorConfig config, Control container)
    {
        return config.UseControlNavigationProvider(container, static _ => { });
    }

    public static NavigatorConfig UseControlNavigationProvider(this NavigatorConfig config, Control container, Action<WindowsFormsNavigationProviderOptions> setupAction)
    {
        var options = new WindowsFormsNavigationProviderOptions();
        setupAction(options);

        config.Configure(static c =>
        {
            c.RemoveAll<ITypeConstraint>();
            c.Add<ITypeConstraint>(new AssignableTypeConstraint(typeof(Control)));
        });

        return config.UseProvider(new WindowsFormsNavigationProvider(container, options));
    }
}
