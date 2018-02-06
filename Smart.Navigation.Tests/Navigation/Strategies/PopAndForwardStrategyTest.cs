//namespace Smart.Navigation.Strategies
//{
//    using System;
//    using System.Threading.Tasks;

//    using Smart.Mock;

//    using Xunit;

//    public class PopAndForwardStrategyTest
//    {
//        // ------------------------------------------------------------
//        // Navigate
//        // ------------------------------------------------------------

//        [Fact]
//        public static void TestNavigatorPopAndForward()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m =>
//                {
//                    m.Register(ViewId.Form1, typeof(Form1));
//                    m.Register(ViewId.Form2, typeof(Form2));
//                    m.Register(ViewId.Form3, typeof(Form3));
//                })
//                .ToNavigator();

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            navigator.PopAndForward(ViewId.Form3);

//            Assert.Equal(1, navigator.StackedCount);
//            var form3 = (MockForm)navigator.CurrentView;
//            Assert.Equal(typeof(Form3), form3.GetType());
//            Assert.True(form3.IsOpen);
//            Assert.True(form3.IsVisible);

//            Assert.Equal(ViewId.Form2, context.Value.FromId);
//            Assert.Equal(ViewId.Form3, context.Value.ToId);
//            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
//        }

//        [Fact]
//        public static void TestNavigatorPopAndForwardMultiple()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));
//            m.Register(ViewId.Form3, typeof(Form3));
//            m.Register(ViewId.Form4, typeof(Form4));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            navigator.Push(ViewId.Form3);
//            navigator.PopAndForward(ViewId.Form4, 2);

//            Assert.Equal(1, navigator.StackedCount);
//            var form4 = (MockForm)navigator.CurrentView;
//            Assert.Equal(typeof(Form4), form4.GetType());
//            Assert.True(form4.IsOpen);
//            Assert.True(form4.IsVisible);

//            Assert.Equal(ViewId.Form3, context.Value.FromId);
//            Assert.Equal(ViewId.Form4, context.Value.ToId);
//            Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
//        }

//        [Fact]
//        public static void TestNavigatorPopAndForwardWithParameter()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));
//            m.Register(ViewId.Form3, typeof(Form3));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            navigator.PopAndForward(ViewId.Form3, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        [Fact]
//        public static void TestNavigatorPopAndForwardMultipleWithParameter()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));
//            m.Register(ViewId.Form3, typeof(Form3));
//            m.Register(ViewId.Form4, typeof(Form4));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            navigator.Push(ViewId.Form3);
//            navigator.PopAndForward(ViewId.Form4, 2, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        // ------------------------------------------------------------
//        // Async
//        // ------------------------------------------------------------

//        [Fact]
//        public static async Task TestNavigatorPopAndForwardAsync()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));
//            m.Register(ViewId.Form3, typeof(Form3));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            await navigator.ForwardAsync(ViewId.Form1);
//            await navigator.PushAsync(ViewId.Form2);
//            await navigator.PopAndForwardAsync(ViewId.Form3);

//            Assert.Equal(1, navigator.StackedCount);
//            Assert.Equal(ViewId.Form3, navigator.CurrentViewId);
//        }

//        [Fact]
//        public static async Task TestNavigatorPopAndForwardMultipleAsync()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));
//            m.Register(ViewId.Form3, typeof(Form3));
//            m.Register(ViewId.Form4, typeof(Form4));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            await navigator.ForwardAsync(ViewId.Form1);
//            await navigator.PushAsync(ViewId.Form2);
//            await navigator.PushAsync(ViewId.Form3);
//            await navigator.PopAndForwardAsync(ViewId.Form4, 2);

//            Assert.Equal(1, navigator.StackedCount);
//            Assert.Equal(ViewId.Form4, navigator.CurrentViewId);
//        }

//        [Fact]
//        public static async Task TestNavigatorPopAndForwardWithParameterAsync()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));
//            m.Register(ViewId.Form3, typeof(Form3));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            await navigator.ForwardAsync(ViewId.Form1);
//            await navigator.PushAsync(ViewId.Form2);
//            await navigator.PopAndForwardAsync(ViewId.Form3, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        [Fact]
//        public static async Task TestNavigatorPopAndForwardMultipleWithParameterAsync()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));
//            m.Register(ViewId.Form3, typeof(Form3));
//            m.Register(ViewId.Form4, typeof(Form4));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            await navigator.ForwardAsync(ViewId.Form1);
//            await navigator.PushAsync(ViewId.Form2);
//            await navigator.PushAsync(ViewId.Form3);
//            await navigator.PopAndForwardAsync(ViewId.Form4, 2, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        // ------------------------------------------------------------
//        // Failed
//        // ------------------------------------------------------------

//        [Fact]
//        public static void TestNavigatorPopAndForwardFailed()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(ViewId.Form3));
//        }

//        [Fact]
//        public static void TestNavigatorPopAndForwardFailed2()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));
//            m.Register(ViewId.Form3, typeof(Form3));

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(ViewId.Form3, 3));
//        }

//        [Fact]
//        public static void TestNavigatorPopAndForwardFailed3()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            m.Register(ViewId.Form1, typeof(Form1));
//            m.Register(ViewId.Form2, typeof(Form2));
//            m.Register(ViewId.Form3, typeof(Form3));

//            // test
//            navigator.Forward(ViewId.Form1);
//            navigator.Push(ViewId.Form2);
//            Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(ViewId.Form3, 0));
//        }

//        // ------------------------------------------------------------
//        // Mock
//        // ------------------------------------------------------------

//        public enum ViewId
//        {
//            Form1,
//            Form2,
//            Form3,
//            Form4
//        }

//        public class Form1 : MockForm
//        {
//        }

//        public class Form2 : MockForm
//        {
//        }

//        public class Form3 : MockForm
//        {
//        }

//        public class Form4 : MockForm
//        {
//        }
//    }
//}
