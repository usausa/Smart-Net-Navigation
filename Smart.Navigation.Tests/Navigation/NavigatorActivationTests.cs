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
        Assert.Equal(["Activation1Form.OnActivated", "Activation1Form.OnNavigatingTo", "Activation1Form.OnNavigatedTo"], recorder.Events);

        // Act
        recorder.Events.Clear();

        navigator.Forward(typeof(Activation2Form));

        // Assert
        Assert.Equal(
            ["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "Activation2Form.OnActivated", "Activation2Form.OnNavigatingTo", "Activation2Form.OnNavigatedTo"],
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
        Assert.Equal(["Activation1Form.OnActivated", "Activation1Form.OnNavigatingTo", "Activation1Form.OnNavigatedTo"], recorder.Events);

        // Act
        recorder.Events.Clear();

        await navigator.ForwardAsync(typeof(Activation2Form));

        // Assert
        Assert.Equal(
            ["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "Activation2Form.OnActivated", "Activation2Form.OnNavigatingTo", "Activation2Form.OnNavigatedTo"],
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
            ["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "Activation2Form.OnActivated", "Activation2Form.OnNavigatingTo", "Activation2Form.OnNavigatedTo"],
            recorder.Events);

        // Act
        recorder.Events.Clear();

        navigator.Pop();

        // Assert
        Assert.Equal(
            ["Activation2Form.OnDeactivated", "Activation2Form.OnNavigatingFrom", "Activation1Form.OnActivated", "Activation1Form.OnNavigatingTo", "Activation1Form.OnNavigatedTo"],
            recorder.Events);
    }

    // ------------------------------------------------------------
    // Order
    // ------------------------------------------------------------

    [Fact]
    public static void ActivationWhileExecuting()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();
        navigator.ExecutingChanged += (_, _) => recorder.Events.Add(navigator.Executing ? "Executing.Start" : "Executing.End");

        // Act
        navigator.Forward(typeof(Activation1Form));
        recorder.Events.Clear();

        navigator.Forward(typeof(Activation2Form));

        // Assert
        Assert.Equal(
            ["Executing.Start", "Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "Activation2Form.OnActivated", "Activation2Form.OnNavigatingTo", "Activation2Form.OnNavigatedTo", "Executing.End"],
            recorder.Events);
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
        Assert.Equal(
            ["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "ThrowForm.OnActivated", "ThrowForm.OnDeactivated", "Activation1Form.OnActivated"],
            recorder.Events);
        Assert.IsType<Activation1Form>(navigator.CurrentView);
    }

    [Fact]
    public static async Task ReactivateWhenNavigationFailedAsync()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();
        await navigator.ForwardAsync(typeof(Activation1Form));
        recorder.Events.Clear();

        // Act
        await Assert.ThrowsAsync<InvalidOperationException>(() => navigator.ForwardAsync(typeof(ThrowForm)));

        // Assert
        Assert.Equal(
            ["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "ThrowForm.OnActivated", "ThrowForm.OnDeactivated", "Activation1Form.OnActivated"],
            recorder.Events);
        Assert.IsType<Activation1Form>(navigator.CurrentView);
    }

    [Fact]
    public static void ReactivateWhenNavigatingFromFailed()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();
        navigator.Forward(typeof(ThrowFromForm));
        recorder.Events.Clear();

        // Act
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(Activation1Form)));

        // Assert
        Assert.Equal(["ThrowFromForm.OnDeactivated", "ThrowFromForm.OnActivated"], recorder.Events);
        Assert.IsType<ThrowFromForm>(navigator.CurrentView);
    }

    [Fact]
    public static void KeepActivatedWhenNavigatedToFailed()
    {
        // Arrange
        var (navigator, recorder) = CreateNavigator();
        navigator.Forward(typeof(Activation1Form));
        recorder.Events.Clear();

        // Act
        Assert.Throws<InvalidOperationException>(() => navigator.Forward(typeof(ThrowNavigatedForm)));

        // Assert
        Assert.Equal(
            ["Activation1Form.OnDeactivated", "Activation1Form.OnNavigatingFrom", "ThrowNavigatedForm.OnActivated", "ThrowNavigatedForm.OnNavigatingTo"],
            recorder.Events);
        Assert.IsType<ThrowNavigatedForm>(navigator.CurrentView);
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

    public abstract class ActivationForm : MockForm, INavigationEventSupport, INavigationLifecycleSupport
    {
        private readonly EventRecorder recorder;

        protected ActivationForm(EventRecorder recorder)
        {
            this.recorder = recorder;
        }

        public virtual void OnNavigatingFrom(INavigationContext context) => Record(nameof(OnNavigatingFrom));

        public virtual void OnNavigatingTo(INavigationContext context) => Record(nameof(OnNavigatingTo));

        public virtual void OnNavigatedTo(INavigationContext context) => Record(nameof(OnNavigatedTo));

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

    public sealed class ThrowFromForm : ActivationForm
    {
        public ThrowFromForm(EventRecorder recorder)
            : base(recorder)
        {
        }

        public override void OnNavigatingFrom(INavigationContext context) => throw new InvalidOperationException("Test");
    }

    public sealed class ThrowNavigatedForm : ActivationForm
    {
        public ThrowNavigatedForm(EventRecorder recorder)
            : base(recorder)
        {
        }

        public override void OnNavigatedTo(INavigationContext context) => throw new InvalidOperationException("Test");
    }
}
