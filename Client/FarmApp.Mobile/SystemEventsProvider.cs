namespace FarmApp.Mobile;

public static class SystemEventsProvider
{
    public static event Action? OnLastTouchLifted;
    public static event Func<object, bool>? OnTouchEvent;

    public static void InvokeLastTouchLifted()
    {
        OnLastTouchLifted?.Invoke();
    }
    
    public static bool InvokeTouch(object e)
    {
        return OnTouchEvent?.Invoke(e) ?? true;
    }
}