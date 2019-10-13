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
        public static void GropedPop()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            var form1 = (Form1)navigator.CurrentView;

            navigator.GroupPush(typeof(FormA1));
            var formA1 = (FormA1)navigator.CurrentView;

            navigator.GroupPush(typeof(FormA2));
            var formA2 = (FormA2)navigator.CurrentView;

            navigator.GroupPop();

            Assert.Equal(1, navigator.StackedCount);
            Assert.Same(form1, navigator.CurrentView);
            Assert.True(form1.IsVisible);
            Assert.False(formA1.IsOpen);
            Assert.False(formA2.IsOpen);

            Assert.Equal(typeof(FormA2), context.Value.FromId);
            Assert.Equal(typeof(Form1), context.Value.ToId);
            Assert.True(context.Value.Attribute.IsRestore());
        }

        [Fact]
        public static void GropedPopLeaveLast()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));

            navigator.GroupPush(typeof(FormA1));
            var formA1 = (FormA1)navigator.CurrentView;

            navigator.GroupPush(typeof(FormA2));
            var formA2 = (FormA2)navigator.CurrentView;

            navigator.GroupPop(true);

            Assert.Equal(2, navigator.StackedCount);
            Assert.Same(formA1, navigator.CurrentView);
            Assert.True(formA1.IsVisible);
            Assert.False(formA2.IsOpen);

            Assert.Equal(typeof(FormA2), context.Value.FromId);
            Assert.Equal(typeof(FormA1), context.Value.ToId);
            Assert.True(context.Value.Attribute.IsRestore());
        }

        [Fact]
        public static void GropedPopLeaveLastNoOperation()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Form1));
            navigator.GroupPush(typeof(FormA1));

            Assert.False(navigator.GroupPop(true));
        }

        [Fact]
        public static void GropedPopWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.GroupPush(typeof(FormA1));
            navigator.GroupPush(typeof(FormA2));
            navigator.GroupPop(new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        [Fact]
        public static void GropedPopLeaveLastWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            navigator.Forward(typeof(Form1));
            navigator.GroupPush(typeof(FormA1));
            navigator.GroupPush(typeof(FormA2));
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

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.GroupPushAsync(typeof(FormA1));
            await navigator.GroupPushAsync(typeof(FormA2));
            await navigator.GroupPopAsync();

            Assert.Equal(1, navigator.StackedCount);
            Assert.Equal(typeof(Form1), navigator.CurrentViewId);
        }

        [Fact]
        public static async Task TestNavigatorGropedPopLeaveLastAsync()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.GroupPushAsync(typeof(FormA1));
            await navigator.GroupPushAsync(typeof(FormA2));
            await navigator.GroupPopAsync(true);

            Assert.Equal(2, navigator.StackedCount);
            Assert.Equal(typeof(FormA1), navigator.CurrentViewId);
        }

        [Fact]
        public static async Task TestNavigatorGropedPopAsyncWithParameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.GroupPushAsync(typeof(FormA1));
            await navigator.GroupPushAsync(typeof(FormA2));
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

            var context = new Holder<INavigationContext>();
            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

            // test
            await navigator.ForwardAsync(typeof(Form1));
            await navigator.GroupPushAsync(typeof(FormA1));
            await navigator.GroupPushAsync(typeof(FormA2));
            await navigator.GroupPopAsync(true, new NavigationParameter().SetValue("test"));

            Assert.NotNull(context.Value);
            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
        }

        // ------------------------------------------------------------
        // Failed
        // ------------------------------------------------------------

        [Fact]
        public static void GropedPopFailed1()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            Assert.Throws<InvalidOperationException>(() => navigator.GroupPop());
        }

        [Fact]
        public static void GropedPopFailed2()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Form1));
            Assert.Throws<InvalidOperationException>(() => navigator.GroupPop());
        }

        [Fact]
        public static void GropedPopFailed3()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.GroupPush(typeof(FormA1));
            Assert.Throws<InvalidOperationException>(() => navigator.GroupPop());
        }

        // ------------------------------------------------------------
        // Mock
        // ------------------------------------------------------------

        public enum Group
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

        [Group(Group.A)]
        public class FormA1 : MockForm
        {
        }

        [Group(Group.A)]
        public class FormA2 : MockForm
        {
        }

        [Group(Group.B)]
        public class FormB1 : MockForm
        {
        }

        [Group(Group.B)]
        public class FormB2 : MockForm
        {
        }

        [Group(Group.C)]
        public class FormC1 : MockForm
        {
        }

        [Group(Group.C)]
        public class FormC2 : MockForm
        {
        }
    }
}
