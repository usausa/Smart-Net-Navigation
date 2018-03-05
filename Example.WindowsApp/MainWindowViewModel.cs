namespace Example.WindowsApp
{
    using System;
    using System.Reactive.Linq;

    using Smart.Navigation;
    using Smart.Windows.ViewModels;

    public class MainWindowViewModel : ViewModelBase
    {
        public INavigator Navigator { get; }

        public MainWindowViewModel(INavigator navigator)
        {
            Navigator = navigator;

            Disposables.Add(Observable
                .FromEvent<EventHandler<EventArgs>, EventArgs>(h => (s, e) => h(e), h => navigator.ExecutingChanged += h, h => navigator.ExecutingChanged -= h)
                .Subscribe(e => BusyState.IsBusy = navigator.Executing));
        }
    }
}
