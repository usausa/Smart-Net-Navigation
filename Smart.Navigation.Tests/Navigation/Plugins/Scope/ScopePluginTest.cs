namespace Smart.Navigation.Plugins.Scope
{
    using System;

    using Smart.Mock;

    using Xunit;

    public class ScopePluginTest
    {
        [Fact]
        public static void TestScope()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Data1Form, typeof(Data1Form));
            navigator.Register(ViewId.Data2Form, typeof(Data2Form));
            navigator.Register(ViewId.Data3Form, typeof(Data3Form));

            // test
            navigator.Forward(ViewId.Data1Form);

            navigator.Forward(ViewId.Data2Form);

            var form2 = (Data2Form)navigator.CurrentView;
            Assert.NotNull(form2.Data);
            Assert.True(form2.Data.IsInitialized);
            Assert.False(form2.Data.IsDisposed);

            navigator.Forward(ViewId.Data3Form);

            var form3 = (Data3Form)navigator.CurrentView;
            Assert.Same(form3.Data, form2.Data);
            Assert.True(form3.Data.IsInitialized);
            Assert.False(form3.Data.IsDisposed);

            navigator.Forward(ViewId.Data1Form);

            Assert.True(form3.Data.IsDisposed);
        }

        [Fact]
        public static void TestScopeByRequestType()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Object1Form, typeof(Object1Form));
            navigator.Register(ViewId.Object2Form, typeof(Object2Form));
            navigator.Register(ViewId.Object3Form, typeof(Object3Form));

            // test
            navigator.Forward(ViewId.Object1Form);

            var form1 = (Object1Form)navigator.CurrentView;
            Assert.NotNull(form1.Object);

            navigator.Forward(ViewId.Object2Form);

            var form2 = (Object2Form)navigator.CurrentView;
            Assert.Same(form2.Object, form1.Object);

            navigator.Forward(ViewId.Object3Form);
        }

        [Fact]
        public static void TestScopeSkipInTheMiddle()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Push1Form, typeof(Push1Form));
            navigator.Register(ViewId.Push2Form, typeof(Push2Form));
            navigator.Register(ViewId.Push3Form, typeof(Push3Form));
            navigator.Register(ViewId.Push4Form, typeof(Push4Form));

            // test
            navigator.Forward(ViewId.Push1Form);

            navigator.Push(ViewId.Push2Form);

            var form2 = (Push2Form)navigator.CurrentView;
            Assert.NotNull(form2.Data);
            Assert.True(form2.Data.IsInitialized);
            Assert.False(form2.Data.IsDisposed);

            navigator.Push(ViewId.Push3Form);

            Assert.False(form2.Data.IsDisposed);

            navigator.Push(ViewId.Push4Form);

            var form4 = (Push4Form)navigator.CurrentView;
            Assert.Same(form4.Data, form2.Data);
            Assert.True(form4.Data.IsInitialized);
            Assert.False(form4.Data.IsDisposed);

            navigator.Pop();

            Assert.False(form4.Data.IsDisposed);

            navigator.Pop();

            Assert.False(form4.Data.IsDisposed);

            navigator.Pop();

            Assert.True(form4.Data.IsDisposed);
        }

        [Fact]
        public static void TestScopeNamed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            navigator.Register(ViewId.Named1Form, typeof(Named1Form));
            navigator.Register(ViewId.Named2Form, typeof(Named2Form));

            // test
            navigator.Forward(ViewId.Named1Form);

            var form1 = (Named1Form)navigator.CurrentView;
            Assert.NotNull(form1.ExportData);

            navigator.Forward(ViewId.Named2Form);

            var form2 = (Named2Form)navigator.CurrentView;
            Assert.Same(form2.ImporttData, form1.ExportData);
        }

        public enum ViewId
        {
            Data1Form,
            Data2Form,
            Data3Form,
            Object1Form,
            Object2Form,
            Object3Form,
            Push1Form,
            Push2Form,
            Push3Form,
            Push4Form,
            Named1Form,
            Named2Form
        }

        public class Data1Form : MockForm
        {
        }

        public class Data2Form : MockForm
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public class Data3Form : MockForm
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public class Object1Form : MockForm
        {
            [Scope(typeof(ScopeObject))]
            public IScopeObject Object { get; set; }
        }

        public class Object2Form : MockForm
        {
            [Scope]
            public ScopeObject Object { get; set; }
        }

        public class Object3Form : MockForm
        {
        }

        public class Push1Form : MockForm
        {
        }

        public class Push2Form : MockForm
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public class Push3Form : MockForm
        {
        }

        public class Push4Form : MockForm
        {
            [Scope]
            public ScopeData Data { get; set; }
        }

        public class Named1Form : MockForm
        {
            [Scope("Data")]
            public ScopeData ExportData { get; set; }
        }

        public class Named2Form : MockForm
        {
            [Scope("Data")]
            public ScopeData ImporttData { get; set; }
        }

        public sealed class ScopeData : IInitializable, IDisposable
        {
            public bool IsInitialized { get; private set; }

            public bool IsDisposed { get; private set; }

            public void Initialize()
            {
                IsInitialized = true;
            }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        public interface IScopeObject
        {
            int Value { get; set; }
        }

        public class ScopeObject : IScopeObject
        {
            public int Value { get; set; }
        }
    }
}
