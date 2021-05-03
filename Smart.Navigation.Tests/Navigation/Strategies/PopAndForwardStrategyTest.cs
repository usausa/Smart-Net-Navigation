namespace Smart.Navigation.Strategies
{
    using System;
    using System.Threading.Tasks;

    using Smart.Mock;

    using Xunit;

    public class PopAndForwardStrategyTest
    {
        // ------------------------------------------------------------
        // Navigate
        // ------------------------------------------------------------

        [Fact]
        public static void PopAndForward()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.PopAndForward(typeof(Form3));

            Assert.Equal(1, navigator.StackedCount);
            var form3 = (MockForm)navigator.CurrentView!;
            Assert.Equal(typeof(Form3), form3.GetType());
            Assert.True(form3.IsOpen);
            Assert.True(form3.IsVisible);

            Assert.Equal(typeof(Form2), context.Value.FromId);
            Assert.Equal(typeof(Form3), context.Value.ToId);
            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
        }

        [Fact]
        public static void PopAndForwardMultiple()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.Push(typeof(Form3));
            navigator.PopAndForward(typeof(Form4), 2);

            Assert.Equal(1, navigator.StackedCount);
            var form4 = (MockForm)navigator.CurrentView!;
            Assert.Equal(typeof(Form4), form4.GetType());
            Assert.True(form4.IsOpen);
            Assert.True(form4.IsVisible);

            Assert.Equal(typeof(Form3), context.Value.FromId);
            Assert.Equal(typeof(Form4), context.Value.ToId);
            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
        }

        [Fact]
        public static void PopAllAndForward()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.Push(typeof(Form3));
            navigator.PopAllAndForward(typeof(Form4));

            Assert.Equal(1, navigator.StackedCount);
            var form4 = (MockForm)navigator.CurrentView!;
            Assert.Equal(typeof(Form4), form4.GetType());
            Assert.True(form4.IsOpen);
            Assert.True(form4.IsVisible);

            Assert.Equal(typeof(Form3), context.Value.FromId);
            Assert.Equal(typeof(Form4), context.Value.ToId);
            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
        }

        [Fact]
        public static void PopAndForwardWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.PopAndForward(typeof(Form3), new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static void PopAndForwardMultipleWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.Push(typeof(Form3));
            navigator.PopAndForward(typeof(Form4), 2, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static void PopAllAndForwardWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            navigator.Push(typeof(Form3));
            navigator.PopAllAndForward(typeof(Form4), new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Async
        // ------------------------------------------------------------

        [Fact]
        public static async ValueTask TestNavigatorPopAndForwardAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PopAndForwardAsync(typeof(Form3));

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(typeof(Form3), navigator.CurrentViewId);
        }

        [Fact]
        public static async ValueTask TestNavigatorPopAndForwardMultipleAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PushAsync(typeof(Form3));
            await navigator.PopAndForwardAsync(typeof(Form4), 2);

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(typeof(Form4), navigator.CurrentViewId);
        }

        [Fact]
        public static async ValueTask TestNavigatorPopAllAndForwardAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PushAsync(typeof(Form3));
            await navigator.PopAllAndForwardAsync(typeof(Form4));

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(typeof(Form4), navigator.CurrentViewId);
        }

        [Fact]
        public static async ValueTask TestNavigatorPopAndForwardWithParameterAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PopAndForwardAsync(typeof(Form3), new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static async ValueTask TestNavigatorPopAndForwardMultipleWithParameterAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PushAsync(typeof(Form3));
            await navigator.PopAndForwardAsync(typeof(Form4), 2, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static async ValueTask TestNavigatorPopAllAndForwardWithParameterAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (_, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.PushAsync(typeof(Form2));
            await navigator.PushAsync(typeof(Form3));
            await navigator.PopAllAndForwardAsync(typeof(Form4), new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void PopAndForwardFailed2()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(typeof(Form3), 3));
        }

        [Fact]
        public static void PopAndForwardFailed3()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Form1));
            navigator.Push(typeof(Form2));
            Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(typeof(Form3), 0));
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

        public class Form4 : MockForm
        {
        }
    }
}
