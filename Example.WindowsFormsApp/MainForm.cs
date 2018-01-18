namespace Example.WindowsFormsApp
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Forms;

    using Example.WindowsFormsApp.Pages;

    using Smart.Navigation;
    using Smart.Resolver;

    public partial class MainForm : Form, IFunctionControl
    {
        private readonly StandardResolver resolver;

        private readonly Navigator navigator;

        private readonly Dictionary<Keys, FunctionKey> enabledFunctions = new Dictionary<Keys, FunctionKey>();

        private readonly List<Button> functionButtons = new List<Button>();

        public MainForm()
        {
            InitializeComponent();

            InitializeFunctionKeys();

            resolver = CreateResolver();

            // Config Navigator
            navigator = new NavigatorConfig()
                .UseProvider(new ControlPageProvider(ContainerPanel))
                .UseResolver(resolver)
                .ToNavigator();
            navigator.NavigatingTo += OnNavigatingTo;
            navigator.Exited += OnExited;

            navigator.AutoRegister(Assembly.GetExecutingAssembly());

            // Forward
            Show();
            navigator.Forward(PageId.Menu);
        }

        private static StandardResolver CreateResolver()
        {
            var config = new ResolverConfig();
            config.UseAutoBinding();
            // TODO ptoperty injection

            // TODO

            return config.ToResolver();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            resolver.Dispose();
        }

        private void OnNavigatingTo(object sender, NavigationEventArgs e)
        {
            // TODO Title,Back,Function
        }

        private void OnExited(object sender, EventArgs e)
        {
            Close();
        }

        // TODO back

        // TODO option * 2

        private void InitializeFunctionKeys()
        {
            Fn1Button.Tag = Keys.F1;
            Fn2Button.Tag = Keys.F2;
            Fn3Button.Tag = Keys.F3;
            Fn4Button.Tag = Keys.F4;
            Fn5Button.Tag = Keys.F5;
            Fn6Button.Tag = Keys.F6;
            Fn7Button.Tag = Keys.F7;
            Fn8Button.Tag = Keys.F8;
            Fn9Button.Tag = Keys.F9;
            Fn10Button.Tag = Keys.F10;
            Fn11Button.Tag = Keys.F11;
            Fn12Button.Tag = Keys.F12;
            functionButtons.Add(Fn1Button);
            functionButtons.Add(Fn2Button);
            functionButtons.Add(Fn3Button);
            functionButtons.Add(Fn4Button);
            functionButtons.Add(Fn5Button);
            functionButtons.Add(Fn6Button);
            functionButtons.Add(Fn7Button);
            functionButtons.Add(Fn8Button);
            functionButtons.Add(Fn9Button);
            functionButtons.Add(Fn10Button);
            functionButtons.Add(Fn11Button);
            functionButtons.Add(Fn12Button);
        }

        public void UpdateKeys(FunctionKey[] keys)
        {
            // TODO Updatefunction, to initial only ?
        }

        private void OnFunctionClick(object sender, EventArgs e)
        {
            if (navigator.CurrentPage is IApplicationPage page)
            {
                page.OnFunctionKey((Keys)((Button)sender).Tag);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (enabledFunctions.ContainsKey(keyData) &&
                navigator.CurrentPage is IApplicationPage page)
            {
                page.OnFunctionKey(keyData);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
    }
}
