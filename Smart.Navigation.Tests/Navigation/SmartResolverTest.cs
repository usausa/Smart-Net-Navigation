namespace Smart.Navigation;

using Smart.Mock;
using Smart.Navigation.Plugins.Scope;
using Smart.Resolver;

public static class SmartResolverTest
{
    [Fact]
    public static void UseSmartResolver()
    {
        // prepare
        var config = new ResolverConfig();
        config.UseAutoBinding();
        config.Bind<IService>().To<ServiceImplement>().InSingletonScope();
        config.Bind<Setting>().ToSelf().InSingletonScope();
        config.AddNavigator(static c => c.UseMockFormProvider());

        var resolver = config.ToResolver();

        var navigator = resolver.Get<INavigator>();

        // test
        navigator.Forward(typeof(Form1));

        var form1 = (Form1)navigator.CurrentView!;
        Assert.NotNull(form1.Service);
        Assert.NotNull(form1.ScopeObject);
        Assert.NotNull(form1.ScopeObject.Setting);

        navigator.Forward(typeof(Form2));

        var form2 = (Form2)navigator.CurrentView!;
        Assert.Same(form2.Service, form1.Service);
        Assert.Same(form2.Setting, form1.ScopeObject.Setting);
    }

    public sealed class Form1 : MockForm
    {
        public IService Service { get; }

        [Scope]
        public ScopeObject ScopeObject { get; set; } = default!;

        public Form1(IService service)
        {
            Service = service;
        }
    }

    public sealed class Form2 : MockForm
    {
        public IService Service { get; }

        public Setting Setting { get; set; }

        public Form2(IService service, Setting setting)
        {
            Service = service;
            Setting = setting;
        }
    }

    public interface IService
    {
        void Process();
    }

    public sealed class ServiceImplement : IService
    {
        public void Process()
        {
            // Method intentionally left empty.
        }
    }

    public sealed class Setting
    {
    }

    public sealed class ScopeObject
    {
        public Setting Setting { get; }

        public ScopeObject(Setting setting)
        {
            Setting = setting;
        }
    }
}
