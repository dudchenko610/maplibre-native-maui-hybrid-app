using Microsoft.Maui.Platform;
using PlatformView = MapboxMaui.MapViewContainer;

namespace MaplibreMaui;

public partial class MaplibreViewHandler
{
#if IOS
    protected override PlatformView CreatePlatformView()
    {
        var maplibreView = VirtualView as MaplibreView;
        
        var styleUrl = maplibreView.StyleUrl;
        var lat = maplibreView.StartLatitude;
        var lng = maplibreView.StartLongitude;
        var zoom = maplibreView.StartZoom;
        
        return new PlatformView((iOSMaplibreMapService)MapService, CallbackService, 
            styleUrl, lat, lng, zoom);
    }

    protected override void ConnectHandler(PlatformView platformView)
    {
        base.ConnectHandler(platformView);
    }

    protected override void DisconnectHandler(PlatformView platformView)
    {
        base.DisconnectHandler(platformView);
    }
#endif
}

