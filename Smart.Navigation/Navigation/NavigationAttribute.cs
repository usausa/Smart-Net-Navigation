namespace Smart.Navigation
{
    using System;

    [Flags]
    public enum NavigationAttribute
    {
        None = 0x00000000,
        Stacked = 0x00000001,
        Restore = 0x00000002
    }
}
