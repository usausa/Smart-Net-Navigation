namespace Smart.Navigation;

using Microsoft.Extensions.DependencyInjection;

using Smart.Mock;
using Smart.Navigation.Plugins.Scope;

public static class ServiceProviderTest
{
    [Fact]
    public static void UseServiceProvider()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddSingleton<IService, ServiceImplement>();
        services.AddSingleton<Setting>();
        services.AddTransient<ScopeObject>();
        services.AddTransient<Form1>();
        services.AddTransient<Form2>();
        services.AddNavigator(static (provider, config) =>
        {
            config.UseMockFormProvider();
            config.UseServiceProvider(provider);
        });
        var provider = services.BuildServiceProvider();

        var navigator = provider.GetRequiredService<INavigator>();

        // Act
        navigator.Forward(typeof(Form1));

        // Assert
        var form1 = (Form1)navigator.CurrentView!;
        Assert.NotNull(form1.Service);
        Assert.NotNull(form1.ScopeObject);
        Assert.NotNull(form1.ScopeObject.Setting);

        // Act
        navigator.Forward(typeof(Form2));

        // Assert
        var form2 = (Form2)navigator.CurrentView!;
        Assert.Same(form2.Service, form1.Service);
        Assert.Same(form2.Setting, form1.ScopeObject.Setting);
    }

    [Fact]
    public static void UseActivatorUtilities()
    {
        // Arrange: views and scope objects are not registered
        var services = new ServiceCollection();
        services.AddSingleton<IService, ServiceImplement>();
        services.AddSingleton<Setting>();
        services.AddNavigator(static (provider, config) =>
        {
            config.UseMockFormProvider();
            config.UseActivatorUtilities(provider);
        });
        var provider = services.BuildServiceProvider();

        var navigator = provider.GetRequiredService<INavigator>();

        // Act
        navigator.Forward(typeof(Form1));

        // Assert
        var form1 = (Form1)navigator.CurrentView!;
        Assert.NotNull(form1.Service);
        Assert.NotNull(form1.ScopeObject);
        Assert.NotNull(form1.ScopeObject.Setting);

        // Act
        navigator.Forward(typeof(Form2));

        // Assert
        var form2 = (Form2)navigator.CurrentView!;
        Assert.Same(form2.Service, form1.Service);
        Assert.Same(form2.Setting, form1.ScopeObject.Setting);

        // views are caller owned: the open view is not disposed with the root provider
        provider.Dispose();

        Assert.False(form2.IsDisposed);
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
