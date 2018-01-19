namespace Example.WindowsFormsApp.Pages.Data
{
    using System.Linq;

    using Example.WindowsFormsApp.Models;
    using Example.WindowsFormsApp.Services;

    using Smart.Navigation;
    using Smart.Resolver.Attributes;

    [Page(PageId.DataList)]
    public partial class DataListPage : AppPageBase
    {
        public override string Title => "Data List";

        public override bool CanBack => true;

        [Inject]
        public DataService DataService { get; set; }

        public DataListPage()
        {
            InitializeComponent();
        }

        public override void OnNavigatingTo(INavigationContext context)
        {
            DataListBox.DataSource = DataService.QueryDataList().ToList();
        }

        public override void OnBack()
        {
            Navigator.Forward(PageId.Menu);
        }

        private void OnNewButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.DataDetailNew);
        }

        private void OnEditButtonClick(object sender, System.EventArgs e)
        {
            var parameter = new NavigationParameter();
            parameter.SetValue((DataEntity)DataListBox.SelectedItem);
            Navigator.Forward(PageId.DataDetailEdit, parameter);
        }
    }
}
