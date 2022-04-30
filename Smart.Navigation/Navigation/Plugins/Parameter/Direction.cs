namespace Smart.Navigation.Plugins.Parameter;

[Flags]
public enum Directions
{
    Import = 0x00000001,
    Export = 0x00000002,
    Both = Import | Export
}
