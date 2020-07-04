namespace Example.WindowsApp
{
    using System;
    using System.Reactive.Linq;

    using Example.WindowsApp.Views;

    using Smart.ComponentModel;
    using Smart.Navigation;
    using Smart.Windows.ViewModels;

    public class MainWindowViewModel : ViewModelBase, IShellControl
    {
        public INavigator Navigator { get; }

        public NotificationValue<string> Title { get; } = new NotificationValue<string>();

        public MainWindowViewModel(INavigator navigator)
        {
            Navigator = navigator;

            Disposables.Add(Observable
                .FromEvent<EventHandler<EventArgs>, EventArgs>(h => (s, e) => h(e), h => navigator.ExecutingChanged += h, h => navigator.ExecutingChanged -= h)
                .Subscribe(e =>
                {
                    if (navigator.Executing)
                    {
                        BusyState.Require();
                    }
                    else
                    {
                        BusyState.Release();
                    }
                }));
        }
    }
}
