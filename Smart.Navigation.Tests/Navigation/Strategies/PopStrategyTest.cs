namespace Smart.Navigation.Strategies;

using Smart.Mock;

public sealed class PopStrategyTest
{
    // ------------------------------------------------------------
    // Navigate
    // ------------------------------------------------------------

    [Fact]
    public static void Pop()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // test
        navigator.Forward(typeof(Form1));

        var form1 = (MockForm)navigator.CurrentView!;
        form1.Focused = "text1";

        navigator.Push(typeof(Form2));
        navigator.Pop();

        Assert.Equal(1, navigator.StackedCount);
        var form1B = (MockForm)navigator.CurrentView!;
        Assert.Same(form1, form1B);
        Assert.Equal(typeof(Form1), form1B.GetType());
        Assert.True(form1B.IsOpen);
        Assert.True(form1B.IsVisible);
        Assert.Equal("text1", form1B.Focused);

        Assert.Equal(typeof(Form2), context.Value.FromId);
        Assert.Equal(typeof(Form1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());
    }

    [Fact]
    public static void PopMultiple()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // test
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.Push(typeof(Form3));
        navigator.Pop(2);

        Assert.Equal(1, navigator.StackedCount);
        var form1 = (MockForm)navigator.CurrentView!;
        Assert.Equal(typeof(Form1), form1.GetType());
        Assert.True(form1.IsOpen);
        Assert.True(form1.IsVisible);

        Assert.Equal(typeof(Form3), context.Value.FromId);
        Assert.Equal(typeof(Form1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());
    }

    [Fact]
    public static void PopWithParameter()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // test
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.Pop(new NavigationParameter().SetValue("test"));

        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    [Fact]
    public static void PopMultipleWithParameter()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // test
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.Push(typeof(Form3));
        navigator.Pop(2, new NavigationParameter().SetValue("test"));

        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    // ------------------------------------------------------------
    // Async
    // ------------------------------------------------------------

    [Fact]
    public async Task TestNavigatorPopAsync()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PopAsync();

        Assert.Equal(1, navigator.StackedCount);
        Assert.Equal(typeof(Form1), navigator.CurrentViewId);
    }

    [Fact]
    public async Task TestNavigatorPopAsyncMultiple()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PushAsync(typeof(Form3));
        await navigator.PopAsync(2);

        Assert.Equal(1, navigator.StackedCount);
        Assert.Equal(typeof(Form1), navigator.CurrentViewId);
    }

    [Fact]
    public async Task TestNavigatorPopAsyncWithParameter()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // test
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PopAsync(new NavigationParameter().SetValue("test"));

        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    [Fact]
    public async Task TestNavigatorPopAsyncMultipleWithParameter()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // test
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PushAsync(typeof(Form3));
        await navigator.PopAsync(2, new NavigationParameter().SetValue("test"));

        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    [Fact]
    public static void PopFailed()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        Assert.Throws<InvalidOperationException>(() => navigator.Pop(2));
    }

    [Fact]
    public static void PopFailed2()
    {
        // prepare
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // test
        Assert.Throws<InvalidOperationException>(() => navigator.Pop(0));
    }

    // ------------------------------------------------------------
    // Mock
    // ------------------------------------------------------------

    public sealed class Form1 : MockForm
    {
    }

    public sealed class Form2 : MockForm
    {
    }

    public sealed class Form3 : MockForm
    {
    }
}
