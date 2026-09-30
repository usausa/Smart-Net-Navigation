namespace Smart.Navigation;

using Smart.Mock;
using Smart.Resolver;

public sealed class NavigatorActivationTests
{
    // ------------------------------------------------------------
    // Forward
    // ------------------------------------------------------------

    [Fact]
    public static void Forward()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();

        // Act
        navigator.Forward(typeof(Activation1Form));

        // Assert
        Assert.Equal(["Activation1Form.OnNavigatingTo", "Activation1Form.OnNavigatedTo", "Activation1Form.OnActivated"], recorder.Events);

        // Act
        recorder.Events.Clear();

        navigator.Forward(typeof(Activation2Form));

        // Assert
        Assert.Equal(
            ["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "Activation2Form.OnNavigatingTo", "Activation2Form.OnNavigatedTo", "Activation2Form.OnActivated"],
            recorder.Events);
    }

    [Fact]
    public static async Task ForwardAsync()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();

        // Act
        await navigator.ForwardAsync(typeof(Activation1Form));

        // Assert
        Assert.Equal(["Activation1Form.OnNavigatingTo", "Activation1Form.OnNavigatedTo", "Activation1Form.OnActivated"], recorder.Events);

        // Act
        recorder.Events.Clear();

        await navigator.ForwardAsync(typeof(Activation2Form));

        // Assert
        Assert.Equal(
            ["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "Activation2Form.OnNavigatingTo", "Activation2Form.OnNavigatedTo", "Activation2Form.OnActivated"],
            recorder.Events);
    }

    // ------------------------------------------------------------
    // Stack
    // ------------------------------------------------------------

    [Fact]
    public static void PushAndPop()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();
        navigator.Forward(typeof(Activation1Form));
        recorder.Events.Clear();

        // Act
        navigator.Push(typeof(Activation2Form));

        // Assert
        Assert.Equal(
            ["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "Activation2Form.OnNavigatingTo", "Activation2Form.OnNavigatedTo", "Activation2Form.OnActivated"],
            recorder.Events);

        // Act
        recorder.Events.Clear();

        navigator.Pop();

        // Assert
        Assert.Equal(
            ["Activation2Form.OnDeactivated", "Activation2Form.OnNavigatingFrom", "Activation1Form.OnNavigatingTo", "Activation1Form.OnNavigatedTo", "Activation1Form.OnActivated"],
            recorder.Events);
    }

    // ------------------------------------------------------------
    // Order
    // ------------------------------------------------------------

    [Fact]
    public static void ActivatedBeforeExecutingChanged()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();
        navigator.ExecutingChanged += (_, _) => recorder.Events.Add(navigator.Executing ? "Executing.Start" : "Executing.End");

        // Act
        navigator.Forward(typeof(Activation1Form));
        recorder.Events.Clear();

        navigator.Forward(typeof(Activation2Form));

        // Assert
        Assert.Equal("Executing.Start", recorder.Events[0]);
        Assert.Equal("Activation1Form.OnDeactivated", recorder.Events[1]);
        Assert.Equal("Activation2Form.OnActivated", recorder.Events[^2]);
        Assert.Equal("Executing.End", recorder.Events[^1]);
    }

    // ------------------------------------------------------------
    // Failure
    // ------------------------------------------------------------

    [Fact]
    public static void ReactivateWhenNavigationFailed()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();
        navigator.Forward(typeof(Activation1Form));
        recorder.Events.Clear();

        // Act
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(ThrowForm)));

        // Assert
        Assert.Equal(["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "Activation1Form.OnActivated"], recorder.Events);
        Assert.IsType<Activation1Form>(navigator.CurrentView);
    }

    // ------------------------------------------------------------
    // Exit
    // ------------------------------------------------------------

    [Fact]
    public static void DeactivateWhenExit()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();
        navigator.Forward(typeof(Activation1Form));
        recorder.Events.Clear();

        // Act
        navigator.Exit();

        // Assert
        Assert.Equal(["Activation1Form.OnDeactivated"], recorder.Events);
    }

    // ------------------------------------------------------------
    // Helper
    // ------------------------------------------------------------

    private static (INavigator Navigator, EventRecorder Recorder) CreateNavigator()
    {
        var resolver = new ResolverConfig()
            .UseAutoBinding()
            .Also(static config => config.Bind<EventRecorder>().ToSelf().InSingletonScope())
            .ToResolver();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseActivator(resolver)
            .ToNavigator();
        return (navigator, resolver.Get<EventRecorder>());
    }

    public abstract class ActivationForm : MockForm, INavigationEventSupport, IActivationSupport
    {
        private readonly EventRecorder recorder;

        protected ActivationForm(EventRecorder recorder)
        {
            this.recorder = recorder;
        }

        public void OnNavigatingFrom(INavigationContext context) => Record(nameof(OnNavigatingFrom));

        public virtual void OnNavigatingTo(INavigationContext context) => Record(nameof(OnNavigatingTo));

        public void OnNavigatedTo(INavigationContext context) => Record(nameof(OnNavigatedTo));

        public void OnActivated() => Record(nameof(OnActivated));

        public void OnDeactivated() => Record(nameof(OnDeactivated));

        private void Record(string name) => recorder.Events.Add($"{GetType().Name}.{name}");
    }

    public sealed class Activation1Form : ActivationForm
    {
        public Activation1Form(EventRecorder recorder)
            : base(recorder)
        {
        }
    }

    public sealed class Activation2Form : ActivationForm
    {
        public Activation2Form(EventRecorder recorder)
            : base(recorder)
        {
        }
    }

    public sealed class ThrowForm : ActivationForm
    {
        public ThrowForm(EventRecorder recorder)
            : base(recorder)
        {
        }

        public override void OnNavigatingTo(INavigationContext context) => throw new InvalidOperationException("Test");
    }
}
