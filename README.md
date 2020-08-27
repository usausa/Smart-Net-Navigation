# Smart.Navigation .NET - navigation library for .NET

[![NuGet Badge](https://buildstats.info/nuget/Usa.Smart.Navigation)](https://www.nuget.org/packages/Usa.Smart.Navigation/)

## What is this ?

* Navigation by control switching.
* Navigationg to view by id.
* Multiplatform support.
* MVVM support with WPF and Xamarin.Forms provider.
* Parameter support between target.
* Stacked navigation support.
* Lifecycle event support.
* Cancel event support.
* Plugin support.
* Library integration support.

### Usage example

```csharp
// Config Navigator
navigator = new NavigatorConfig()
    .UseFormsNavigationProvider()
    .UseResolver(resolver)
    .UseIdViewMapper(m => m.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes))
    .ToNavigator();

// Navigate to view
navigator.Forward(ViewId.Menu);
```

## NuGet

| Package | Note |
|-|-|
| [![NuGet Badge](https://buildstats.info/nuget/Usa.Smart.Navigation)](https://www.nuget.org/packages/Usa.Smart.Navigation/) | Core libyrary |
| [![NuGet Badge](https://buildstats.info/nuget/Usa.Smart.Navigation.Resolver)](https://www.nuget.org/packages/Usa.Smart.Navigation.Resolver/) | Smart.Resolver integration |
| [![NuGet Badge](https://buildstats.info/nuget/Usa.Smart.Navigation.Forms)](https://www.nuget.org/packages/Usa.Smart.Navigation.Forms/) | Xamarin.Forms provider |
| [![NuGet Badge](https://buildstats.info/nuget/Usa.Smart.Navigation.Windows)](https://www.nuget.org/packages/Usa.Smart.Navigation.Windows/) | WPF provider |
| [![NuGet Badge](https://buildstats.info/nuget/Usa.Smart.Navigation.Windows.Forms)](https://www.nuget.org/packages/Usa.Smart.Navigation.Windows.Forms/) | Windows Forms provider |

## Supported platform

### Windows Forms

``Control`` is a container and ``Control`` is a view.

```csharp
navigator = new NavigatorConfig()
    .UseControlNavigationProvider(ContainerPanel)
    .ToNavigator();
```

### WPF

``ContentControl`` is a container and ``Control`` is a view.

```csharp
navigator = new NavigatorConfig()
    .UseWindowsNavigationProvider()
    .ToNavigator();
```

```xml
<ContentControl>
    <i:Interaction.Behaviors>
        <navigation:NavigationContainerBehavior Navigator="{Binding Navigator}"/>
    </i:Interaction.Behaviors>
</ContentControl>
```

### Xamarin.Forms

``ContentView`` is a container and ``View`` is a view.

```csharp
navigator = new NavigatorConfig()
    .UseFormsNavigationProvider()
    .ToNavigator();
```

```xml
<ContentView>
    <ContentView.Behaviors>
        <navigation:NavigationContainerBehavior Navigator="{Binding Navigator}" />
    </ContentView.Behaviors>
</ContentView>
```

### Create custom platform support

Implement the following interface.

```csharp
public interface INavigationProvider
{
    // Resolve target(DataContext/BindingContext) from view
    object ResolveTarget(object view);

    // Add view to container
    void OpenView(object view);

    // Remove view from container and dispose
    void CloseView(object view);

    // Restore view from stack
    void ActiveView(object view, object parameter);

    // Deactive view
    object DeactiveView(object view);
}
```

## View mapper

### Id to Type dictionary mapping

```csharp
// Configuration method
NavigatorConfig UseIdViewMapper(Action<IIdViewRegister> action);

// Register interface
public interface IIdViewRegister
{
    void Register(object id, Type type);
}

// Auto register extension
void AutoRegister(IEnumerable<Type> types);
```

```csharp
// Usage
public enum ViewId
{
    ViewList,
    ViewDetailNew,
    ViewDetailUpdate
}

[View(ViewId.ViewList)]
public class ViewList
{
}

[View(ViewId.ViewDetailNew)]
[View(ViewId.ViewDetailUpdate)]
public class ViewDetail
{
}

// config
var navigator = new NavigatorConfig()
    .UseSomeProvider()
    .UseIdViewMapper(m => m.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes))
    .ToNavigator();

// navigation
navigator.Forward(ViewId.ViewList);

```

### Direct type mapping (default)

```csharp
// Configuration method
NavigatorConfig UseDirectViewMapper();
```

```csharp
// config
var navigator = new NavigatorConfig()
    .UseSomeProvider()
    .UseDirectViewMapper()
    .ToNavigator();

// navigation
navigator.Forward(typeof(View1));

```

### Path mapping

```csharp
// Configuration method
NavigatorConfig UsePathViewMapper(Action<PathViewMapperOptions> action)
```

```csharp
// Usage
namespace Example.Views
{
    public class ParentView
    {
    }
}

namespace Example.Views.Children
{
    public class Child1View
    {
    }

    public class Child2View
    {
    }
}

// config
var navigator = new NavigatorConfig()
    .UseMockFormProvider()
    .UsePathViewMapper(option =>
    {
        option.Root = "Example.Views";
        option.Suffix = "View";
        option.AddAssembly(Assembly.GetExecutingAssembly());
    })
    .ToNavigator();

// navigation
navigator.Forward("/Parent");
navigator.Forward("/Children/Child1");
navigator.Forward("Child2");
navigator.Forward("../Parent");
navigator.Forward("Children/Child2");
```

## Navigation

### Forward

Non stacked navigation.

```csharp
bool Forward(object id);

bool Forward(object id, INavigationParameter parameter);

Task<bool> ForwardAsync(object id);

Task<bool> ForwardAsync(object id, INavigationParameter parameter);
```

### Push

Stacked navigation.

```csharp
bool Push(object id);

bool Push(object id, INavigationParameter parameter);

Task<bool> PushAsync(object id);

Task<bool> PushAsync(object id, INavigationParameter parameter);
```

### Pop

Pop stack navigation.

```csharp
bool Pop();

bool Pop(INavigationParameter parameter);

bool Pop(int level);

bool Pop(int level, INavigationParameter parameter);

Task<bool> PopAsync();

Task<bool> PopAsync(INavigationParameter parameter);

Task<bool> PopAsync(int level);

Task<bool> PopAsync(int level, INavigationParameter parameter);
```

### PopAndForward

Pop with Forward navigation.

```csharp
bool PopAndForward(object id);

bool PopAndForward(object id, int level);

bool PopAllAndForward(object id);

bool PopAndForward(object id, INavigationParameter parameter);

bool PopAndForward(object id, int level, INavigationParameter parameter);

bool PopAllAndForward(object id, INavigationParameter parameter);

Task<bool> PopAndForwardAsync(object id);

Task<bool> PopAndForwardAsync(object id, int level);

Task<bool> PopAllAndForwardAsync(object id);

Task<bool> PopAndForwardAsync(object id, INavigationParameter parameter);

Task<bool> PopAndForwardAsync(object id, int level, INavigationParameter parameter);

Task<bool> PopAllAndForwardAsync(object id, INavigationParameter parameter);
```

### Create custom navigation

Implement the following interface to create custom navigation.

```csharp
public interface INavigationStrategy
{
    // Initialize
    StragtegyResult Initialize(INavigationController controller);

    // Resolve next view
    object ResolveToView(INavigationController controller);

    // Stack update
    void UpdateStack(INavigationController controller, object toView);
}
```

## Navigator property

```csharp
public interface INavigator
{
    // Stack count
    int StackedCount { get; }

    // Current view id
    object CurrentViewId { get; }

    // Current view instance
    object CurrentView { get; }

    // Current target(DataContext/BindingContext or view itself) instance
    object CurrentTarget { get; }

    // Navigating status
    bool Executing { get; }
}
```

## Event

### INavigationEventSupport

```csharp
public interface INavigationEventSupport
{
    // From page event berfore stack changed
    void OnNavigatingFrom(INavigationContext context);

    // To page event berfore stack changed
    void OnNavigatingTo(INavigationContext context);

    // To page event after stack changed
    void OnNavigatedTo(INavigationContext context);
}
```

``INavigationContext`` has navigation information.

```csharp
public interface INavigationContext
{
    object FromId { get; }

    object ToId { get; }

    NavigationAttributes Attribute { get; }

    INavigationParameter Parameter { get; }
}
```

``INavigationParameter`` is navigation parameteter store.

```csharp
public interface INavigationParameter
{
    T GetValue<T>(string key);

    T GetValue<T>();

    T GetValueOrDefault<T>(string key);

    T GetValueOrDefault<T>();

    T GetValueOr<T>(string key, T defaultValue);

    T GetValueOr<T>(T defaultValue);
}
```

``NavigationAttributes`` has the following extension methods.

```csharp
public static class NavigationAttributesExtensions
{
    public static bool IsStacked(this NavigationAttributes attributes);

    public static bool IsRestore(this NavigationAttributes attributes);
}
```

### INavigator events

```csharp
public interface INavigator
{
    // Cancel confirm event
    event EventHandler<ConfirmEventArgs> Confirm;

    // Navigating event before stack changed
    event EventHandler<NavigationEventArgs> Navigating;

    // Navigating event after stack changed
    event EventHandler<NavigationEventArgs> Navigated;

    // Navigator exit event
    event EventHandler<EventArgs> Exited;

    // Navigation executing changed event
    event EventHandler<EventArgs> ExecutingChanged;
}
```

``ConfirmEventArgs`` is ``CancelEventArgs`` with ``INavigationContext``.

```csharp
public sealed class ConfirmEventArgs : CancelEventArgs
{
    public INavigationContext Context { get; }
}
```

``NavigationEventArgs`` has ``INavigationContext`` and view instance information.

```csharp
public sealed class NavigationEventArgs : EventArgs
{
    public INavigationContext Context { get; }

    public object FromView { get; }

    public object FromTarget { get; }

    public object ToView { get; }

    public object ToTarget { get; }
}
```

## Supported interfaces

### IConfirmRequest/IConfirmRequestAsync

Cancel when false is returned.

```csharp
public interface IConfirmRequest
{
    bool CanNavigate(INavigationContext context);
}
```

```csharp
public interface IConfirmRequestAsync
{
    Task<bool> CanNavigateAsync(INavigationContext context);
}
```

### INotifySupport/INotifySupportAsync

Navigator external notification reception interface.

```csharp
public interface INotifySupport
{
    void NavigatorNotify(object parameter);
}

public interface INotifySupport<in T>
{
    void NavigatorNotify(T parameter);
}

public interface INotifySupportAsync
{
    Task NavigatorNotifyAsync(object parameter);
}

public interface INotifySupportAsync<in T>
{
    Task NavigatorNotifyAsync(T parameter);
}
```

Notification method is as follows.

```csharp
// Usage
navigator.Notyfy(parameter);

// Usage(Async)
await navigator.NotyfyAsync(parameter);
```

### INavigatorAware

Navigator is injected.

```csharp
public interface INavigatorAware
{
    INavigator Navigator { get; set; }
}
```

## Plugins

### Parameter plugin

Previous parameter is set next.

```csharp
public class View1
{
    [Parameter]
    public int IntParameter { get; set; }
}

public class View2
{
    [Parameter]
    public int IntParameter { get; set; }
}

// Test
navigator.Forward(typeof(View1));

var view1 = (View1)navigator.CurrentView;
view1.IntParameter = 123;

navigator.Forward(typeof(View2));

var view2 = (View2)navigator.CurrentView;
Assert.Equal(123, view2.IntParameter);
```

* Set to property with same name.
* Even if the name of the property is different, it can be specified by attribute.
* I/O direction can be limited by ``Directions.Import``/``Directions.Export``.
* When different types are converted by IConverter.

### Scope plugin

Inject object that exist between scopes.

```csharp
public sealed class ScopeData : IInitializable, IDisposable
{
...
}

public class Data1View
{
}

public class Data2View
{
    [Scope]
    public ScopeData Data { get; set; }
}

public class Data3View
{
    [Scope]
    public ScopeData Data { get; set; }
}

// Test
navigator.Forward(typeof(Data1View));

navigator.Forward(typeof(Data2View)); // ScopeDate create and initialized
var view2 = (Data2View)navigator.CurrentView;

navigator.Forward(typeof(Data3View));
var view3 = (Data3View)navigator.CurrentView;

Assert.Equal(view3.Data, view2.Data);

navigator.Forward(typeof(Data1View)); // ScopeData disposed

```

* Set to property with same name.
* Even if the name of the property is different, it can be specified by attribute.
* Supports ``IInitializable`` and ``IDisposable`` lifecycle events.

### Create custom plugin

Implement the following interface and register to NavigatorConfig.

```csharp
public interface IPlugin
{
    // Process when view created
    void OnCreate(IPluginContext context, object view, object target);

    // Process when view closed
    void OnClose(IPluginContext context, object view, object target);

    // Process before stack is changed
    void OnNavigatingFrom(IPluginContext context, object view, object target);

    // Process before stack is changed
    void OnNavigatingTo(IPluginContext context, object view, object target);

    // Process after stack is changed
    void OnNavigatedTo(IPluginContext context, object view, object target);
}
```

IPluginContext is data store for plugin.

```csharp
public interface IPluginContext
{
    void Save<T>(Type type, T value);

    T Load<T>(Type type);

    T LoadOr<T>(Type type, T defaultValue);

    T LoadOr<T>(Type type, Func<T> defaultValueFactory);
}
```

## Library integration

### IActivator

* Interface is used for object creation.
* Default implementation is ``Activator.CreateInstance()``.
* Customizable by creating the following implementation.

```csharp
public interface IActivator
{
    object Resolve(Type type);
}
```

``Usa.Smart.Navigation.Resolver`` provides an implementation of ``IActivator`` using ``Usa.Smart.Resolver``.

```csharp
// Usage

// Config Resolver
var resolver = CreateResolver();

// Config Navigator
navigator = new NavigatorConfig()
    .UseSomeProvider()
    .UseResolver(resolver)
    .ToNavigator();
```

### IConverter

* Interface is used for type conversion.
* Default implementation uses ``Smart.Converter.IObjectConverter``.
* Customizable by creating the following implementation.

```csharp
public interface IConverter
{
    object Convert(object value, Type type);
}
```

## Link

* [Smart.Resolver](https://github.com/usausa/Smart-Net-Resolver)

## Future

* Animation support required (・ω・)?
