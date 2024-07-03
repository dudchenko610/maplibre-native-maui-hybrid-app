using MaplibreMaui.Services;

namespace MaplibreMaui;

public partial class MaplibreView : View
{
    public IMaplibreMapService MapService => Handler is MaplibreViewHandler maplibreViewHandler
        ? maplibreViewHandler.MapService
        : null!;

    public MaplibreCallbackService CallbackService => Handler is MaplibreViewHandler maplibreViewHandler
        ? maplibreViewHandler.CallbackService
        : new MaplibreCallbackService();
}