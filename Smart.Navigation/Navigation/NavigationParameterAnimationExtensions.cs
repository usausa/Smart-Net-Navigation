namespace Smart.Navigation;

public static class NavigationParameterAnimationExtensions
{
    public static NavigationParameter WithAnimation(this NavigationParameter parameter, string kind)
    {
        parameter.AnimationKind = kind;
        return parameter;
    }

    public static NavigationParameter ClearAnimation(this NavigationParameter parameter)
    {
        parameter.AnimationKind = null;
        return parameter;
    }
}
