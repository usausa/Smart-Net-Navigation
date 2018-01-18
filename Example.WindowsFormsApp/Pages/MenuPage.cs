namespace Example.WindowsFormsApp.Pages
{
    using Smart.Navigation;

    [Page(PageId.Menu)]
    public partial class MenuPage : AppPageBase
    {
        public override string Title { get; } = "Menu";

        public MenuPage()
        {
            InitializeComponent();
        }
    }
}
