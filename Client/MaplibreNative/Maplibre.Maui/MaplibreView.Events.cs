namespace MaplibreMaui;

partial class MaplibreView
{
    public static readonly BindableProperty HandlerChangedProperty = 
        BindableProperty.Create(nameof(HandlerChanged), typeof(Action), typeof(MaplibreView));

    public Action HandlerChanged
    {
        get => (Action)GetValue(HandlerChangedProperty);
        set => SetValue(HandlerChangedProperty, value);
    }
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        if (Handler != null) HandlerChanged.Invoke();
    }
    
    public void TriggerTouchEvent(object e)
    {
        if (Handler is not MaplibreViewHandler maplibreViewHandler) return;
        maplibreViewHandler.TriggerTouchEvent(e);
    }
}