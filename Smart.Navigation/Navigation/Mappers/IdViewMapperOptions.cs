namespace Smart.Navigation.Mappers
{
    using System;

    public class IdViewMapperOptions
    {
        public Action<IIdViewRegister> SetupAction { get; }

        public IdViewMapperOptions(Action<IIdViewRegister> setupAction)
        {
            SetupAction = setupAction;
        }
    }
}
