namespace Example.WindowsFormsApp
{
    using System.Windows.Forms;

    public interface IApplicationPage
    {
        string Title { get; }

        IFunctionControl FunctionControl { set; }

        void OnFunctionKey(Keys key);

        void OnBack();
    }
}
