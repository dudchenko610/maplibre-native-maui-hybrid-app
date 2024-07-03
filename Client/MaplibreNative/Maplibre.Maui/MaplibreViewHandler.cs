using MaplibreMaui.Services;
using Microsoft.Maui.Handlers;

#if IOS 
using PlatformView = MapboxMaui.MapViewContainer;
#elif __ANDROID__
using PlatformView = AndroidX.Fragment.App.FragmentContainerView;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !__ANDROID__)
using PlatformView = System.Object;
#endif

namespace MaplibreMaui;

public partial class MaplibreViewHandler : ViewHandler<MaplibreView, PlatformView>
{
    public IMaplibreMapService MapService { get; }
    public MaplibreCallbackService CallbackService { get; } = new();
    
    public static IPropertyMapper<MaplibreView, MaplibreViewHandler> PropertyMapper
        = new PropertyMapper<MaplibreView, MaplibreViewHandler>(ViewHandler.ViewMapper)
        {
            // [nameof(MapboxView.CameraOptions)] = HandleCameraOptionsChanged,
            // [nameof(MapboxView.MapboxStyle)] = HandleMapboxStyleChanged,
            // [nameof(MapboxView.ScaleBarVisibility)] = HandleScaleBarVisibilityChanged,
            // [nameof(MapboxView.DebugOptions)] = HandleDebugOptionsChanged,
            // [nameof(MapboxView.Sources)] = HandleSourcesChanged,
            // [nameof(MapboxView.Layers)] = HandleLayersChanged,
            // [nameof(MapboxView.Images)] = HandleImagesChanged,
            // [nameof(MapboxView.Terrain)] = HandleTerrainChanged,
            // [nameof(MapboxView.Light)] = HandleLightChanged
        };

    public MaplibreViewHandler() : base(PropertyMapper)
    {
#if ANDROID
        MapService = new AndroidMaplibreMapService();
#elif IOS
        return null!;
#else
        return null!;
#endif
    }
}