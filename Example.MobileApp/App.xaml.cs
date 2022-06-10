namespace Example.MobileApp;

public partial class App
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<MainPage>();
    }
}
