namespace Smart.Navigation;

using System.ComponentModel;

using Smart.Navigation.Strategies;

public interface INavigator : INotifyPropertyChanged
{
    event EventHandler<ConfirmEventArgs>? Confirm;

    event EventHandler<NavigationEventArgs>? Navigating;

    event EventHandler<NavigationEventArgs>? Navigated;

    event EventHandler<EventArgs>? Exited;

    event EventHandler<EventArgs>? ExecutingChanged;

    int StackedCount { get; }

    object? CurrentViewId { get; }

    object? CurrentView { get; }

    object? CurrentTarget { get; }

    bool Executing { get; }

    // Exit

#pragma warning disable CA1716
    void Exit();
#pragma warning restore CA1716

    // Navigate

    bool Navigate(INavigationStrategy strategy, INavigationParameter? parameter);

    Task<bool> NavigateAsync(INavigationStrategy strategy, INavigationParameter? parameter);
}
