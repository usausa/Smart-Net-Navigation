namespace Smart.Navigation
{
    using Smart.Functional;
    using Smart.Mock;
    using Smart.Resolver;

    using Xunit;

    public class NavigatorEventTest
    {
        // ------------------------------------------------------------
        // Page
        // ------------------------------------------------------------

        [Fact]
        public static void TestPageNavigatorEvent()
        {
            // prepare
            var recorder = new EventRecorder();
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();
            navigator.Navigating += (sender, args) => recorder.Events.Add($"{args.Context.FromId}.Navigating");
            navigator.Navigated += (sender, args) => recorder.Events.Add($"{args.Context.ToId}.Navigated");

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));

            // test
            navigator.Forward(Pages.Page1);

            Assert.Equal(".Navigating", recorder.Events[0]);
            Assert.Equal("Page1.Navigated", recorder.Events[1]);

            recorder.Events.Clear();

            navigator.Forward(Pages.Page2);

            Assert.Equal("Page1.Navigating", recorder.Events[0]);
            Assert.Equal("Page2.Navigated", recorder.Events[1]);
        }

        [Fact]
        public static void TestPageNavigatorEventSupport()
        {
            // prepare
            var resolver = new ResolverConfig()
                .UseAutoBinding()
                .Also(config => config.Bind<EventRecorder>().ToSelf().InSingletonScope())
                .ToResolver();
            var recorder = resolver.Get<EventRecorder>();
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .UseResolver(resolver)
                .ToNavigator();

            navigator.Register(Pages.EventPage1, typeof(EventPage1));
            navigator.Register(Pages.EventPage2, typeof(EventPage2));

            // test
            navigator.Forward(Pages.EventPage1);

            Assert.Equal("EventPage1.OnNavigatingTo", recorder.Events[0]);
            Assert.Equal("EventPage1.OnNavigatedTo", recorder.Events[1]);

            recorder.Events.Clear();

            navigator.Forward(Pages.EventPage2);

            Assert.Equal("EventPage1.OnNavigatedFrom", recorder.Events[0]);
            Assert.Equal("EventPage2.OnNavigatingTo", recorder.Events[1]);
            Assert.Equal("EventPage2.OnNavigatedTo", recorder.Events[2]);
        }

        public enum Pages
        {
            Page1,
            Page2,
            EventPage1,
            EventPage2
        }

        public class Page1 : MockPage
        {
        }

        public class Page2 : MockPage
        {
        }

        public class EventPage1 : MockPage, INavigationEventSupport
        {
            private readonly EventRecorder recorder;

            public EventPage1(EventRecorder recorder)
            {
                this.recorder = recorder;
            }

            public void OnNavigatedFrom(INavigationContext context)
            {
                recorder.Events.Add("EventPage1.OnNavigatedFrom");
            }

            public void OnNavigatingTo(INavigationContext context)
            {
                recorder.Events.Add("EventPage1.OnNavigatingTo");
            }

            public void OnNavigatedTo(INavigationContext context)
            {
                recorder.Events.Add("EventPage1.OnNavigatedTo");
            }
        }

        public class EventPage2 : MockPage, INavigationEventSupport
        {
            private readonly EventRecorder recorder;

            public EventPage2(EventRecorder recorder)
            {
                this.recorder = recorder;
            }

            public void OnNavigatedFrom(INavigationContext context)
            {
                recorder.Events.Add("EventPage2.OnNavigatedFrom");
            }

            public void OnNavigatingTo(INavigationContext context)
            {
                recorder.Events.Add("EventPage2.OnNavigatingTo");
            }

            public void OnNavigatedTo(INavigationContext context)
            {
                recorder.Events.Add("EventPage2.OnNavigatedTo");
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
                .UseMockViewProvider()
                .UseResolver(resolver)
                .ToNavigator();

            navigator.Register(Views.EventView1, typeof(EventView1));
            navigator.Register(Views.EventView2, typeof(EventView2));

            // test
            navigator.Forward(Views.EventView1);

            Assert.Equal("EventViewModel1.OnNavigatingTo", recorder.Events[0]);
            Assert.Equal("EventViewModel1.OnNavigatedTo", recorder.Events[1]);

            recorder.Events.Clear();

            navigator.Forward(Views.EventView2);

            Assert.Equal("EventViewModel1.OnNavigatedFrom", recorder.Events[0]);
            Assert.Equal("EventViewModel2.OnNavigatingTo", recorder.Events[1]);
            Assert.Equal("EventViewModel2.OnNavigatedTo", recorder.Events[2]);
        }

        public enum Views
        {
            EventView1,
            EventView2
        }

        public class EventViewModel1 : INavigationEventSupport
        {
            private readonly EventRecorder recorder;

            public EventViewModel1(EventRecorder recorder)
            {
                this.recorder = recorder;
            }

            public void OnNavigatedFrom(INavigationContext context)
            {
                recorder.Events.Add("EventViewModel1.OnNavigatedFrom");
            }

            public void OnNavigatingTo(INavigationContext context)
            {
                recorder.Events.Add("EventViewModel1.OnNavigatingTo");
            }

            public void OnNavigatedTo(INavigationContext context)
            {
                recorder.Events.Add("EventViewModel1.OnNavigatedTo");
            }
        }

        public class EventViewModel2 : INavigationEventSupport
        {
            private readonly EventRecorder recorder;

            public EventViewModel2(EventRecorder recorder)
            {
                this.recorder = recorder;
            }

            public void OnNavigatedFrom(INavigationContext context)
            {
                recorder.Events.Add("EventViewModel2.OnNavigatedFrom");
            }

            public void OnNavigatingTo(INavigationContext context)
            {
                recorder.Events.Add("EventViewModel2.OnNavigatingTo");
            }

            public void OnNavigatedTo(INavigationContext context)
            {
                recorder.Events.Add("EventViewModel2.OnNavigatedTo");
            }
        }

        public class EventView1 : MockView
        {
            public EventView1(EventViewModel1 vm)
            {
                Context = vm;
            }
        }

        public class EventView2 : MockView
        {
            public EventView2(EventViewModel2 vm)
            {
                Context = vm;
            }
        }
    }
}
