namespace Smart.Navigation.Mappers
{
    public interface IViewMapper
    {
        ViewDescriptor FindDescriptor(object id);

        void Updated(object id);
    }
}
