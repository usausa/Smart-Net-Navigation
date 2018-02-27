namespace Example.FormsApp.Views
{
    using Xamarin.Forms;

    public static class ShellProperty
    {
        public static readonly BindableProperty ShellControlProperty = BindableProperty.CreateAttached(
            "ShellControl",
            typeof(IShellControl),
            typeof(ShellProperty),
            null);

        public static IShellControl GetShellControl(BindableObject view)
        {
            return (IShellControl)view.GetValue(ShellControlProperty);
        }

        public static void SetShellControl(BindableObject view, IShellControl value)
        {
            view.SetValue(ShellControlProperty, value);
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.CreateAttached(
            "Title",
            typeof(string),
            typeof(ShellProperty),
            null,
            propertyChanged: PropertyChanged);

        public static string GetTitle(BindableObject view)
        {
            return (string)view.GetValue(TitleProperty);
        }

        public static void SetTitle(BindableObject view, string value)
        {
            view.SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty IsBusyProperty = BindableProperty.CreateAttached(
            "IsBusy",
            typeof(bool),
            typeof(ShellProperty),
            false,
            propertyChanged: PropertyChanged);

        // TODO

        public static readonly BindableProperty CanGoHomeProperty = BindableProperty.CreateAttached(
            "CanGoHome",
            typeof(bool),
            typeof(ShellProperty),
            true,
            propertyChanged: PropertyChanged);

        // TODO

        public static readonly BindableProperty Function1TextProperty = BindableProperty.CreateAttached(
            "Function1Text",
            typeof(string),
            typeof(ShellProperty),
            null,
            propertyChanged: PropertyChanged);

        // TODO

        public static readonly BindableProperty Function2TextProperty = BindableProperty.CreateAttached(
            "Function2Text",
            typeof(string),
            typeof(ShellProperty),
            null,
            propertyChanged: PropertyChanged);

        // TODO

        public static readonly BindableProperty Function3TextProperty = BindableProperty.CreateAttached(
            "Function3Text",
            typeof(string),
            typeof(ShellProperty),
            null,
            propertyChanged: PropertyChanged);

        // TODO

        public static readonly BindableProperty Function4TextProperty = BindableProperty.CreateAttached(
            "Function4Text",
            typeof(string),
            typeof(ShellProperty),
            null,
            propertyChanged: PropertyChanged);

        // TODO

        public static readonly BindableProperty Function1EnabledProperty = BindableProperty.CreateAttached(
            "Function1Enabled",
            typeof(bool),
            typeof(ShellProperty),
            true,
            propertyChanged: PropertyChanged);

        // TODO

        public static readonly BindableProperty Function2EnabledProperty = BindableProperty.CreateAttached(
            "Function2Enabled",
            typeof(bool),
            typeof(ShellProperty),
            true,
            propertyChanged: PropertyChanged);

        // TODO

        public static readonly BindableProperty Function3EnabledProperty = BindableProperty.CreateAttached(
            "Function3Enabled",
            typeof(bool),
            typeof(ShellProperty),
            true,
            propertyChanged: PropertyChanged);

        // TODO

        public static readonly BindableProperty Function4EnabledProperty = BindableProperty.CreateAttached(
            "Function4Enabled",
            typeof(bool),
            typeof(ShellProperty),
            true,
            propertyChanged: PropertyChanged);

        // TODO

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var parent = ((ContentView)bindable).Parent;
            if (parent == null)
            {
                return;
            }

            UpdateShellControl(parent, bindable);
        }

        public static void UpdateShellControl(BindableObject parent, BindableObject child)
        {
            var shellControl = GetShellControl(parent);
            if (shellControl != null)
            {
                shellControl.Title = GetTitle(child);
                // TODO
            }
        }
    }
}
