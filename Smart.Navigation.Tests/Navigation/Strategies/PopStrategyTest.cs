namespace Smart.Navigation.Strategies
{
    using System;
    using System.Threading.Tasks;

    using Smart.Mock;

    using Xunit;

    public class PopStrategyTest
    {
        // ------------------------------------------------------------
        // Navigate
        // ------------------------------------------------------------

        [Fact]
        public static void Pop()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));

            var form1 = (MockForm)navigator.CurrentView;
            form1.Focused = "text1";

            navigator.Push(typeof(Form2));
            navigator.Pop();

            Assert.Equal(1, navigator.StackedCount);
            var form1B = (MockForm)navigator.CurrentView;
            Assert.Same(form1, form1B);
            Assert.Equal(typeof(Form1), form1B.GetType());
            Assert.True(form1B.IsOpen);
            Assert.True(form1B.IsVisible);
            Assert.Equal("text1", form1B.Focused);

            Assert.Equal(typeof(Form2), context.Value.FromId);
            Assert.Equal(typeof(Form1), context.Value.ToId);
            Assert.True(context.Value.Attribute.IsRestore());
        }

        [Fact]
        public static void PopMultiple()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.Push(typeof(Form3));
            navigator.Pop(2);

            Assert.Equal(1, navigator.StackedCount);
            var form1 = (MockForm)navigator.CurrentView;
            Assert.Equal(typeof(Form1), form1.GetType());
            Assert.True(form1.IsOpen);
            Assert.True(form1.IsVisible);

            Assert.Equal(typeof(Form3), context.Value.FromId);
            Assert.Equal(typeof(Form1), context.Value.ToId);
            Assert.True(context.Value.Attribute.IsRestore());
        }

        [Fact]
        public static void PopWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.Pop(new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static void PopMultipleWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.Push(typeof(Form3));
            navigator.Pop(2, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Async
        // ------------------------------------------------------------

        [Fact]
        public static async ValueTask TestNavigatorPopAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PopAsync();

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(typeof(Form1), navigator.CurrentViewId);
        }

        [Fact]
        public static async ValueTask TestNavigatorPopAsyncMultiple()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PushAsync(typeof(Form3));
            await navigator.PopAsync(2);

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(typeof(Form1), navigator.CurrentViewId);
        }

        [Fact]
        public static async ValueTask TestNavigatorPopAsyncWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PopAsync(new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static async ValueTask TestNavigatorPopAsyncMultipleWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PushAsync(typeof(Form3));
            await navigator.PopAsync(2, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static void PopFailed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            Assert.Throws<InvalidOperationException>(() => navigator.Pop(2));
        }

        [Fact]
        public static void PopFailed2()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.Pop(0));
        }

        // ------------------------------------------------------------
        // Mock
        // ------------------------------------------------------------

        public class Form1 : MockForm
        {
        }

        public class Form2 : MockForm
        {
        }

        public class Form3 : MockForm
        {
        }
    }
}
