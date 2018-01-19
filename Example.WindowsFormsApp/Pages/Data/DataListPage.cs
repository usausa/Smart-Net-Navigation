namespace Example.WindowsFormsApp.Pages.Data
{
    using Smart.Navigation;

    [Page(PageId.DataList)]
    public partial class DataListPage : AppPageBase
    {
        public override bool CanBack => true;

        public DataListPage()
        {
            InitializeComponent();
        }

        public override void OnBack()
        {
            Navigator.Forward(PageId.Menu);
        }
    }
}
