namespace Smart.Navigation;

public static class NavigationParameterEffectExtensions
{
    public static NavigationParameter WithEffect(this NavigationParameter parameter, string kind)
    {
        parameter.EffectKind = kind;
        return parameter;
    }

    public static NavigationParameter ClearEffect(this NavigationParameter parameter)
    {
        parameter.EffectKind = null;
        return parameter;
    }
}
