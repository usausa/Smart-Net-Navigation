namespace Example.FormsApp.Shell
{
    using Smart.ComponentModel;

    public interface IShellControl
    {
        NotificationValue<string> Title { get; }

        NotificationValue<bool> CanGoHome { get; }

        NotificationValue<string> Function1Text { get; }

        NotificationValue<string> Function2Text { get; }

        NotificationValue<string> Function3Text { get; }

        NotificationValue<string> Function4Text { get; }

        NotificationValue<bool> Function1Enabled { get; }

        NotificationValue<bool> Function2Enabled { get; }

        NotificationValue<bool> Function3Enabled { get; }

        NotificationValue<bool> Function4Enabled { get; }
    }
}