namespace Smart.Navigation.Strategies
{
    using System;
    using System.Threading.Tasks;

    using Smart.Mock;

    using Xunit;

    public class GroupPopStrategyTest
    {
        // ------------------------------------------------------------
        // Navigate
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorGropedPop()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.FormA1, typeof(FormA1));
            navigator.Register(ViewId.FormA2, typeof(FormA2));

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(ViewId.Form1);
            var form1 = (Form1)navigator.CurrentView;

            navigator.GroupPush(ViewId.FormA1);
            var formA1 = (FormA1)navigator.CurrentView;

            navigator.GroupPush(ViewId.FormA2);
            var formA2 = (FormA2)navigator.CurrentView;

            navigator.GroupPop();

            Assert.Equal(1, navigator.StackedCount);
            Assert.Same(form1, navigator.CurrentView);
            Assert.True(form1.IsVisible);
            Assert.False(formA1.IsOpen);
            Assert.False(formA2.IsOpen);

            Assert.Equal(ViewId.FormA2, context.Value.FromId);
            Assert.Equal(ViewId.Form1, context.Value.ToId);
            Assert.True(context.Value.Attribute.IsRestore());
        }

        [Fact]
        public static void TestNavigatorGropedPopLeaveLast()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.FormA1, typeof(FormA1));
            navigator.Register(ViewId.FormA2, typeof(FormA2));

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(ViewId.Form1);

            navigator.GroupPush(ViewId.FormA1);
            var formA1 = (FormA1)navigator.CurrentView;

            navigator.GroupPush(ViewId.FormA2);
            var formA2 = (FormA2)navigator.CurrentView;

            navigator.GroupPop(true);

            Assert.Equal(2, navigator.StackedCount);
            Assert.Same(formA1, navigator.CurrentView);
            Assert.True(formA1.IsVisible);
            Assert.False(formA2.IsOpen);

            Assert.Equal(ViewId.FormA2, context.Value.FromId);
            Assert.Equal(ViewId.FormA1, context.Value.ToId);
            Assert.True(context.Value.Attribute.IsRestore());
        }

        [Fact]
        public static void TestNavigatorGropedPopLeaveLastNoOperation()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.FormA1, typeof(FormA1));

            // test
            navigator.Forward(ViewId.Form1);
            navigator.GroupPush(ViewId.FormA1);

            Assert.False(navigator.GroupPop(true));
        }

        [Fact]
        public static void TestNavigatorGropedPopWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.FormA1, typeof(FormA1));
            navigator.Register(ViewId.FormA2, typeof(FormA2));

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(ViewId.Form1);
            navigator.GroupPush(ViewId.FormA1);
            navigator.GroupPush(ViewId.FormA2);
            navigator.GroupPop(new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static void TestNavigatorGropedPopLeaveLastWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.FormA1, typeof(FormA1));
            navigator.Register(ViewId.FormA2, typeof(FormA2));

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(ViewId.Form1);
            navigator.GroupPush(ViewId.FormA1);
            navigator.GroupPush(ViewId.FormA2);
            navigator.GroupPop(true, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Async
        // ------------------------------------------------------------

        [Fact]
        public static async Task TestNavigatorGropedPopAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.FormA1, typeof(FormA1));
            navigator.Register(ViewId.FormA2, typeof(FormA2));

            // test
            await navigator.ForwardAsync(ViewId.Form1);
            await navigator.GroupPushAsync(ViewId.FormA1);
            await navigator.GroupPushAsync(ViewId.FormA2);
            await navigator.GroupPopAsync();

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(ViewId.Form1, navigator.CurrentViewId);
        }

        [Fact]
        public static async Task TestNavigatorGropedPopLeaveLastAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.FormA1, typeof(FormA1));
            navigator.Register(ViewId.FormA2, typeof(FormA2));

            // test
            await navigator.ForwardAsync(ViewId.Form1);
            await navigator.GroupPushAsync(ViewId.FormA1);
            await navigator.GroupPushAsync(ViewId.FormA2);
            await navigator.GroupPopAsync(true);

            Assert.Equal(2, navigator.StackedCount);
            Assert.Equal(ViewId.FormA1, navigator.CurrentViewId);
        }

        [Fact]
        public static async Task TestNavigatorGropedPopAsyncWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.FormA1, typeof(FormA1));
            navigator.Register(ViewId.FormA2, typeof(FormA2));

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(ViewId.Form1);
            await navigator.GroupPushAsync(ViewId.FormA1);
            await navigator.GroupPushAsync(ViewId.FormA2);
            await navigator.GroupPopAsync(new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static async Task TestNavigatorGropedPopAsyncLeaveLastWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));
            navigator.Register(ViewId.FormA1, typeof(FormA1));
            navigator.Register(ViewId.FormA2, typeof(FormA2));

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(ViewId.Form1);
            await navigator.GroupPushAsync(ViewId.FormA1);
            await navigator.GroupPushAsync(ViewId.FormA2);
            await navigator.GroupPopAsync(true, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void TestNavigatorGropedPopFailed1()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.GroupPop());
        }

        [Fact]
        public static void TestNavigatorGropedPopFailed2()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Form1, typeof(Form1));

            // test
            navigator.Forward(ViewId.Form1);
            Assert.Throws<InvalidOperationException>(() => navigator.GroupPop());
        }

        [Fact]
        public static void TestNavigatorGropedPopFailed3()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.FormA1, typeof(FormA1));

            // test
            navigator.GroupPush(ViewId.FormA1);
            Assert.Throws<InvalidOperationException>(() => navigator.GroupPop());
        }

        // ------------------------------------------------------------
        // Mock
        // ------------------------------------------------------------

        public enum ViewId
        {
            Form1,
            Form2,
            FormA1,
            FormA2,
            FormB1,
            FormB2,
            FormC1,
            FormC2
        }

        public enum Groups
        {
            A,
            B,
            C
        }

        public class Form1 : MockForm
        {
        }

        public class Form2 : MockForm
        {
        }

        [Group(Groups.A)]
        public class FormA1 : MockForm
        {
        }

        [Group(Groups.A)]
        public class FormA2 : MockForm
        {
        }

        [Group(Groups.B)]
        public class FormB1 : MockForm
        {
        }

        [Group(Groups.B)]
        public class FormB2 : MockForm
        {
        }

        [Group(Groups.C)]
        public class FormC1 : MockForm
        {
        }

        [Group(Groups.C)]
        public class FormC2 : MockForm
        {
        }
    }
}
