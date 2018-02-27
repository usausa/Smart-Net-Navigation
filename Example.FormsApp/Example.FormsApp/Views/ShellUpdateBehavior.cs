namespace Example.FormsApp.Views
{
    using System.ComponentModel;

    using Smart.Forms.Interactivity;

    using Xamarin.Forms;

    public class ShellUpdateBehavior : BehaviorBase<ContentView>
    {
        protected override void OnAttachedTo(ContentView bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.PropertyChanged += BindableOnPropertyChanged;
        }

        protected override void OnDetachingFrom(ContentView bindable)
        {
            bindable.PropertyChanged -= BindableOnPropertyChanged;

            base.OnDetachingFrom(bindable);
        }

        private void BindableOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(ContentView.Content))
            {
                return;
            }

            ShellProperty.UpdateShellControl(AssociatedObject, AssociatedObject.Content);
        }
    }
}
