using FarmApp.Shared.Math;
using MaplibreMaui.Models;
using MaplibreMaui.Models.Layers;
using MaplibreMaui.Models.Sources;

#if ANDROID 
using Android.Views;
// ReSharper disable AssignmentInConditionalExpression
#endif

namespace FarmApp.Mobile;

public partial class MainPage : ContentPage, IDisposable
{
    private bool _pointerIsDownOnMap;
    private readonly Vec2 _downPointerPos = new();

    private const string SteadsLayerId = "steads-layer-id";
    private const string LinesLayerId = "stead-lines-id-layer";
    private const string SteadsSourceId = "steads-source-id";
    private const string SteadPolygonsSourceLayer = "stead-polygons-layer";
    
    public MainPage()
    {
        InitializeComponent();
        
        SystemEventsProvider.OnTouchEvent += OnTouchEvent;
        MaplibreView.HandlerChanged = MaplibreHandlerInitialized;
        Main.OnBlazorInitialized += OnBlazorLoadedAsync;
    }
    
    private void MaplibreHandlerInitialized()
    {
        MaplibreView.CallbackService.OnMapReady += OnMapReady;
        MaplibreView.CallbackService.OnStyleLoaded += OnStyleLoaded;
    }

    public void Dispose()
    {
        SystemEventsProvider.OnTouchEvent -= OnTouchEvent;
        MaplibreView.CallbackService.OnMapReady -= OnMapReady;
        MaplibreView.CallbackService.OnStyleLoaded -= OnStyleLoaded;
        
        Main.OnBlazorInitialized -= OnBlazorLoadedAsync;
    }

    private async Task OnBlazorLoadedAsync()
    {
        await Task.Delay(300);
        BackgroundImage.IsVisible = false;
    }
    
    private void OnMapReady()
    {
        MaplibreView.MapService.SetStyle(49.985983f, 36.233640f, 10, 
            "https://tile.openstreetmap.org.ua/styles/osm-bright/style.json");
        
        MaplibreView.MapService.ToggleQuickZoomActions(false);
        MaplibreView.MapService.ToggleCompass(false);
        MaplibreView.MapService.ToggleDebugMode(false);
    }
    
    private void OnStyleLoaded()
    {
        // var steadSource = new VectorSource(SteadsSourceId)
        // {
        //     Tiles = new List<string> { "https://domain.com/{z}/tile_{z}_{x}_{y}.mvt" },
        //     MinZoom = 11,
        //     MaxZoom = 14
        // };
        //
        // var fillLayer = new FillLayer(SteadsLayerId, SteadsSourceId)
        // {
        //     SourceLayer = SteadPolygonsSourceLayer,
        //     Properties = new Dictionary<string, object?>
        //     {
        //         { Properties.FillColor, "#008800" },
        //         { Properties.FillOpacity, $"['case', ['boolean', ['feature-state', 'selected'], false], 0.8, 0.1]" }
        //     }
        // };
        //
        // var lineLayer = new LineLayer(LinesLayerId, SteadsSourceId)
        // {
        //     SourceLayer = SteadPolygonsSourceLayer,
        //     Properties = new Dictionary<string, object?>
        //     {
        //         { Properties.LineColor, "#008C00" },
        //         { Properties.LineWidth, 0.5f }
        //     }
        // };
        //
        // MaplibreView.MapService.AddSource(steadSource);
        // MaplibreView.MapService.AddLayer(fillLayer);
        // MaplibreView.MapService.AddLayer(lineLayer);
    }
    
    private bool OnTouchEvent(object e)
    {
        var processResult = true;
        
        #if ANDROID 
        {
            if (e is not MotionEvent motionEvent) return true;

            if (motionEvent.ActionMasked == MotionEventActions.Move)
            {
                var x = motionEvent.GetX();
                var y = motionEvent.GetY();

                if (_pointerIsDownOnMap)
                {
                    // process map move
                    // foreach (var service in _services)
                    //     processResult &= service.OnMapMove(x, y);
                }
            }
            
            if (motionEvent.ActionMasked == MotionEventActions.Up)
            {
                var pointerIsDownOnMap = _pointerIsDownOnMap;
                _pointerIsDownOnMap = false;

                var x = motionEvent.GetX();
                var y = motionEvent.GetY();
                const float tolerance = 0.01f;

                // process map up event
                // foreach (var service in _services)
                //     processResult &= service.OnUp(x, y);
                
                // process map click event
                // if (pointerIsDownOnMap && Math.Abs(x - _downPointerPos.X) < tolerance && Math.Abs(y - _downPointerPos.Y) < tolerance)
                //     foreach (var service in _services)
                //         processResult &= service.OnClick(x, y);
            }
            
            if (motionEvent.ActionMasked == MotionEventActions.Down)
            {
                var x = motionEvent.GetX();
                var y = motionEvent.GetY();
                
                if (!(_pointerIsDownOnMap = MapCollisionResolver.IsPointerOnMap(x, y))) return true;

                _downPointerPos.X = x;
                _downPointerPos.Y = y;
                
                // process map down event
                // foreach (var service in _services)
                //     processResult &= service.OnDown(x, y);
            }

            if (motionEvent.ActionMasked is MotionEventActions.PointerDown or MotionEventActions.Pointer1Down or MotionEventActions.Pointer2Down or MotionEventActions.Pointer3Down)
            {
                var x = motionEvent.GetX();
                var y = motionEvent.GetY();
                
                if (motionEvent.PointerCount > 1)
                {
                    // process map up event
                    // foreach (var service in _services)
                    //     processResult &= service.OnUp(x, y);
                }
            }
        }
        #elif IOS
        {
        }
        #endif
        
        if (processResult || _pointerIsDownOnMap)
            MaplibreView.TriggerTouchEvent(e);

        return processResult;
    }
}