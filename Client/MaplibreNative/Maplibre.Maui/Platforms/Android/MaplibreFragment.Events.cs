using Mapbox.Android.Gestures;
using Mapbox.Mapboxsdk.Maps;
using Style = Mapbox.Mapboxsdk.Maps.Style;

namespace MaplibreMaui;

public partial class MaplibreFragment : IOnMapReadyCallback, MapboxMap.IOnMoveListener,
    MapboxMap.IOnCameraIdleListener, MapboxMap.IOnRotateListener,
    Style.IOnStyleLoaded
{
    public void OnMapReady(MapboxMap map)
    {
        _mapService.MapboxMap = map;
        _mapService.MaplibreFragment = this;
        map.AddOnRotateListener(this);
        // map.AddOnMoveListener(this);
        // map.AddOnCameraIdleListener(this);
        
        _callbackService.InvokeMapReady();
    }

    public void OnStyleLoaded(Style style)
    {
        _mapService.Style = style;
        _callbackService.InvokeStyleLoaded();
    }

    public void OnMove(MoveGestureDetector p0)
    {
    }

    public void OnMoveBegin(MoveGestureDetector p0)
    {
        _callbackService.InvokeMapMoveStart();
    }

    public void OnMoveEnd(MoveGestureDetector p0)
    {
        _callbackService.InvokeMapMoveEnd();
    }

    public void OnCameraIdle()
    {
        _callbackService.InvokeCameraIdle();
    }

    public void OnRotate(RotateGestureDetector p0)
    {
        _callbackService.InvokeMapRotate();
    }

    public void OnRotateBegin(RotateGestureDetector p0)
    {
    }

    public void OnRotateEnd(RotateGestureDetector p0)
    {
    }
}