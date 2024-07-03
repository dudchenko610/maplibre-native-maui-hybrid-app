using Android.Views;
using AndroidX.Fragment.App;
using Android.OS;
using Mapbox.Mapboxsdk.Maps;
using MaplibreMaui.Services;

using View = Android.Views.View;

namespace MaplibreMaui;

public partial class MaplibreFragment : Fragment
{
    private MapView? _mapView;
    private readonly AndroidMaplibreMapService _mapService;
    private readonly MaplibreCallbackService _callbackService;

    public MaplibreFragment(AndroidMaplibreMapService mapService, MaplibreCallbackService callbackService)
    {
        _mapService = mapService;
        _callbackService = callbackService;
    }

    public override View OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        Mapbox.Mapboxsdk.Mapbox.GetInstance(Context!);
        
        _mapView = new MapView(Context!);
        _mapView.GetMapAsync(this);
            
        return _mapView;
    }

    public override void OnStart()
    {
        base.OnStart();
        _mapView?.OnStart();
    }

    public override void OnStop()
    {
        base.OnStop();
        _mapView?.OnStop();
    }

    public override void OnLowMemory()
    {
        base.OnLowMemory();
        _mapView?.OnLowMemory();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        _mapView?.OnDestroy();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            _mapView?.Dispose();
        }
    }
}