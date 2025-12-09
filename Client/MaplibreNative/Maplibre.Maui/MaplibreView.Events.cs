namespace MaplibreMaui;

public partial class MaplibreView
{
    public void TriggerTouchEvent(object e)
    {
        if (Handler is not MaplibreViewHandler maplibreViewHandler) return;
        
#if ANDROID
        maplibreViewHandler.TriggerTouchEvent(e);
#elif IOS
        
#else
        
#endif
    }
}