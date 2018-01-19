namespace Example.WindowsFormsApp.Pages.Stack
{
    using Smart.Navigation;

    [Page(PageId.Stack3)]
    public partial class Stack3Page : AppPageBase
    {
        public override string Title => "Stack3";

        public override bool CanGoHome => true;

        public Stack3Page()
        {
            InitializeComponent();
        }

        public override void OnGoHome()
        {
            Navigator.PopAndForward(PageId.Menu, 2);
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
