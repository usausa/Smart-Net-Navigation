namespace Smart.Navigation.Mappers;

public interface IViewMapper
{
    ViewDescriptor FindDescriptor(object id);

    void CurrentUpdated(object? id);
}
