namespace Smart.Navigation.Mappers;

using System.Reflection;

using Smart.Mock;
using Smart.Navigation.Mappers.Views;
using Smart.Navigation.Mappers.Views.Children;
using Smart.Navigation.Mappers.Views.Children.GrandChildren;
using Smart.Navigation.Mappers.Views.OtherChildren;

public sealed class PathViewMapperTests
{
    private static Navigator CreateNavigator()
    {
        return new NavigatorConfig()
            .UseMockFormProvider()
            .UsePathViewMapper(static option =>
            {
                option.Root = "Smart.Navigation.Mappers.Views";
                option.Suffix = "Form";
                option.AddAssembly(Assembly.GetExecutingAssembly());
            })
            .ToNavigator();
    }

    [Fact]
    public static void UsePathViewMapperAbsolutePath()
    {
        // Arrange
#pragma warning disable CA2000
        var navigator = CreateNavigator();
#pragma warning restore CA2000

        // Act
        navigator.Forward("/Parent1");

        // Assert
        Assert.Equal(typeof(Parent1Form), navigator.CurrentView!.GetType());

        // Act
        navigator.Forward("/Children/Child1");

        // Assert
        Assert.Equal(typeof(Child1Form), navigator.CurrentView.GetType());
    }

    [Fact]
    public static void UsePathViewMapperRelativePath()
    {
        // Arrange
#pragma warning disable CA2000
        var navigator = CreateNavigator();
#pragma warning restore CA2000

        // Act & Assert
        navigator.Forward("Parent1");
        Assert.Equal(typeof(Parent1Form), navigator.CurrentView!.GetType());

        navigator.Forward("Children/Child1");
        Assert.Equal(typeof(Child1Form), navigator.CurrentView.GetType());

        navigator.Forward("Child2");
        Assert.Equal(typeof(Child2Form), navigator.CurrentView.GetType());

        navigator.Forward("GrandChildren/GrandChild");
        Assert.Equal(typeof(GrandChildForm), navigator.CurrentView.GetType());

        navigator.Forward("../../OtherChildren/OtherChild");
        Assert.Equal(typeof(OtherChildForm), navigator.CurrentView.GetType());

        navigator.Forward("../Parent2");
        Assert.Equal(typeof(Parent2Form), navigator.CurrentView.GetType());
    }

    [Fact]
    public static void Cached()
    {
        // Arrange
        var option = new PathViewMapperOptions
        {
            Root = "Smart.Navigation.Mappers.Views",
            Suffix = "Form"
        };
        option.AddAssembly(Assembly.GetExecutingAssembly());
        var mapper = new PathViewMapper(option, new AssignableTypeConstraint(typeof(MockForm)));

        // Assert
        var descriptor1 = mapper.FindDescriptor("Parent1");
        var descriptor2 = mapper.FindDescriptor("/Parent1");
        Assert.Equal(descriptor1, descriptor2);
    }

    [Theory]
    [InlineData("/../Parent1")]
    [InlineData("../../Parent1")]
    [InlineData("/Children/../../../Parent1")]
    public static void UsePathViewMapperRootOverflow(string path)
    {
        // Arrange
#pragma warning disable CA2000
        var navigator = CreateNavigator();
#pragma warning restore CA2000

        // Act
        navigator.Forward(path);

        // Assert
        Assert.Equal(typeof(Parent1Form), navigator.CurrentView!.GetType());
    }

    // ------------------------------------------------------------
    // Failed
    // ------------------------------------------------------------

    [Fact]
    public static void UsePathViewMapperFailedInvalidIdType()
    {
        // Arrange
#pragma warning disable CA2000
        var navigator = CreateNavigator();
#pragma warning restore CA2000

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(0));
    }

    [Fact]
    public static void UsePathViewMapperFailedNotExists()
    {
        // Arrange
#pragma warning disable CA2000
        var navigator = CreateNavigator();
#pragma warning restore CA2000

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => navigator.Forward("/NotExists"));
    }

    [Fact]
    public static void UsePathViewMapperFailedInvalidType()
    {
        // Arrange
#pragma warning disable CA2000
        var navigator = CreateNavigator();
#pragma warning restore CA2000

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => navigator.Forward("/InvalidType"));
    }
}
