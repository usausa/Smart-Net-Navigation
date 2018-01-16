namespace Smart.Navigation
{
    using Smart.Functional;
    using Smart.Mock;
    using Smart.Resolver;

    using Xunit;

    public class NavigatorEventTest
    {
        [Fact]
        public static void TestNavigatorEvent()
        {
            // prepare
            var recorder = new EventRecorder();
            var navigator = new NavigatorConfig()
                .UseMockPageProvider()
                .ToNavigator();
            navigator.NavigatedFrom += (sender, args) => recorder.Events.Add($"{args.Context.FromId}.NavigatedFrom");
            navigator.NavigatingTo += (sender, args) => recorder.Events.Add($"{args.Context.ToId}.NavigatingTo");
            navigator.NavigatedTo += (sender, args) => recorder.Events.Add($"{args.Context.ToId}.NavigatedTo");

            navigator.Register(Pages.Page1, typeof(Page1));
            navigator.Register(Pages.Page2, typeof(Page2));

            // test
            navigator.Forward(Pages.Page1);

            Assert.Equal("Page1.NavigatingTo", recorder.Events[0]);
            Assert.Equal("Page1.NavigatedTo", recorder.Events[1]);

            recorder.Events.Clear();

            navigator.Forward(Pages.Page2);

            Assert.Equal("Page1.NavigatedFrom", recorder.Events[0]);
            Assert.Equal("Page2.NavigatingTo", recorder.Events[1]);
            Assert.Equal("Page2.NavigatedTo", recorder.Events[2]);
        }

        [Fact]
        public static void TestNavigatorEventSupport()
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
    }
}
