//namespace Smart.Navigation
//{
//    using System.Threading.Tasks;

//    using Smart.Mock;

//    using Xunit;

//    public class NavigatorConfirmTest
//    {
//        [Fact]
//        public static void TestNavigatorCanceldByEvent()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();
//            navigator.Confirm += (sender, args) =>
//            {
//                args.Cancel = args.Context.Parameter.GetValue<bool>("Cancel");
//            };

//            navigator.Register(ViewId.ToForm, typeof(ToForm));

//            // test
//            Assert.False(navigator.Forward(ViewId.ToForm, new NavigationParameter().SetValue("Cancel", true)));
//            Assert.True(navigator.Forward(ViewId.ToForm, new NavigationParameter().SetValue("Cancel", false)));
//        }

//        [Fact]
//        public static void TestNavigatorCanceldByInterface()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            navigator.Register(ViewId.CancelForm, typeof(CancelForm));
//            navigator.Register(ViewId.ToForm, typeof(ToForm));

//            // test
//            navigator.Forward(ViewId.CancelForm);

//            Assert.False(navigator.Forward(ViewId.ToForm, new NavigationParameter().SetValue("CanNavigate", false)));
//            Assert.True(navigator.Forward(ViewId.ToForm, new NavigationParameter().SetValue("CanNavigate", true)));
//        }

//        [Fact]
//        public static async Task TestNavigatorCanceldByAsyncInterface()
//        {
//            // prepare
//            var navigator = new NavigatorConfig()
//                .UseMockFormProvider()
//                .ToNavigator();

//            navigator.Register(ViewId.CancelForm, typeof(CancelAsyncForm));
//            navigator.Register(ViewId.ToForm, typeof(ToForm));

//            // test
//            await navigator.ForwardAsync(ViewId.CancelForm);

//            Assert.False(await navigator.ForwardAsync(ViewId.ToForm, new NavigationParameter().SetValue("CanNavigate", false)));
//            Assert.True(await navigator.ForwardAsync(ViewId.ToForm, new NavigationParameter().SetValue("CanNavigate", true)));
//        }

//        public enum ViewId
//        {
//            ToForm,
//            CancelForm,
//            CancelAsyncForm
//        }

//        public class ToForm : MockForm
//        {
//        }

//        public class CancelForm : MockForm, IConfirmRequest
//        {
//            public bool CanNavigate(INavigationContext context)
//            {
//                return context.Parameter.GetValue<bool>("CanNavigate");
//            }
//        }

//        public class CancelAsyncForm : MockForm, IConfirmRequestAsync
//        {
//            public async Task<bool> CanNavigateAsync(INavigationContext context)
//            {
//                await Task.Delay(0);
//                return context.Parameter.GetValue<bool>("CanNavigate");
//            }
//        }
//    }
//}
