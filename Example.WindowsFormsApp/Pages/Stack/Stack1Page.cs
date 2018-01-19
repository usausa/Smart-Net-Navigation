namespace Example.WindowsFormsApp.Pages.Stack
{
    using Smart.Navigation;

    [Page(PageId.Stack1)]
    public partial class Stack1Page : AppPageBase
    {
        public override string Title => "Stack1";

        public override bool CanBack => true;

        public Stack1Page()
        {
            InitializeComponent();
        }

        public override void OnBack()
        {
            Navigator.Forward(PageId.Menu);
        }

        private void OnCancelButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.Menu);
        }

        private void OnPushButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Push(PageId.Stack2);
        }
    }
}
