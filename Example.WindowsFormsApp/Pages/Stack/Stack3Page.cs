namespace Example.WindowsFormsApp.Pages.Stack
{
    using Smart.Navigation;

    [Page(PageId.Stack3)]
    public partial class Stack3Page : AppPageBase
    {
        public override string Title { get; } = "Stack3";

        public override bool CanBack => true;

        public Stack3Page()
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

        private void OnPop2ButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Pop(2);
        }
    }
}
