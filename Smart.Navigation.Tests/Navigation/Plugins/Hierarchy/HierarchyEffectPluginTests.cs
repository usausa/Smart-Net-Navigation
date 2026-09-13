namespace Smart.Navigation.Plugins.Hierarchy;

using Smart.Mock;
using Smart.Navigation.Attributes;

public sealed class HierarchyEffectPluginTests
{
    // ------------------------------------------------------------
    // Forward
    // ------------------------------------------------------------

    [Fact]
    public static async Task ForwardByLevel()
    {
        // Arrange
        var recorder = new EventRecorder();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseProvider(new MockAsyncFormNavigationProvider(recorder))
            .AddHierarchyEffectPlugin()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigated += (_, args) => { context.Value = args.Context; };

        // Act: initial (no from view)
        await navigator.ForwardAsync(typeof(Level1Form));

        // Assert
        Assert.Null(context.Value.Parameter.Effect);
        Assert.Equal("Open:Level1Form:", Assert.Single(recorder.Events));

        // Act: level 1 -> 2
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(Level2Form));

        // Assert
        Assert.Equal("Forward", context.Value.Parameter.Effect);
        Assert.Equal(2, recorder.Events.Count);
        Assert.Equal("Open:Level2Form:Forward", recorder.Events[0]);
        Assert.Equal("Close:Level1Form:Forward", recorder.Events[1]);

        // Act: level 2 -> 3
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(Level3Form));

        // Assert
        Assert.Equal("Forward", context.Value.Parameter.Effect);
        Assert.Equal("Open:Level3Form:Forward", recorder.Events[0]);
        Assert.Equal("Close:Level2Form:Forward", recorder.Events[1]);

        // Act: level 3 -> 1 (skip level)
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(Level1Form));

        // Assert
        Assert.Equal("Back", context.Value.Parameter.Effect);
        Assert.Equal("Open:Level1Form:Back", recorder.Events[0]);
        Assert.Equal("Close:Level3Form:Back", recorder.Events[1]);

        // Act: level 1 -> 1 (same level)
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(AnotherLevel1Form));

        // Assert
        Assert.Null(context.Value.Parameter.Effect);
        Assert.Equal("Open:AnotherLevel1Form:", recorder.Events[0]);
        Assert.Equal("Close:Level1Form:", recorder.Events[1]);
    }

    [Fact]
    public static async Task ForwardByLevelWithIdViewMapper()
    {
        // Arrange
        var recorder = new EventRecorder();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseProvider(new MockAsyncFormNavigationProvider(recorder))
            .UseIdViewMapper(static r => r.AutoRegister([typeof(MenuForm), typeof(ListForm), typeof(DetailForm)]))
            .AddHierarchyEffectPlugin()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigated += (_, args) => { context.Value = args.Context; };

        // Act
        await navigator.ForwardAsync(ViewId.Menu);

        // Assert
        Assert.Null(context.Value.Parameter.Effect);

        // Act: Menu(1) -> List(2)
        recorder.Events.Clear();
        await navigator.ForwardAsync(ViewId.List);

        // Assert
        Assert.Equal("Forward", context.Value.Parameter.Effect);
        Assert.Equal("Open:ListForm:Forward", recorder.Events[0]);
        Assert.Equal("Close:MenuForm:Forward", recorder.Events[1]);

        // Act: List(2) -> Detail(3)
        recorder.Events.Clear();
        await navigator.ForwardAsync(ViewId.Detail);

        // Assert
        Assert.Equal("Forward", context.Value.Parameter.Effect);
        Assert.Equal("Open:DetailForm:Forward", recorder.Events[0]);
        Assert.Equal("Close:ListForm:Forward", recorder.Events[1]);

        // Act: Detail(3) -> Menu(1)
        recorder.Events.Clear();
        await navigator.ForwardAsync(ViewId.Menu);

        // Assert
        Assert.Equal("Back", context.Value.Parameter.Effect);
        Assert.Equal("Open:MenuForm:Back", recorder.Events[0]);
        Assert.Equal("Close:DetailForm:Back", recorder.Events[1]);
    }

    // ------------------------------------------------------------
    // Push/Pop
    // ------------------------------------------------------------

    [Fact]
    public static async Task PushAndPopByLevel()
    {
        // Arrange
        var recorder = new EventRecorder();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseProvider(new MockAsyncFormNavigationProvider(recorder))
            .AddHierarchyEffectPlugin()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigated += (_, args) => { context.Value = args.Context; };

        await navigator.ForwardAsync(typeof(Level1Form));

        // Act: push level 1 -> 2
        recorder.Events.Clear();
        await navigator.PushAsync(typeof(Level2Form));

        // Assert
        Assert.Equal("Forward", context.Value.Parameter.Effect);
        Assert.Equal("Open:Level2Form:Forward", recorder.Events[0]);
        Assert.Equal("Deactivate:Level1Form:Forward", recorder.Events[1]);

        // Act: push level 2 -> 3
        recorder.Events.Clear();
        await navigator.PushAsync(typeof(Level3Form));

        // Assert
        Assert.Equal("Forward", context.Value.Parameter.Effect);
        Assert.Equal("Open:Level3Form:Forward", recorder.Events[0]);
        Assert.Equal("Deactivate:Level2Form:Forward", recorder.Events[1]);

        // Act: pop level 3 -> 2
        recorder.Events.Clear();
        await navigator.PopAsync();

        // Assert
        Assert.Equal("Back", context.Value.Parameter.Effect);
        Assert.Equal("Activate:Level2Form:Back", recorder.Events[0]);
        Assert.Equal("Close:Level3Form:Back", recorder.Events[1]);

        // Act: pop level 2 -> 1
        recorder.Events.Clear();
        await navigator.PopAsync();

        // Assert
        Assert.Equal("Back", context.Value.Parameter.Effect);
        Assert.Equal("Activate:Level1Form:Back", recorder.Events[0]);
        Assert.Equal("Close:Level2Form:Back", recorder.Events[1]);
    }

    [Fact]
    public static async Task PushToLowerLevelAndPopToHigherLevel()
    {
        // Arrange
        var recorder = new EventRecorder();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseProvider(new MockAsyncFormNavigationProvider(recorder))
            .AddHierarchyEffectPlugin()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigated += (_, args) => { context.Value = args.Context; };

        await navigator.ForwardAsync(typeof(Level2Form));

        // Act: push level 2 -> 1 (level decides, not strategy)
        recorder.Events.Clear();
        await navigator.PushAsync(typeof(Level1Form));

        // Assert
        Assert.Equal("Back", context.Value.Parameter.Effect);
        Assert.Equal("Open:Level1Form:Back", recorder.Events[0]);
        Assert.Equal("Deactivate:Level2Form:Back", recorder.Events[1]);

        // Act: pop level 1 -> 2
        recorder.Events.Clear();
        await navigator.PopAsync();

        // Assert
        Assert.Equal("Forward", context.Value.Parameter.Effect);
        Assert.Equal("Activate:Level2Form:Forward", recorder.Events[0]);
        Assert.Equal("Close:Level1Form:Forward", recorder.Events[1]);
    }

    // ------------------------------------------------------------
    // Options
    // ------------------------------------------------------------

    [Fact]
    public static async Task CustomEffect()
    {
        // Arrange
        var recorder = new EventRecorder();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseProvider(new MockAsyncFormNavigationProvider(recorder))
            .AddHierarchyEffectPlugin(static options =>
            {
                options.ForwardEffect = "Zoom";
                options.BackEffect = "Drop";
            })
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigated += (_, args) => { context.Value = args.Context; };

        await navigator.ForwardAsync(typeof(Level1Form));

        // Act: level 1 -> 2
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(Level2Form));

        // Assert
        Assert.Equal("Zoom", context.Value.Parameter.Effect);
        Assert.Equal("Open:Level2Form:Zoom", recorder.Events[0]);
        Assert.Equal("Close:Level1Form:Zoom", recorder.Events[1]);

        // Act: level 2 -> 1
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(Level1Form));

        // Assert
        Assert.Equal("Drop", context.Value.Parameter.Effect);
        Assert.Equal("Open:Level1Form:Drop", recorder.Events[0]);
        Assert.Equal("Close:Level2Form:Drop", recorder.Events[1]);
    }

    // ------------------------------------------------------------
    // Priority
    // ------------------------------------------------------------

    [Fact]
    public static async Task ExplicitEffectIsPreserved()
    {
        // Arrange
        var recorder = new EventRecorder();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseProvider(new MockAsyncFormNavigationProvider(recorder))
            .AddHierarchyEffectPlugin()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigated += (_, args) => { context.Value = args.Context; };

        await navigator.ForwardAsync(typeof(Level1Form));

        // Act: level 1 -> 2 with explicit effect
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(Level2Form), new NavigationParameter().WithEffect("Fade"));

        // Assert
        Assert.Equal("Fade", context.Value.Parameter.Effect);
        Assert.Equal("Open:Level2Form:Fade", recorder.Events[0]);
        Assert.Equal("Close:Level1Form:Fade", recorder.Events[1]);
    }

    [Fact]
    public static async Task NoLevelIsIgnored()
    {
        // Arrange
        var recorder = new EventRecorder();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseProvider(new MockAsyncFormNavigationProvider(recorder))
            .AddHierarchyEffectPlugin()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigated += (_, args) => { context.Value = args.Context; };

        await navigator.ForwardAsync(typeof(Level1Form));

        // Act: level 1 -> (none)
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(NoLevelForm));

        // Assert
        Assert.Null(context.Value.Parameter.Effect);
        Assert.Equal("Open:NoLevelForm:", recorder.Events[0]);

        // Act: (none) -> (none)
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(AnotherNoLevelForm));

        // Assert
        Assert.Null(context.Value.Parameter.Effect);
        Assert.Equal("Open:AnotherNoLevelForm:", recorder.Events[0]);
        Assert.Equal("Close:NoLevelForm:", recorder.Events[1]);

        // Act: (none) -> level 2
        recorder.Events.Clear();
        await navigator.ForwardAsync(typeof(Level2Form));

        // Assert
        Assert.Null(context.Value.Parameter.Effect);
        Assert.Equal("Open:Level2Form:", recorder.Events[0]);
    }

    // ------------------------------------------------------------
    // Sync
    // ------------------------------------------------------------

    [Fact]
    public static void SyncNavigationDoesNotPlayEffect()
    {
        // Arrange
        var recorder = new EventRecorder();
        var navigator = new NavigatorConfig()
            .UseMockFormProvider()
            .UseProvider(new MockAsyncFormNavigationProvider(recorder))
            .AddHierarchyEffectPlugin()
            .ToNavigator();

        var context = new Holder<INavigationContext>();
        navigator.Navigated += (_, args) => { context.Value = args.Context; };

        // Act
        navigator.Forward(typeof(Level1Form));
        navigator.Forward(typeof(Level2Form));

        // Assert: effect is resolved, but the sync path never reaches the async provider methods
        Assert.Equal("Forward", context.Value.Parameter.Effect);
        Assert.Empty(recorder.Events);
    }

    // ------------------------------------------------------------
    // Mock
    // ------------------------------------------------------------

    public sealed class MockAsyncFormNavigationProvider : IAsyncNavigationProvider
    {
        private readonly MockFormNavigationProvider provider = new();

        private readonly EventRecorder recorder;

        public MockAsyncFormNavigationProvider(EventRecorder recorder)
        {
            this.recorder = recorder;
        }

        public object ResolveTarget(object view) => provider.ResolveTarget(view);

        public void OpenView(object view) => provider.OpenView(view);

        public void CloseView(object view) => provider.CloseView(view);

        public void ActivateView(object view, object? parameter) => provider.ActivateView(view, parameter);

        public object? DeactivateView(object view) => provider.DeactivateView(view);

        public Task OpenViewAsync(object view, INavigationParameter parameter)
        {
            Record("Open", view, parameter);
            OpenView(view);
            return Task.CompletedTask;
        }

        public Task CloseViewAsync(object view, INavigationParameter parameter)
        {
            Record("Close", view, parameter);
            CloseView(view);
            return Task.CompletedTask;
        }

        public Task ActivateViewAsync(object view, object? state, INavigationParameter parameter)
        {
            Record("Activate", view, parameter);
            ActivateView(view, state);
            return Task.CompletedTask;
        }

        public Task<object?> DeactivateViewAsync(object view, INavigationParameter parameter)
        {
            Record("Deactivate", view, parameter);
            return Task.FromResult(DeactivateView(view));
        }

        private void Record(string phase, object view, INavigationParameter parameter)
        {
            recorder.Events.Add($"{phase}:{view.GetType().Name}:{parameter.Effect}");
        }
    }

    public enum ViewId
    {
        Menu,
        List,
        Detail
    }

    [Hierarchy(1)]
    public sealed class Level1Form : MockForm;

    [Hierarchy(1)]
    public sealed class AnotherLevel1Form : MockForm;

    [Hierarchy(2)]
    public sealed class Level2Form : MockForm;

    [Hierarchy(3)]
    public sealed class Level3Form : MockForm;

    public sealed class NoLevelForm : MockForm;

    public sealed class AnotherNoLevelForm : MockForm;

    [View(ViewId.Menu)]
    [Hierarchy(1)]
    public sealed class MenuForm : MockForm;

    [View(ViewId.List)]
    [Hierarchy(2)]
    public sealed class ListForm : MockForm;

    [View(ViewId.Detail)]
    [Hierarchy(3)]
    public sealed class DetailForm : MockForm;
}
