namespace Example.WindowsFormsApp.Views
{
    using System.Windows.Forms;

    using Smart.Navigation;

    [View(ViewId.Menu)]
    public partial class MenuView : AppViewBase
    {
        public override string Title => "Menu";

        public override FunctionKey[] FunctionKeys => new[]
        {
            new FunctionKey(Keys.F12, "Exit")
        };

        public MenuView()
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
            Navigator.Forward(ViewId.DataList);
        }

        private void OnMenuButton2Click(object sender, System.EventArgs e)
        {
            Navigator.Forward(ViewId.Stack1);
        }

        private void OnMenuButton3Click(object sender, System.EventArgs e)
        {
            Navigator.Forward(ViewId.WizardInput1);
        }
    }
}
