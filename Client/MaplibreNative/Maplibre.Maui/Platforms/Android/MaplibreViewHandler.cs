using PlatformView = AndroidX.Fragment.App.FragmentContainerView;
using Microsoft.Maui.Platform;
using Android.Views;

namespace MaplibreMaui;

public partial class MaplibreViewHandler
{
    MaplibreFragment? _maplibreFragment;
    
    protected override PlatformView CreatePlatformView()
    {
        var mainActivity = (MauiAppCompatActivity?) Context.GetActivity();
        var fragmentContainerView = new PlatformView(Context) { Id = Android.Views.View.GenerateViewId() };
        
        var maplibreView = VirtualView as MaplibreView;
        
        var styleUrl = maplibreView.StyleUrl;
        var lat = maplibreView.StartLatitude;
        var lng = maplibreView.StartLongitude;
        var zoom = maplibreView.StartZoom;
        
        _maplibreFragment = new MaplibreFragment((AndroidMaplibreMapService) MapService, CallbackService,
            styleUrl, lat, lng, zoom);

        var fragmentTransaction = mainActivity.SupportFragmentManager.BeginTransaction();
        fragmentTransaction.Replace(fragmentContainerView.Id, _maplibreFragment, $"mapbox-maui-{fragmentContainerView.Id}");
        fragmentTransaction.CommitAllowingStateLoss();
        return fragmentContainerView;
    }

    protected override void ConnectHandler(PlatformView platformView)
    {
        base.ConnectHandler(platformView);
    }

    protected override void DisconnectHandler(PlatformView platformView)
    {
        if (_maplibreFragment != null)
        {
            // mapboxFragment.MapViewReady -= HandleMapViewReady;
            // mapboxFragment.StyleLoaded -= HandleStyleLoaded;
            // mapboxFragment.MapLoaded -= HandleMapLoaded;
            // mapboxFragment.MapClicked -= HandleMapClicked;
            _maplibreFragment.Dispose();
            _maplibreFragment = null;
        }

        if (VirtualView is MaplibreView mapboxView)
        {
            // mapboxView.AnnotationController = null;
            // mapboxView.QueryManager = null;
        }
        base.DisconnectHandler(platformView);
    }

    internal void TriggerTouchEvent(object e)
    {
        var fragmentView = _maplibreFragment?.View;
        if (fragmentView is null || e is not MotionEvent motionEvent) return;
        
        fragmentView.DispatchTouchEvent(MotionEvent.Obtain(motionEvent));
    }
}