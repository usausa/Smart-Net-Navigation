namespace Example.WindowsFormsApp.Pages.Data
{
    using System;

    using Example.WindowsFormsApp.Models;
    using Example.WindowsFormsApp.Services;

    using Smart.Navigation;
    using Smart.Resolver.Attributes;

    [Page(PageId.DataDetailNew)]
    [Page(PageId.DataDetailEdit)]
    public partial class DataDetailPage : AppPageBase
    {
        private bool update;

        private DataEntity entity;

        public override string Title => update ? "Data New" : "Data Edit";

        public override bool CanGoHome => true;

        [Inject]
        public DataService DataService { get; set; }

        public DataDetailPage()
        {
            InitializeComponent();
        }

        public override void OnNavigatingTo(INavigationContext context)
        {
            update = Equals(context.ToId, PageId.DataDetailEdit);
            if (update)
            {
                entity = context.Parameter.GetValue<DataEntity>();
                NameText.Text = entity.Name;
            }
        }

        public override void OnGoHome()
        {
            Navigator.Forward(PageId.Menu);
        }

        private void OnPrevButtonClick(object sender, System.EventArgs e)
        {
            Navigator.Forward(PageId.DataList);
        }

        private void OnUpdateButtonClick(object sender, System.EventArgs e)
        {
            if (String.IsNullOrEmpty(NameText.Text))
            {
                NameText.Focus();
                return;
            }

            if (update)
            {
                entity.Name = NameText.Text;
                DataService.UpdateData(entity);
            }
            else
            {
                DataService.InsertData(NameText.Text);
            }

            Navigator.Forward(PageId.DataList);
        }
    }
}
