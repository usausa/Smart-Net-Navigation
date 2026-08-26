namespace Smart.Navigation.Strategies;

using Smart.Mock;

public sealed class GroupPushStrategyTest
{
    // ------------------------------------------------------------
    // Navigate
    // ------------------------------------------------------------

    [Fact]
    public static void GropedPushNewGroup()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        // A1 B1       new []
        // A1 B1 C1    D:B1 C:C1
        navigator.Forward(typeof(FormA1));

        navigator.GroupPush(typeof(FormB1));

        var formB1 = (FormB1)navigator.CurrentView!;

        navigator.GroupPush(typeof(FormC1));

        // Assert
        Assert.Equal(3, navigator.StackedCount);
        Assert.False(formB1.IsVisible);

        Assert.Equal(typeof(FormB1), context.Value.FromId);
        Assert.Equal(typeof(FormC1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsStacked());
    }

    [Fact]
    public static void GropedPushNewAndBring()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        // A1 B1       new [0]
        // B1 A1 A2    D:B1 C:A2
        navigator.Forward(typeof(FormA1));

        var formA1 = (FormA1)navigator.CurrentView!;

        navigator.GroupPush(typeof(FormB1));

        var formB1 = (FormB1)navigator.CurrentView!;

        navigator.GroupPush(typeof(FormA2));

        // Assert
        Assert.Equal(3, navigator.StackedCount);
        Assert.False(formB1.IsVisible);

        Assert.Equal(typeof(FormB1), context.Value.FromId);
        Assert.Equal(typeof(FormA2), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsStacked());

        // Act
        navigator.Pop();

        // Assert
        Assert.True(formA1.IsVisible);
        Assert.False(formB1.IsVisible);

        Assert.Equal(typeof(FormA2), context.Value.FromId);
        Assert.Equal(typeof(FormA1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());

        // Act
        navigator.Pop();

        // Assert
        Assert.False(formA1.IsOpen);
        Assert.True(formB1.IsVisible);

        Assert.Equal(typeof(FormA1), context.Value.FromId);
        Assert.Equal(typeof(FormB1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());
    }

    [Fact]
    public static void GropedPushNewAndNotBring()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        // A1 B1       new [1]
        // A1 B1 B2    D:B1 C:B2
        navigator.Forward(typeof(FormA1));

        var formA1 = (FormA1)navigator.CurrentView!;

        navigator.GroupPush(typeof(FormB1));

        var formB1 = (FormB1)navigator.CurrentView!;

        navigator.GroupPush(typeof(FormB2));

        // Assert
        Assert.Equal(3, navigator.StackedCount);
        Assert.False(formB1.IsVisible);

        Assert.Equal(typeof(FormB1), context.Value.FromId);
        Assert.Equal(typeof(FormB2), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsStacked());

        // Act
        navigator.Pop();

        // Assert
        Assert.False(formA1.IsVisible);
        Assert.True(formB1.IsVisible);

        Assert.Equal(typeof(FormB2), context.Value.FromId);
        Assert.Equal(typeof(FormB1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());

        // Act
        navigator.Pop();

        // Assert
        Assert.True(formA1.IsVisible);
        Assert.False(formB1.IsOpen);

        Assert.Equal(typeof(FormB1), context.Value.FromId);
        Assert.Equal(typeof(FormA1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());
    }

    [Fact]
    public static void GropedPushExistAndBring()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        // A1 B1       exist [0]
        // B1 A1       D:B1 A:A1
        navigator.Forward(typeof(FormA1));

        var formA1 = (FormA1)navigator.CurrentView!;

        navigator.GroupPush(typeof(FormB1));

        var formB1 = (FormB1)navigator.CurrentView!;

        navigator.GroupPush(typeof(FormA1));

        // Assert
        Assert.Equal(2, navigator.StackedCount);
        Assert.True(formA1.IsVisible);
        Assert.False(formB1.IsVisible);

        Assert.Equal(typeof(FormB1), context.Value.FromId);
        Assert.Equal(typeof(FormA1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());

        // Act
        navigator.Pop();

        // Assert
        Assert.False(formA1.IsOpen);
        Assert.True(formB1.IsVisible);

        Assert.Equal(typeof(FormA1), context.Value.FromId);
        Assert.Equal(typeof(FormB1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());
    }

    [Fact]
    public static void GropedPushExistAndNotBring()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        // A1 B1       exist [1]
        // A1 B1       -
        navigator.Forward(typeof(FormA1));

        var formA1 = (FormA1)navigator.CurrentView!;

        navigator.GroupPush(typeof(FormB1));

        var formB1 = (FormB1)navigator.CurrentView!;

        // Act & Assert
        Assert.False(navigator.GroupPush(typeof(FormB1)));

        // Assert
        Assert.Equal(2, navigator.StackedCount);
        Assert.False(formA1.IsVisible);
        Assert.True(formB1.IsVisible);

        // Act
        navigator.Pop();

        // Assert
        Assert.True(formA1.IsVisible);
        Assert.False(formB1.IsOpen);

        Assert.Equal(typeof(FormB1), context.Value.FromId);
        Assert.Equal(typeof(FormA1), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());
    }

    [Fact]
    public static void GropedPushUseCase()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        navigator.Forward(typeof(Form1));

        // group A
        // 1:A1
        navigator.GroupPush(typeof(FormA1));
        var formA1 = (FormA1)navigator.CurrentView!;

        // 1:A1:A2
        navigator.GroupPush(typeof(FormA2));
        var formA2 = (FormA2)navigator.CurrentView!;

        // group B
        // 1:A1:A2:B1
        navigator.GroupPush(typeof(FormB1));
        var formB1 = (FormB1)navigator.CurrentView!;

        // 1:A1:A2:B1:B2
        navigator.GroupPush(typeof(FormB2));
        var formB2 = (FormB2)navigator.CurrentView!;

        // group A
        // 1:B1:B2:A1:A2
        navigator.GroupPush(typeof(FormA1));

        // Assert
        Assert.True(formA2.IsVisible);
        Assert.False(formA1.IsVisible);
        Assert.False(formB2.IsVisible);
        Assert.False(formB1.IsVisible);

        Assert.Equal(typeof(FormB2), context.Value.FromId);
        Assert.Equal(typeof(FormA2), context.Value.ToId);
        Assert.True(context.Value.Attribute.IsRestore());

        // Act
        // 1:B1:B2:A1
        navigator.Pop();

        // Assert
        Assert.True(formA1.IsVisible);
        Assert.False(formB2.IsVisible);
        Assert.False(formB1.IsVisible);

        // Act
        // group B
        // 1:A1:B1:B2
        navigator.GroupPush(typeof(FormB1));

        // Assert
        Assert.True(formB2.IsVisible);
        Assert.False(formB1.IsVisible);
        Assert.False(formA1.IsVisible);

        // Act
        // 1:A1:B1
        navigator.Pop();

        // Assert
        Assert.True(formB1.IsVisible);
        Assert.False(formA1.IsVisible);
    }

    [Fact]
    public static void GropedPushWithParameter()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        navigator.Forward(typeof(FormA1));

        navigator.GroupPush(typeof(FormB1), new NavigationParameter().SetValue("test"));

        // Assert
        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    // ------------------------------------------------------------
    // Async
    // ------------------------------------------------------------

    [Fact]
    public async Task TestNavigatorGropedPushAsyncExistAndNotBring()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act
        await navigator.ForwardAsync(typeof(FormA1));

        var formA1 = (FormA1)navigator.CurrentView!;

        await navigator.GroupPushAsync(typeof(FormB1));

        var formB1 = (FormB1)navigator.CurrentView!;

        // Act & Assert
        Assert.False(await navigator.GroupPushAsync(typeof(FormB1)));

        // Assert
        Assert.Equal(2, navigator.StackedCount);
        Assert.False(formA1.IsVisible);
        Assert.True(formB1.IsVisible);

        // Act
        await navigator.PopAsync();

        // Assert
        Assert.True(formA1.IsVisible);
        Assert.False(formB1.IsOpen);
    }

    [Fact]
    public async Task TestNavigatorGropedPushAsyncWithParameter()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigating += (_, args) => { context.Value = args.Context; };

        // Act
        await navigator.ForwardAsync(typeof(FormA1));

        await navigator.GroupPushAsync(typeof(FormB1), new NavigationParameter().SetValue("test"));

        // Assert
        Assert.NotNull(context.Value);
        Assert.Equal("test", context.Value.Parameter.GetValue<string>());
    }

    // ------------------------------------------------------------
    // Failed
    // ------------------------------------------------------------

    [Fact]
    public static void GropedPushFailed()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act & Assert
        navigator.Forward(typeof(Form1));
        Assert.Throws<InvalidOperationException>(() => navigator.GroupPush(typeof(Form2)));
    }

    [Fact]
    public static void GropedPushFailed2()
    {
        // Arrange
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .ToNavigator();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => navigator.GroupPush(typeof(Form1)));
    }

    // ------------------------------------------------------------
    // Mock
    // ------------------------------------------------------------

    public enum Group
    {
        A,
        B,
        C
    }

    public sealed class Form1 : MockForm
    {
    }

    public sealed class Form2 : MockForm
    {
    }

    [Group(Group.A)]
    public sealed class FormA1 : MockForm
    {
    }

    [Group(Group.A)]
    public sealed class FormA2 : MockForm
    {
    }

    [Group(Group.B)]
    public sealed class FormB1 : MockForm
    {
    }

    [Group(Group.B)]
    public sealed class FormB2 : MockForm
    {
    }

    [Group(Group.C)]
    public sealed class FormC1 : MockForm
    {
    }

    [Group(Group.C)]
    public sealed class FormC2 : MockForm
    {
    }
}
