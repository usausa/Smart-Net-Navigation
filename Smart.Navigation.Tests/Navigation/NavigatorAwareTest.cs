//namespace Smart.Navigation
//{
//    using Smart.Mock;

//    using Xunit;

//    public class NavigatorAwareTest
//    {
//        [Fact]
//        public static void TestNavigatorAware()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .UseIdMapper(m => m.Register(ViewId.AwareForm, typeof(AwareForm)))
//                .ToNavigator();

//            // test
//            navigator.Forward(ViewId.AwareForm);

//            var awareForm = (AwareForm)navigator.CurrentView;
//            Assert.Same(navigator, awareForm.Navigator);
//        }

//        public enum ViewId
//        {
//            AwareForm
//        }

//        public class AwareForm : MockForm, INavigatorAware
//        {
//            public INavigator Navigator { get; set; }
//        }
//    }
//}
