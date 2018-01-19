namespace Example.WindowsFormsApp.Pages
{
    using System.Windows.Forms;

    using Smart.Navigation;

    [Page(PageId.Menu)]
    public partial class MenuPage : AppPageBase
    {
        public override string Title { get; } = "Menu";

        public override FunctionKey[] FunctionKeys => new[]
        {
            new FunctionKey(Keys.F12, "Exit")
        };

        public MenuPage()
        {
            InitializeComponent();
        }

        public override void OnFunctionKey(Keys key)
        {
            switch (key)
            {
                case Keys.F12:
                    Navigator.Exit();
                    break;
            }
        }

        private void OnMenuButton1Click(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.DataList);
        }

        private void OnMenuButton2Click(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.Stack1);
        }

        private void OnMenuButton3Click(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.WizardInput1);
        }
    }
}
