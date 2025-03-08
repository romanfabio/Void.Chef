namespace Void.Chef.BlazorApp.Utils;

public class LoadingManager
{
    private int _count = 0;

    public bool IsLoading
    {
        get { return _count > 0; }
        set
        {
            _count = value ? _count + 1 : (_count > 0 ? _count - 1 : _count);
        }
    }
}
