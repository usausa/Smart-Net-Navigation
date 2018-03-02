namespace Example.FormsApp
{
    using Example.FormsApp.Modules;
    using Example.FormsApp.Views;

    using Smart.ComponentModel;
    using Smart.Forms.Components;
    using Smart.Forms.Input;
    using Smart.Forms.ViewModels;
    using Smart.Navigation;

    public class MainPageViewModel : ViewModelBase, IShellControl
    {
        public NotificationValue<string> Title { get; } = new NotificationValue<string>();

        public NotificationValue<bool> CanGoHome { get; } = new NotificationValue<bool>();

        public NotificationValue<string> Function1Text { get; } = new NotificationValue<string>();

        public NotificationValue<string> Function2Text { get; } = new NotificationValue<string>();

        public NotificationValue<string> Function3Text { get; } = new NotificationValue<string>();

        public NotificationValue<string> Function4Text { get; } = new NotificationValue<string>();

        public NotificationValue<bool> Function1Enabled { get; } = new NotificationValue<bool>();

        public NotificationValue<bool> Function2Enabled { get; } = new NotificationValue<bool>();

        public NotificationValue<bool> Function3Enabled { get; } = new NotificationValue<bool>();

        public NotificationValue<bool> Function4Enabled { get; } = new NotificationValue<bool>();

        public ApplicationState ApplicationState { get; }

        public INavigator Navigator { get; }

        public AsyncCommand GoHome { get; }

        public AsyncCommand Option { get; }

        public AsyncCommand Function1 { get; }

        public AsyncCommand Function2 { get; }

        public AsyncCommand Function3 { get; }

        public AsyncCommand Function4 { get; }

        public MainPageViewModel(
            ApplicationState applicationState,
            INavigator navigator,
            IDialogService dialogService)
            : base(applicationState)
        {
            ApplicationState = applicationState;
            Navigator = navigator;

            GoHome = MakeAsyncCommand(
                    () => Navigator.PopAllAndForwardAsync(ViewId.Menu),
                    () => CanGoHome.Value)
                .Observe(CanGoHome);
            Option = MakeAsyncCommand(() => dialogService.DisplayAlert("Option", "Option", "OK"));
            Function1 = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(FunctionKeys.Function1),
                    () => Function1Enabled.Value)
                .Observe(Function1Enabled);
            Function2 = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(FunctionKeys.Function2),
                    () => Function2Enabled.Value)
                .Observe(Function2Enabled);
            Function3 = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(FunctionKeys.Function3),
                    () => Function3Enabled.Value)
                .Observe(Function3Enabled);
            Function4 = MakeAsyncCommand(
                    () => Navigator.NotifyAsync(FunctionKeys.Function4),
                    () => Function4Enabled.Value)
                .Observe(Function4Enabled);
        }
    }
}
