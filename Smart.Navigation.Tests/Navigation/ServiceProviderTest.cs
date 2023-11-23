namespace Smart.Navigation;

using Microsoft.Extensions.DependencyInjection;

using Smart.Mock;
using Smart.Navigation.Plugins.Scope;

using Xunit;

public static class ServiceProviderTest
{
    [Fact]
    public static void UseServiceProvider()
    {
        // prepare
        var services = new ServiceCollection();
        services.AddSingleton<IService, ServiceImpl>();
        services.AddSingleton<Setting>();
        services.AddTransient<ScopeObject>();
        services.AddTransient<Form1>();
        services.AddTransient<Form2>();
        services.AddNavigator(static config => config.UseMockFormProvider());
        var provider = services.BuildServiceProvider();

        var navigator = provider.GetRequiredService<INavigator>();

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

    public class Form1 : MockForm
    {
        public IService Service { get; }

        [Scope]
        public ScopeObject ScopeObject { get; set; } = default!;

        public Form1(IService service)
        {
            Service = service;
        }
    }

    public class Form2 : MockForm
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Ignore")]
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
