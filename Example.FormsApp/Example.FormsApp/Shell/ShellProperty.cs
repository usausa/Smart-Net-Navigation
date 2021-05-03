namespace Example.FormsApp.Shell
{
    using Xamarin.Forms;

    public static class ShellProperty
    {
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

        public static readonly BindableProperty CanGoHomeProperty = BindableProperty.CreateAttached(
            "CanGoHome",
            typeof(bool),
            typeof(ShellProperty),
            true,
            propertyChanged: PropertyChanged);

        public static bool GetCanGoHome(BindableObject view)
        {
            return (bool)view.GetValue(CanGoHomeProperty);
        }

        public static void SetCanGoHome(BindableObject view, bool value)
        {
            view.SetValue(CanGoHomeProperty, value);
        }

        public static readonly BindableProperty Function1TextProperty = BindableProperty.CreateAttached(
            "Function1Text",
            typeof(string),
            typeof(ShellProperty),
            string.Empty,
            propertyChanged: PropertyChanged);

        public static string GetFunction1Text(BindableObject view)
        {
            return (string)view.GetValue(Function1TextProperty);
        }

        public static void SetFunction1Text(BindableObject view, string value)
        {
            view.SetValue(Function1TextProperty, value);
        }

        public static readonly BindableProperty Function2TextProperty = BindableProperty.CreateAttached(
            "Function2Text",
            typeof(string),
            typeof(ShellProperty),
            string.Empty,
            propertyChanged: PropertyChanged);

        public static string GetFunction2Text(BindableObject view)
        {
            return (string)view.GetValue(Function2TextProperty);
        }

        public static void SetFunction2Text(BindableObject view, string value)
        {
            view.SetValue(Function2TextProperty, value);
        }

        public static readonly BindableProperty Function3TextProperty = BindableProperty.CreateAttached(
            "Function3Text",
            typeof(string),
            typeof(ShellProperty),
            string.Empty,
            propertyChanged: PropertyChanged);

        public static string GetFunction3Text(BindableObject view)
        {
            return (string)view.GetValue(Function3TextProperty);
        }

        public static void SetFunction3Text(BindableObject view, string value)
        {
            view.SetValue(Function3TextProperty, value);
        }

        public static readonly BindableProperty Function4TextProperty = BindableProperty.CreateAttached(
            "Function4Text",
            typeof(string),
            typeof(ShellProperty),
            string.Empty,
            propertyChanged: PropertyChanged);

        public static string GetFunction4Text(BindableObject view)
        {
            return (string)view.GetValue(Function4TextProperty);
        }

        public static void SetFunction4Text(BindableObject view, string value)
        {
            view.SetValue(Function4TextProperty, value);
        }

        public static readonly BindableProperty Function1EnabledProperty = BindableProperty.CreateAttached(
            "Function1Enabled",
            typeof(bool),
            typeof(ShellProperty),
            false,
            propertyChanged: PropertyChanged);

        public static bool GetFunction1Enabled(BindableObject view)
        {
            return (bool)view.GetValue(Function1EnabledProperty);
        }

        public static void SetFunction1Enabled(BindableObject view, bool value)
        {
            view.SetValue(Function1EnabledProperty, value);
        }

        public static readonly BindableProperty Function2EnabledProperty = BindableProperty.CreateAttached(
            "Function2Enabled",
            typeof(bool),
            typeof(ShellProperty),
            false,
            propertyChanged: PropertyChanged);

        public static bool GetFunction2Enabled(BindableObject view)
        {
            return (bool)view.GetValue(Function2EnabledProperty);
        }

        public static void SetFunction2Enabled(BindableObject view, bool value)
        {
            view.SetValue(Function2EnabledProperty, value);
        }

        public static readonly BindableProperty Function3EnabledProperty = BindableProperty.CreateAttached(
            "Function3Enabled",
            typeof(bool),
            typeof(ShellProperty),
            false,
            propertyChanged: PropertyChanged);

        public static bool GetFunction3Enabled(BindableObject view)
        {
            return (bool)view.GetValue(Function3EnabledProperty);
        }

        public static void SetFunction3Enabled(BindableObject view, bool value)
        {
            view.SetValue(Function3EnabledProperty, value);
        }

        public static readonly BindableProperty Function4EnabledProperty = BindableProperty.CreateAttached(
            "Function4Enabled",
            typeof(bool),
            typeof(ShellProperty),
            false,
            propertyChanged: PropertyChanged);

        public static bool GetFunction4Enabled(BindableObject view)
        {
            return (bool)view.GetValue(Function4EnabledProperty);
        }

        public static void SetFunction4Enabled(BindableObject view, bool value)
        {
            view.SetValue(Function4EnabledProperty, value);
        }

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var parent = ((ContentView)bindable).Parent;
            if (parent?.BindingContext is IShellControl shell)
            {
                UpdateShellControl(shell, bindable);
            }
        }

        public static void UpdateShellControl(IShellControl shell, BindableObject? bindable)
        {
            if (bindable is null)
            {
                shell.Title.Value = string.Empty;
                shell.CanGoHome.Value = false;
                shell.Function1Text.Value = string.Empty;
                shell.Function2Text.Value = string.Empty;
                shell.Function3Text.Value = string.Empty;
                shell.Function4Text.Value = string.Empty;
                shell.Function1Enabled.Value = false;
                shell.Function2Enabled.Value = false;
                shell.Function3Enabled.Value = false;
                shell.Function4Enabled.Value = false;
            }
            else
            {
                shell.Title.Value = GetTitle(bindable);
                shell.CanGoHome.Value = GetCanGoHome(bindable);
                shell.Function1Text.Value = GetFunction1Text(bindable);
                shell.Function2Text.Value = GetFunction2Text(bindable);
                shell.Function3Text.Value = GetFunction3Text(bindable);
                shell.Function4Text.Value = GetFunction4Text(bindable);
                shell.Function1Enabled.Value = GetFunction1Enabled(bindable);
                shell.Function2Enabled.Value = GetFunction2Enabled(bindable);
                shell.Function3Enabled.Value = GetFunction3Enabled(bindable);
                shell.Function4Enabled.Value = GetFunction4Enabled(bindable);
            }
        }
    }
}
