namespace Example.WindowsFormsApp.Modules;

using System;
using System.Collections.Generic;

using Smart.Navigation.Attributes;

public static partial class ViewRegistry
{
    [ViewSource]
    public static partial IEnumerable<KeyValuePair<ViewId, Type>> ListViews();
}
