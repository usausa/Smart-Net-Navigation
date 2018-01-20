namespace Example.WindowsFormsApp.Views.Stack
{
    using Smart.Navigation;

    [View(ViewId.Stack3)]
    public partial class Stack3View : AppViewBase
    {
        public override string Title => "Stack3View";

        public override bool CanGoHome => true;

        public Stack3View()
        {
            InitializeComponent();
        }

        public override void OnGoHome()
        {
            Navigator.PopAndForward(ViewId.Menu, 2);
        }

        private void OnPopButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Pop();
        }

        private void OnPop2ButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Pop(2);
        }
    }
}
