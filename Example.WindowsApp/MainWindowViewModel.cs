namespace Example.WindowsApp
{
    using System;
    using System.Reactive.Linq;

    using Smart.ComponentModel;

    using Smart.Navigation;
    using Smart.Windows.ViewModels;

    public class MainWindowViewModel : ViewModelBase
    {
        public INavigator Navigator { get; }

        public NotificationValue<object> CurrentView { get; } = new NotificationValue<object>();

        public MainWindowViewModel(INavigator navigator)
        {
            Navigator = navigator;

            Disposables.Add(Observable
                .FromEvent<EventHandler<EventArgs>, EventArgs>(h => (s, e) => h(e), h => navigator.ExecutingChanged += h, h => navigator.ExecutingChanged -= h)
                .Subscribe(e => BusyState.IsBusy = navigator.Executing));
            Disposables.Add(Observable
                .FromEvent<EventHandler<NavigationEventArgs>, NavigationEventArgs>(h => (s, e) => h(e), h => navigator.Navigated += h, h => navigator.Navigated -= h)
                .Subscribe(e => CurrentView.Value = e.ToView));
            Disposables.Add(Observable
                .FromEvent<EventHandler<EventArgs>, EventArgs>(h => (s, e) => h(e), h => navigator.Exited += h, h => navigator.Exited -= h)
                .Subscribe(e => CurrentView.Value = null));
        }
    }
}
