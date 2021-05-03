namespace Smart.Navigation.Plugins.Parameter
{
    using Smart.Mock;

    using Xunit;

    public class ParameterPluginTest
    {
        [Fact]
        public static void Parameter()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Form1));

            var form1 = (Form1)navigator.CurrentView!;
            form1.IntParameter = 123;
            form1.StringParameter = "abc";

            navigator.Forward(typeof(Form2));

            Assert.Equal(1, navigator.StackedCount);
            var form2 = (Form2)navigator.CurrentView!;
            Assert.Equal(123, form2.IntParameter);
            Assert.Equal("abc", form2.StringParameter);
        }

        [Fact]
        public static void ParameterOneWay()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Form1));

            var form1 = (Form1)navigator.CurrentView!;
            form1.IntParameter = 123;
            form1.StringParameter = "abc";

            navigator.Push(typeof(OneWayForm));

            var oneWayForm = (OneWayForm)navigator.CurrentView!;
            Assert.Equal(123, oneWayForm.IntParameter);
            Assert.Null(oneWayForm.StringParameter);

            oneWayForm.IntParameter = 987;
            oneWayForm.StringParameter = "xyz";

            navigator.Pop();
            Assert.Equal(123, form1.IntParameter);
            Assert.Equal("xyz", form1.StringParameter);
        }

        [Fact]
        public static void ParameterNamed()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Named1Form));

            var namedForm1 = (Named1Form)navigator.CurrentView!;
            namedForm1.Value1 = 123;
            namedForm1.Parameter2 = "abc";

            navigator.Forward(typeof(Named2Form));

            var namedForm2 = (Named2Form)navigator.CurrentView!;
            Assert.Equal(123, namedForm2.Parameter1);
            Assert.Equal("abc", namedForm2.Value2);
        }

        [Fact]
        public static void ParameterConvert()
        {
            // prepare
            var navigator = new NavigatorConfig()
                .UseMockFormProvider()
                .ToNavigator();

            // test
            navigator.Forward(typeof(Convert1Form));

            var convertForm1 = (Convert1Form)navigator.CurrentView!;
            convertForm1.Value1 = 123;
            convertForm1.Value2 = "456";

            navigator.Forward(typeof(Convert2Form));

            var convertForm2 = (Convert2Form)navigator.CurrentView!;
            Assert.Equal("123", convertForm2.Value1);
            Assert.Equal(456, convertForm2.Value2);
        }

        public class Form1 : MockForm
        {
            [Parameter]
            public int IntParameter { get; set; }

            [Parameter]
            public string? StringParameter { get; set; }
        }

        public class Form2 : MockForm
        {
            [Parameter]
            public int IntParameter { get; set; }

            [Parameter]
            public string? StringParameter { get; set; }
        }

        public class OneWayForm : MockForm
        {
            [Parameter(Directions.Import)]
            public int IntParameter { get; set; }

            [Parameter(Directions.Export)]
            public string? StringParameter { get; set; }
        }

        public class Named1Form : MockForm
        {
            [Parameter("Parameter1")]
            public int Value1 { get; set; }

            [Parameter]
            public string? Parameter2 { get; set; }
        }

        public class Named2Form : MockForm
        {
            [Parameter]
            public int Parameter1 { get; set; }

            [Parameter("Parameter2")]
            public string? Value2 { get; set; }
        }

        public class Convert1Form : MockForm
        {
            [Parameter]
            public int Value1 { get; set; }

            [Parameter]
            public string? Value2 { get; set; }
        }

        public class Convert2Form : MockForm
        {
            [Parameter]
            public string? Value1 { get; set; }

            [Parameter]
            public int Value2 { get; set; }
        }
    }
}
