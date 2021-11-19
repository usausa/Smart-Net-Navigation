namespace Smart.Navigation.Components;

public sealed class PageContextKeyManager
{
    private int next;

    public int Acquire()
    {
        return next++;
    }
}
