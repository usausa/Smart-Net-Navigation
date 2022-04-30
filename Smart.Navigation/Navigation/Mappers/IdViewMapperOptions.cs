namespace Smart.Navigation.Mappers;

public class IdViewMapperOptions
{
    public Action<IIdViewRegister> SetupAction { get; }

    public IdViewMapperOptions(Action<IIdViewRegister> setupAction)
    {
        SetupAction = setupAction;
    }
}
