﻿namespace Example.WindowsApp
{
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

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
            navigator.Navigated += (sender, args) =>
            {
                // for debug
                System.Diagnostics.Debug.WriteLine(
                    $"Navigated: [{args.Context.FromId}]->[{args.Context.ToId}] : stacked=[{navigator.StackedCount}]");
            };

            navigator.Register(Assembly.GetExecutingAssembly().ExportedTypes
                .Where(x => x.Namespace.StartsWith("Example.WindowsApp.Views") &&
                            typeof(UserControl).IsAssignableFrom(x)));

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
