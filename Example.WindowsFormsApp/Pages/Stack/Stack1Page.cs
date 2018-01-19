namespace Example.WindowsFormsApp.Pages.Stack
{
    using Smart.Navigation;

    [Page(PageId.Stack1)]
    public partial class Stack1Page : AppPageBase
    {
        public override bool CanBack => true;

        public Stack1Page()
        {
            InitializeComponent();
        }

        public override void OnBack()
        {
            Navigator.Forward(PageId.Menu);
        }
    }
}
