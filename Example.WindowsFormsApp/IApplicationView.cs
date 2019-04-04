namespace Example.WindowsFormsApp
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    public interface IApplicationView
    {
        string Title { get; }

        bool CanGoHome { get; }

        IReadOnlyList<FunctionKey> FunctionKeys { get; }

        void OnFunctionKey(Keys key);

        void OnGoHome();
    }
}
