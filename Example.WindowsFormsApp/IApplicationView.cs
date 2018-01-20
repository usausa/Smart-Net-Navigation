namespace Example.WindowsFormsApp
{
    using System.Windows.Forms;

    public interface IApplicationView
    {
        string Title { get; }

        bool CanGoHome { get; }

        FunctionKey[] FunctionKeys { get; }

        void OnFunctionKey(Keys key);

        void OnGoHome();
    }
}
