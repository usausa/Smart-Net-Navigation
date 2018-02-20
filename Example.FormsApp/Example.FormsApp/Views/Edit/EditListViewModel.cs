﻿namespace Example.FormsApp.Views.Edit
{
    using Example.FormsApp.Services;

    using Smart.Forms.Input;
    using Smart.Navigation;
    using Smart.Resolver.Attributes;

    public class EditListViewModel : AppViewModelBase
    {
        [Inject]
        public DataService DataService { get; set; }

        public AsyncCommand<ViewId> Forward { get; }

        public EditListViewModel()
        {
            Forward = MakeAsyncCommand<ViewId>(x => Navigator.ForwardAsync(x));
        }
    }
}
