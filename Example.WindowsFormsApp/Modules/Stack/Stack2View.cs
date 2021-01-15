namespace Example.WindowsFormsApp.Modules.Stack
{
    using Smart.Navigation;
    using Smart.Navigation.Attributes;

    [View(ViewId.Stack2)]
    public partial class Stack2View : AppViewBase
    {
        public override string Title => "Stack2View";

        public override bool CanGoHome => true;

        public Stack2View()
        {
            InitializeComponent();
        }

        public override void OnGoHome()
        {
            Navigator.PopAndForward(ViewId.Menu);
        }

        private void OnPopButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Pop();
        }

        private void OnPushButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Push(ViewId.Stack3);
        }
    }
}
