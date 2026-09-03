namespace Smart.Navigation.Strategies;

using Smart.Mock;

public sealed class PopAndForwardStrategyTests
{
    // ------------------------------------------------------------
    // Navigate
    // ------------------------------------------------------------

    [Fact]
    public static void PopAndForward()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.PopAndForward(typeof(Form3));

        // Assert
        Assert.Equal(1, navigator.StackedCount);
        var form3 = (MockForm)navigator.CurrentView!;
        Assert.Equal(typeof(Form3), form3.GetType());
        Assert.True(form3.IsOpen);
        Assert.True(form3.IsVisible);

        Assert.Equal(typeof(Form2), context.Value.FromId);
        Assert.Equal(typeof(Form3), context.Value.ToId);
        Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
    }

    [Fact]
    public static void PopAndForwardMultiple()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.Push(typeof(Form3));
        navigator.PopAndForward(typeof(Form4), 2);

        // Assert
        Assert.Equal(1, navigator.StackedCount);
        var form4 = (MockForm)navigator.CurrentView!;
        Assert.Equal(typeof(Form4), form4.GetType());
        Assert.True(form4.IsOpen);
        Assert.True(form4.IsVisible);

        Assert.Equal(typeof(Form3), context.Value.FromId);
        Assert.Equal(typeof(Form4), context.Value.ToId);
        Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
    }

    [Fact]
    public static void PopAllAndForward()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.Push(typeof(Form3));
        navigator.PopAllAndForward(typeof(Form4));

        // Assert
        Assert.Equal(1, navigator.StackedCount);
        var form4 = (MockForm)navigator.CurrentView!;
        Assert.Equal(typeof(Form4), form4.GetType());
        Assert.True(form4.IsOpen);
        Assert.True(form4.IsVisible);

        Assert.Equal(typeof(Form3), context.Value.FromId);
        Assert.Equal(typeof(Form4), context.Value.ToId);
        Assert.Equal(NavigationAttributes.None, context.Value.Attribute);
    }

    [Fact]
    public static void PopAndForwardWithParameter()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.PopAndForward(typeof(Form3), new NavigationParameter().SetValue("test"));

        // Assert
        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    [Fact]
    public static void PopAndForwardMultipleWithParameter()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.Push(typeof(Form3));
        navigator.PopAndForward(typeof(Form4), 2, new NavigationParameter().SetValue("test"));

        // Assert
        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    [Fact]
    public static void PopAllAndForwardWithParameter()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        navigator.Push(typeof(Form3));
        navigator.PopAllAndForward(typeof(Form4), new NavigationParameter().SetValue("test"));

        // Assert
        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    // ------------------------------------------------------------
    // Async
    // ------------------------------------------------------------

    [Fact]
    public async Task TestNavigatorPopAndForwardAsync()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PopAndForwardAsync(typeof(Form3));

        // Assert
        Assert.Equal(1, navigator.StackedCount);
        Assert.Equal(typeof(Form3), navigator.CurrentViewId);
    }

    [Fact]
    public async Task TestNavigatorPopAndForwardMultipleAsync()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PushAsync(typeof(Form3));
        await navigator.PopAndForwardAsync(typeof(Form4), 2);

        // Assert
        Assert.Equal(1, navigator.StackedCount);
        Assert.Equal(typeof(Form4), navigator.CurrentViewId);
    }

    [Fact]
    public async Task TestNavigatorPopAllAndForwardAsync()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PushAsync(typeof(Form3));
        await navigator.PopAllAndForwardAsync(typeof(Form4));

        // Assert
        Assert.Equal(1, navigator.StackedCount);
        Assert.Equal(typeof(Form4), navigator.CurrentViewId);
    }

    [Fact]
    public async Task TestNavigatorPopAndForwardWithParameterAsync()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PopAndForwardAsync(typeof(Form3), new NavigationParameter().SetValue("test"));

        // Assert
        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    [Fact]
    public async Task TestNavigatorPopAndForwardMultipleWithParameterAsync()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PushAsync(typeof(Form3));
        await navigator.PopAndForwardAsync(typeof(Form4), 2, new NavigationParameter().SetValue("test"));

        // Assert
        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    [Fact]
    public async Task TestNavigatorPopAllAndForwardWithParameterAsync()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        await navigator.ForwardAsync(typeof(Form1));
        await navigator.PushAsync(typeof(Form2));
        await navigator.PushAsync(typeof(Form3));
        await navigator.PopAllAndForwardAsync(typeof(Form4), new NavigationParameter().SetValue("test"));

        // Assert
        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    // ------------------------------------------------------------
    // Failed
    // ------------------------------------------------------------

    [Fact]
    public static void PopAndForwardFailed2()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act & Assert
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(typeof(Form3), 3));
    }

    [Fact]
    public static void PopAndForwardFailed3()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act & Assert
        navigator.Forward(typeof(Form1));
        navigator.Push(typeof(Form2));
        Assert.Throws<InvalidOperationException>(() => navigator.PopAndForward(typeof(Form3), 0));
    }

    // ------------------------------------------------------------
    // Mock
    // ------------------------------------------------------------

    public sealed class Form1 : MockForm;

    public sealed class Form2 : MockForm;

    public sealed class Form3 : MockForm;

    public sealed class Form4 : MockForm;
}
