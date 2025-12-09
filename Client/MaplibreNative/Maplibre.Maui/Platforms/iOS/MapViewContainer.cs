using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using MapLibre.Native.iOS;
using UIKit;

namespace MapboxMaui;

public class MapViewContainer : UIView
{
    private readonly MaplibreMaui.Services.MaplibreCallbackService _callbackService;
    private readonly MaplibreMaui.iOSMaplibreMapService _mapService;
    private MLNMapView? _mapView;

    public MapViewContainer(MaplibreMaui.iOSMaplibreMapService mapService, 
        MaplibreMaui.Services.MaplibreCallbackService callbackService,
        string styleUrl,
        float startLatitude,
        float startLongitude,
        float startZoom)
    {
        _mapService = mapService;
        _callbackService = callbackService;
        
        // Ensure MapLibre uses a tokenless tile server for built-in styles
        MLNSettings.UseWellKnownTileServer(MLNWellKnownTileServer.Libre);
        
        _mapView = new MLNMapView(UIScreen.MainScreen.Bounds);
        _mapView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
        _mapView.Delegate = new MapViewDelegate(_mapService, _callbackService);
        
        // Enable verbose MapLibre logging to console for debugging
        var logCfg = MLNLoggingConfiguration.SharedConfiguration;
        logCfg.LoggingLevel = MLNLoggingLevel.Verbose;
        logCfg.Handler = (level, file, line, msg) =>
        {
            Console.WriteLine($"[MLN {level}] {file}:{line} {msg}");
        };
        AddSubview(_mapView);

        _mapService.MapView = _mapView;
        _mapService.SetStyle(startLatitude, startLongitude, startZoom, styleUrl);
        
        _mapService.ToggleQuickZoomActions(false);
        _mapService.ToggleCompass(false);
        _mapService.ToggleDebugMode(false);
    }

    public override void LayoutSubviews()
    {
        base.LayoutSubviews();
        if (_mapView is not null) _mapView.Frame = Bounds;
    }

    class MapViewDelegate : MLNMapViewDelegate
    {
        private readonly MaplibreMaui.iOSMaplibreMapService _svc;
        private readonly MaplibreMaui.Services.MaplibreCallbackService _cb;

        public MapViewDelegate(MaplibreMaui.iOSMaplibreMapService svc, MaplibreMaui.Services.MaplibreCallbackService cb)
        {
            _svc = svc;
            _cb = cb;
        }

        [Export("mapView:didFinishLoadingStyle:")]
        public void MapView(MLNMapView mapView, MLNStyle style)
        {
            _svc.Style = style;
            _cb.InvokeStyleLoaded();
            
            // Inspect style contents to ensure sources/layers loaded
            Console.WriteLine($"[MLN] Style loaded. Sources: {style.Sources?.Count}, Layers: {style.Layers?.Length}");
        }
        
        [Export("mapViewWillStartLoadingMap:")]
        public void MapViewWillStartLoadingMap(MLNMapView mapView)
        {
            Console.WriteLine("[MLN] Will start loading map");
        }
        
        [Export("mapViewDidFinishLoadingMap:")]
        public void MapViewDidFinishLoadingMap(MLNMapView mapView)
        {
            Console.WriteLine("[MLN] Did finish loading map");
        }
        
        [Export("mapViewDidFailLoadingMap:withError:")]
        public void MapViewDidFailLoadingMap(MLNMapView mapView, NSError error)
        {
            Console.WriteLine($"[MLN] Failed loading map: {error?.Code} {error?.LocalizedDescription}");
        }
        
        [Export("mapViewWillStartRenderingMap:")]
        public void MapViewWillStartRenderingMap(MLNMapView mapView)
        {
            Console.WriteLine("[MLN] Will start rendering map");
        }
        
        [Export("mapViewDidFinishRenderingMap:fullyRendered:")]
        public void MapViewDidFinishRenderingMap(MLNMapView mapView, bool fullyRendered)
        {
            Console.WriteLine($"[MLN] Did finish rendering map. fullyRendered = {fullyRendered}");
        }

        [Export("mapView:regionDidChangeWithReason:animated:")]
        public void MapViewRegionDidChangeWithReason(MLNMapView mapView, MLNCameraChangeReason reason, bool animated)
        {
            _cb.InvokeMapRotate();
        }

        [Export("mapView:regionDidChangeAnimated:")]
        public void MapViewRegionDidChangeAnimated(MLNMapView mapView, bool animated)
        {
            _cb.InvokeMapRotate();
        }

        [Export("mapViewRegionIsChanging:")]
        public void MapViewRegionIsChanging(MLNMapView mapView)
        {
            _cb.InvokeMapRotate();
        }

        [Export("mapView:regionIsChangingWithReason:")]
        public void MapViewRegionIsChangingWithReason(MLNMapView mapView, MLNCameraChangeReason reason)
        {
            _cb.InvokeMapRotate();
        }

        [Export("locationManager:didFailWithError:")]
        public void LocationManager(MLNLocationManager manager, NSError error)
        {
            
        }
    }
}

