namespace Smart.Navigation
{
    using System;
    using Smart.Mock;
    using Smart.Resolver;

    using Xunit;

    public class NavigatorEventTest
    {
        // ------------------------------------------------------------
        // EventArgs
        // ------------------------------------------------------------

        [Fact]
        public static void FormEventArgs()
        {
            // prepare
            var resolver = new ResolverConfig()
                .UseAutoBinding()
                .ToResolver();
            var navigator = new NavigatorConfig()
                .UseMockWindowProvider()
                .UseResolver(resolver)
                .ToNavigator();

            var eventArgs = new Holder<NavigationEventArgs>();
            navigator.Navigating += (_, args) => eventArgs.Value = args;

            // test
            navigator.Forward(typeof(EventArgs1Window));

            Assert.NotNull(eventArgs.Value.Context);
            Assert.Null(eventArgs.Value.FromView);
            Assert.Null(eventArgs.Value.FromTarget);
            Assert.NotNull(eventArgs.Value.ToView);
            Assert.Equal(typeof(EventArgs1Window), eventArgs.Value.ToView.GetType());
            Assert.NotNull(eventArgs.Value.ToTarget);
            Assert.Equal(typeof(EventArgs1WindowViewModel), eventArgs.Value.ToTarget.GetType());

            navigator.Forward(typeof(EventArgs2Window));

            Assert.NotNull(eventArgs.Value.Context);
            Assert.NotNull(eventArgs.Value.FromView);
            Assert.Equal(typeof(EventArgs1Window), eventArgs.Value.FromView!.GetType());
            Assert.NotNull(eventArgs.Value.FromTarget);
            Assert.Equal(typeof(EventArgs1WindowViewModel), eventArgs.Value.FromTarget!.GetType());
            Assert.NotNull(eventArgs.Value.ToView);
            Assert.Equal(typeof(EventArgs2Window), eventArgs.Value.ToView.GetType());
            Assert.NotNull(eventArgs.Value.ToTarget);
            Assert.Equal(typeof(EventArgs2WindowViewModel), eventArgs.Value.ToTarget.GetType());
        }

        public class EventArgs1WindowViewModel
        {
        }

        public class EventArgs2WindowViewModel
        {
        }

        public class EventArgs1Window : MockWindow
        {
            public EventArgs1Window(EventArgs1WindowViewModel vm)
            {
                Context = vm;
            }
        }

        public class EventArgs2Window : MockWindow
        {
            public EventArgs2Window(EventArgs2WindowViewModel vm)
            {
                Context = vm;
            }
        }

        // ------------------------------------------------------------
        // View
        // ------------------------------------------------------------

        [Fact]
        public static void FormEvent()
        {
            // prepare
            var recorder = new EventRecorder();
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();
            navigator.Navigating += (_, args) => recorder.Events.Add($"{((Type?)args.Context.FromId)?.Name}.Navigating");
            navigator.Navigated += (_, args) => recorder.Events.Add($"{((Type)args.Context.ToId).Name}.Navigated");

            // test
            navigator.Forward(typeof(Form1));

            Assert.Equal(".Navigating", recorder.Events[0]);
            Assert.Equal("Form1.Navigated", recorder.Events[1]);

            recorder.Events.Clear();

            navigator.Forward(typeof(Form2));

            Assert.Equal("Form1.Navigating", recorder.Events[0]);
            Assert.Equal("Form2.Navigated", recorder.Events[1]);
        }

        [Fact]
        public static void FormEventSupport()
        {
            // prepare
            var resolver = new ResolverConfig()
                .UseAutoBinding()
                .Also(config => config.Bind<EventRecorder>().ToSelf().InSingletonScope())
                .ToResolver();
            var recorder = resolver.Get<EventRecorder>();
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .UseResolver(resolver)
                .ToNavigator();

            // test
            navigator.Forward(typeof(Event1Form));

            Assert.Equal("Event1Form.OnNavigatingTo", recorder.Events[0]);
            Assert.Equal("Event1Form.OnNavigatedTo", recorder.Events[1]);

            recorder.Events.Clear();

            navigator.Forward(typeof(Event2Form));

            Assert.Equal("Event1Form.OnNavigatingFrom", recorder.Events[0]);
            Assert.Equal("Event2Form.OnNavigatingTo", recorder.Events[1]);
            Assert.Equal("Event2Form.OnNavigatedTo", recorder.Events[2]);
        }

        public class Form1 : MockForm
        {
        }

        public class Form2 : MockForm
        {
        }

        public class Event1Form : MockForm, INavigationEventSupport
        {
            private readonly EventRecorder recorder;

            public Event1Form(EventRecorder recorder)
            {
                this.recorder = recorder;
            }

            public void OnNavigatingFrom(INavigationContext context)
            {
                recorder.Events.Add("Event1Form.OnNavigatingFrom");
            }

            public void OnNavigatingTo(INavigationContext context)
            {
                recorder.Events.Add("Event1Form.OnNavigatingTo");
            }

            public void OnNavigatedTo(INavigationContext context)
            {
                recorder.Events.Add("Event1Form.OnNavigatedTo");
            }
        }

        public class Event2Form : MockForm, INavigationEventSupport
        {
            private readonly EventRecorder recorder;

            public Event2Form(EventRecorder recorder)
            {
                this.recorder = recorder;
            }

            public void OnNavigatingFrom(INavigationContext context)
            {
                recorder.Events.Add("Event2Form.OnNavigatingFrom");
            }

            public void OnNavigatingTo(INavigationContext context)
            {
                recorder.Events.Add("Event2Form.OnNavigatingTo");
            }

            public void OnNavigatedTo(INavigationContext context)
            {
                recorder.Events.Add("Event2Form.OnNavigatedTo");
            }
        }

        // ------------------------------------------------------------
        // View
        // ------------------------------------------------------------

        [Fact]
        public static void ViewEventSupport()
        {
            // prepare
            var resolver = new ResolverConfig()
                .UseAutoBinding()
                .Also(config => config.Bind<EventRecorder>().ToSelf().InSingletonScope())
                .ToResolver();
            var recorder = resolver.Get<EventRecorder>();
            var navigator = new NavigatorConfig()
                .UseMockWindowProvider()
                .UseResolver(resolver)
                .ToNavigator();

            // test
            navigator.Forward(typeof(Event1Window));

            Assert.Equal("Event1WindowViewModel.OnNavigatingTo", recorder.Events[0]);
            Assert.Equal("Event1WindowViewModel.OnNavigatedTo", recorder.Events[1]);

            recorder.Events.Clear();

            navigator.Forward(typeof(Event2Window));

            Assert.Equal("Event1WindowViewModel.OnNavigatingFrom", recorder.Events[0]);
            Assert.Equal("Event2WindowViewModel.OnNavigatingTo", recorder.Events[1]);
            Assert.Equal("Event2WindowViewModel.OnNavigatedTo", recorder.Events[2]);
        }

        public class Event1WindowViewModel : INavigationEventSupport
        {
            private readonly EventRecorder recorder;

            public Event1WindowViewModel(EventRecorder recorder)
            {
                this.recorder = recorder;
            }

            public void OnNavigatingFrom(INavigationContext context)
            {
                recorder.Events.Add("Event1WindowViewModel.OnNavigatingFrom");
            }

            public void OnNavigatingTo(INavigationContext context)
            {
                recorder.Events.Add("Event1WindowViewModel.OnNavigatingTo");
            }

            public void OnNavigatedTo(INavigationContext context)
            {
                recorder.Events.Add("Event1WindowViewModel.OnNavigatedTo");
            }
        }

        public class Event2WindowViewModel : INavigationEventSupport
        {
            private readonly EventRecorder recorder;

            public Event2WindowViewModel(EventRecorder recorder)
            {
                this.recorder = recorder;
            }

            public void OnNavigatingFrom(INavigationContext context)
            {
                recorder.Events.Add("Event2WindowViewModel.OnNavigatingFrom");
            }

            public void OnNavigatingTo(INavigationContext context)
            {
                recorder.Events.Add("Event2WindowViewModel.OnNavigatingTo");
            }

            public void OnNavigatedTo(INavigationContext context)
            {
                recorder.Events.Add("Event2WindowViewModel.OnNavigatedTo");
            }
        }

        public class Event1Window : MockWindow
        {
            public Event1Window(Event1WindowViewModel vm)
            {
                Context = vm;
            }
        }

        public class Event2Window : MockWindow
        {
            public Event2Window(Event2WindowViewModel vm)
            {
                Context = vm;
            }
        }
    }
}
