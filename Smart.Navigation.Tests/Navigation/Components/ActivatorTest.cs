namespace Smart.Navigation.Components;

using Smart.Mock;
using Smart.Navigation.Plugins.Scope;

public static class ActivatorTest
{
    [Fact]
    public static void UseDelegateActivator()
    {
        var created = new List<Type>();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseActivator(type =>
            {
                created.Add(type);
                return Activator.CreateInstance(type);
            })
            .ToNavigator();

        navigator.Forward(typeof(ActivatorForm));

        var form = (ActivatorForm)navigator.CurrentView!;
        Assert.NotNull(form.Data);
        Assert.Contains(typeof(ActivatorForm), created);
        Assert.Contains(typeof(ActivatorScopeData), created);
    }

    [Fact]
    public static void DelegateActivatorNullThrows()
    {
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseActivator(static _ => null)
            .ToNavigator();

        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(ActivatorForm)));
    }

    public sealed class ActivatorForm : MockForm
    {
        [Scope]
        public ActivatorScopeData Data { get; set; } = default!;
    }

    public sealed class ActivatorScopeData
    {
    }
}
