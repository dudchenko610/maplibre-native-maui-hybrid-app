using MaplibreMaui.Services;
using Microsoft.Maui.Controls;

namespace MaplibreMaui;

public partial class MaplibreView : View
{
    public IMaplibreMapService MapService => Handler is MaplibreViewHandler maplibreViewHandler
        ? maplibreViewHandler.MapService
        : null!;

    public MaplibreCallbackService CallbackService => Handler is MaplibreViewHandler maplibreViewHandler
        ? maplibreViewHandler.CallbackService
        : null!;
    
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        if (Handler != null) HandlerChanged.Invoke();
    }
}