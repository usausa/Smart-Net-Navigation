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
    }
}