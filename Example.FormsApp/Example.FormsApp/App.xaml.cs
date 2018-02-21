[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]

namespace Example.FormsApp
{
    using System.Reflection;

    using Example.FormsApp.Views;

    using Smart.Forms.Resolver;
    using Smart.Navigation;
    using Smart.Resolver;

    public partial class App
    {
        private readonly Navigator navigator;

        public App()
        {
            InitializeComponent();

            // Config Resolver
            var resolver = CreateResolver();
            ResolveProvider.Default.UseSmartResolver(resolver);

            // Config Navigator
            navigator = new NavigatorConfig()
                .UseFormsNavigationProvider()
                .UseResolver(resolver)
                .UseIdViewMapper(m => m.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes))
                .ToNavigator();
            navigator.Navigated += (sender, args) =>
            {
                // for debug
                System.Diagnostics.Debug.WriteLine(
                    $"Navigated: [{args.Context.FromId}]->[{args.Context.ToId}] : stacked=[{navigator.StackedCount}]");
            };

            // Show MainWindow
            MainPage = resolver.Get<MainPage>();

            navigator.Forward(ViewId.Menu);
        }

        private SmartResolver CreateResolver()
        {
            var config = new ResolverConfig()
                .UseAutoBinding()
                .UseArrayBinding()
                //.UseAssignableBinding()
                .UsePropertyInjector();

            config.Bind<INavigator>().ToMethod(kernel => navigator).InSingletonScope();

            return config.ToResolver();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
