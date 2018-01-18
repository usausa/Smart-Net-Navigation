namespace Example.WindowsFormsApp
{
    using System.Windows.Forms;

    public sealed class FunctionKey
    {
        public Keys Key { get; }

        public string Display { get; }

        public FunctionKey(Keys key, string display)
        {
            Key = key;
            Display = display;
        }
    }
}
