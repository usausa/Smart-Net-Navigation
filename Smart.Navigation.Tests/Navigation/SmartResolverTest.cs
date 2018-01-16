namespace Smart.Navigation
{
    using Smart.Mock;
    using Smart.Navigation.Plugins.Scope;
    using Smart.Resolver;

    using Xunit;

    public static class SmartResolverTest
    {
        [Fact]
        public static void TestUseSmartResolver()
        {
            // prepare
            var config = new ResolverConfig();
            config.UseAutoBinding();
            config.Bind<IService>().To<ServiceImpl>().InSingletonScope();
            config.Bind<Setting>().ToSelf().InSingletonScope();

            var resolver = config.ToResolver();

            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .UseResolver(resolver)
                .ToNavigator();

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));

            // test
            navigator.Forward(Pages.Page1);

            var page1 = (Page1)navigator.CurrentPage;
            Assert.NotNull(page1.Service);
            Assert.NotNull(page1.ScopeObject);
            Assert.NotNull(page1.ScopeObject.Setting);

            navigator.Forward(Pages.Page2);

            var page2 = (Page2)navigator.CurrentPage;
            Assert.Same(page2.Service, page1.Service);
            Assert.Same(page2.Setting, page1.ScopeObject.Setting);
        }

        public enum Pages
        {
            Page1,
            Page2
        }

        public class Page1 : MockPage
        {
            public IService Service { get; }

            [Scope]
            public ScopeObject ScopeObject { get; set; }

            public Page1(IService service)
            {
                Service = service;
            }
        }

        public class Page2 : MockPage
        {
            public IService Service { get; }

            public Setting Setting { get; set; }

            public Page2(IService service, Setting setting)
            {
                Service = service;
                Setting = setting;
            }
        }

        public interface IService
        {
            void Process();
        }

        public class ServiceImpl : IService
        {
            public void Process()
            {
                // Method intentionally left empty.
            }
        }

        public class Setting
        {
        }

        public class ScopeObject
        {
            public Setting Setting { get; }

            public ScopeObject(Setting setting)
            {
                Setting = setting;
            }
        }
    }
}
