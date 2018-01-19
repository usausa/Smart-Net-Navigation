namespace Example.WindowsFormsApp.Pages.Stack
{
    using Smart.Navigation;

    [Page(PageId.Stack2)]
    public partial class Stack2Page : AppPageBase
    {
        public override string Title => "Stack2";

        public override bool CanGoHome => true;

        public Stack2Page()
        {
            InitializeComponent();
        }

        public override void OnGoHome()
        {
            Navigator.PopAndForward(PageId.Menu);
        }

        private void OnPopButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Pop();
        }

        private void OnPushButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Push(PageId.Stack3);
        }
    }
}
