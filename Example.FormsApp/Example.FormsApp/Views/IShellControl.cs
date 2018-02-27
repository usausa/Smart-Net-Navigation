namespace Example.FormsApp.Views
{
    public interface IShellControl
    {
        string Title { get; set; }

        bool IsBusy { get; set; }

        bool CanGoHome { get; set; }

        string Function1Text { get; set; }

        string Function2Text { get; set; }

        string Function3Text { get; set; }

        string Function4Text { get; set; }

        bool Function1Enabled { get; set; }

        bool Function2Enabled { get; set; }

        bool Function3Enabled { get; set; }

        bool Function4Enabled { get; set; }
    }
}