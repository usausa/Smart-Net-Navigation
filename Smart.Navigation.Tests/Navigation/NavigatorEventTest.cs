namespace Smart.Navigation
{
    using System;
    using Smart.Functional;
    using Smart.Mock;
    using Smart.Resolver;

    using Xunit;

    public class NavigatorEventTest
    {
        // ------------------------------------------------------------
        // View
        // ------------------------------------------------------------

        [Fact]
        public static void TestFormNavigatorEvent()
        {
            // prepare
            var recorder = new EventRecorder();
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();
            navigator.Navigating += (sender, args) => recorder.Events.Add($"{((Type)args.Context.FromId)?.Name}.Navigating");
            navigator.Navigated += (sender, args) => recorder.Events.Add($"{((Type)args.Context.ToId).Name}.Navigated");

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
        public static void TestFormNavigatorEventSupport()
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

            Assert.Equal("Event1Form.OnNavigatedFrom", recorder.Events[0]);
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

            public void OnNavigatedFrom(INavigationContext context)
            {
                recorder.Events.Add("Event1Form.OnNavigatedFrom");
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

            public void OnNavigatedFrom(INavigationContext context)
            {
                recorder.Events.Add("Event2Form.OnNavigatedFrom");
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
        public static void TestViewNavigatorEventSupport()
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

            Assert.Equal("Event1WindowViewModel.OnNavigatedFrom", recorder.Events[0]);
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

            public void OnNavigatedFrom(INavigationContext context)
            {
                recorder.Events.Add("Event1WindowViewModel.OnNavigatedFrom");
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

            public void OnNavigatedFrom(INavigationContext context)
            {
                recorder.Events.Add("Event2WindowViewModel.OnNavigatedFrom");
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
