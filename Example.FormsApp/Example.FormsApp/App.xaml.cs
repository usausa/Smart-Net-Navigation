namespace Example.FormsApp
{
    using System.Reflection;

    using Example.FormsApp.Modules;
    using Example.FormsApp.Services;

    using Smart.Forms.Components;
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
            navigator.Navigated += (_, args) =>
            {
                // for debug
                System.Diagnostics.Debug.WriteLine(
                    $"Navigated: [{args.Context.FromId}]->[{args.Context.ToId}] : stacked=[{navigator.StackedCount}]");
            };

            // Show MainWindow
            MainPage = resolver.Get<MainPage>();
        }

        private SmartResolver CreateResolver()
        {
            var config = new ResolverConfig()
                .UseAutoBinding()
                .UseArrayBinding()
                .UseAssignableBinding()
                .UsePropertyInjector();

            config.Bind<INavigator>().ToMethod(_ => navigator).InSingletonScope();

            config.Bind<IDialogService>().To<DialogService>().InSingletonScope();

            config.Bind<DataService>().ToSelf().InSingletonScope();
            config.Bind<ApplicationState>().ToSelf().InSingletonScope();

            return config.ToResolver();
        }

        protected override void OnStart()
        {
            navigator.Forward(ViewId.Menu);
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
