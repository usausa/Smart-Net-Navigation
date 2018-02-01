namespace Example.WindowsApp
{
    using System.Windows;

    using Example.WindowsApp.Views;

    using Smart.Navigation;
    using Smart.Resolver;
    using Smart.Windows.Resolver;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App
    {
        private SmartResolver resolver;

        private Navigator navigator;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Config Resolver
            resolver = CreateResolver();
            ResolveProvider.Default.UseSmartResolver(resolver);

            // Config Navigator
            navigator = new NavigatorConfig()
                .UseWindowsNavigationProvider()
                .UseResolver(resolver)
                .ToNavigator();

            navigator.Register(typeof(MenuView));

            // Show MainWindow
            MainWindow = resolver.Get<MainWindow>();
            MainWindow.Show();

            navigator.Forward(typeof(MenuView));
        }

        private SmartResolver CreateResolver()
        {
            var config = new ResolverConfig()
                .UseAutoBinding()
                .UsePropertyInjector();

            config.Bind<INavigator>().ToMethod(kernel => navigator);

            return config.ToResolver();
        }
    }
}
