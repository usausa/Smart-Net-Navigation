//namespace Smart.Navigation.Strategies
//{
//    using System;
//    using System.Threading.Tasks;

//    using Smart.Mock;

//    using Xunit;

//    public class PushStrategyTest
//    {
//        // ------------------------------------------------------------
//        // Navigate
//        // ------------------------------------------------------------

//        [Fact]
//        public static void TestNavigatorPush()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            navigator.Register(ViewId.Form1, typeof(Form1));
//            navigator.Register(ViewId.Form2, typeof(Form2));
//            navigator.Register(ViewId.Form3, typeof(Form3));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);

//            Assert.Equal(1, navigator.StackedCount);
//            var form1 = (MockForm)navigator.CurrentView;
//            Assert.Equal(typeof(Form1), form1.GetType());
//            Assert.True(form1.IsOpen);

//            navigator.Push(ViewId.Form2);

//            Assert.Equal(2, navigator.StackedCount);
//            var form2 = (MockForm)navigator.CurrentView;
//            Assert.Equal(typeof(Form2), form2.GetType());
//            Assert.True(form2.IsOpen);
//            Assert.True(form1.IsOpen);
//            Assert.False(form1.IsVisible);

//            Assert.Equal(ViewId.Form1, context.Value.FromId);
//            Assert.Equal(ViewId.Form2, context.Value.ToId);
//            Assert.True(context.Value.Attribute.IsStacked());

//            navigator.Push(ViewId.Form3);

//            Assert.Equal(3, navigator.StackedCount);
//            var form3 = (MockForm)navigator.CurrentView;
//            Assert.Equal(typeof(Form3), form3.GetType());
//            Assert.True(form3.IsOpen);
//            Assert.True(form2.IsOpen);
//            Assert.False(form2.IsVisible);

//            Assert.Equal(ViewId.Form2, context.Value.FromId);
//            Assert.Equal(ViewId.Form3, context.Value.ToId);
//            Assert.True(context.Value.Attribute.IsStacked());
//        }

//        [Fact]
//        public static void TestNavigatorPushWithParameter()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            navigator.Register(ViewId.Form1, typeof(Form1));
//            navigator.Register(ViewId.Form2, typeof(Form2));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            navigator.Forward(ViewId.Form1);

//            navigator.Push(ViewId.Form2, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        // ------------------------------------------------------------
//        // Async
//        // ------------------------------------------------------------

//        [Fact]
//        public static async Task TestNavigatorPushAsync()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            navigator.Register(ViewId.Form1, typeof(Form1));
//            navigator.Register(ViewId.Form2, typeof(Form2));
//            navigator.Register(ViewId.Form3, typeof(Form3));

//            // test
//            await navigator.ForwardAsync(ViewId.Form1);

//            Assert.Equal(1, navigator.StackedCount);
//            Assert.Equal(ViewId.Form1, navigator.CurrentViewId);

//            await navigator.PushAsync(ViewId.Form2);

//            Assert.Equal(2, navigator.StackedCount);
//            Assert.Equal(ViewId.Form2, navigator.CurrentViewId);

//            await navigator.PushAsync(ViewId.Form3);

//            Assert.Equal(3, navigator.StackedCount);
//            Assert.Equal(ViewId.Form3, navigator.CurrentViewId);
//        }

//        [Fact]
//        public static async Task TestNavigatorPushAsyncWithParameter()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            navigator.Register(ViewId.Form1, typeof(Form1));
//            navigator.Register(ViewId.Form2, typeof(Form2));

//            var context = new Holder<INavigationContext>();
//            navigator.Navigating += (sender, args) => { context.Value = args.Context; };

//            // test
//            await navigator.ForwardAsync(ViewId.Form1);

//            await navigator.PushAsync(ViewId.Form2, new NavigationParameter().SetValue("test"));

//            Assert.NotNull(context.Value);
//            Assert.Equal("test", context.Value.Parameter.GetValue<string>());
//        }

//        // ------------------------------------------------------------
//        // Failed
//        // ------------------------------------------------------------

//        [Fact]
//        public static void TestNavigatorPushFailed()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            // test
//            Assert.Throws<InvalidOperationException>(() => navigator.Forward(ViewId.Form1));
//        }

//        // ------------------------------------------------------------
//        // Mock
//        // ------------------------------------------------------------

//        public enum ViewId
//        {
//            Form1,
//            Form2,
//            Form3
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
//    }
//}
