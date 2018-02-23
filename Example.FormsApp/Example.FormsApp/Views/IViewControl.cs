namespace Example.FormsApp.Views
{
    public interface IViewControl
    {
        string Title { get; set; }

        bool IsBusy { get; set; }

        bool CanBack { get; set; }

        bool Function1Enabled { get; set; }

        string Function1Text { get; set; }

        bool Function2Enabled { get; set; }

        string Function2Text { get; set; }

        bool Function3Enabled { get; set; }

        string Function3Text { get; set; }

        bool Function4Enabled { get; set; }

        string Function4Text { get; set; }
    }
}