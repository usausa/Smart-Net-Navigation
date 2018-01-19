namespace Example.WindowsFormsApp.Pages.Stack
{
    using Smart.Navigation;

    [Page(PageId.Stack2)]
    public partial class Stack2Page : AppPageBase
    {
        public override string Title { get; } = "Stack2";

        public override bool CanBack => true;

        public Stack2Page()
        {
            InitializeComponent();
        }

        public override void OnBack()
        {
            Navigator.Pop();
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
