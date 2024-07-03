using Mapbox.Geojson;
using Mapbox.Mapboxsdk.Camera;
using Mapbox.Mapboxsdk.Maps;
using Mapbox.Mapboxsdk.Style.Expressions;
using MaplibreMaui.Models.Layers;
using MaplibreMaui.Models.Sources;
using MaplibreMaui.Services;
using Boolean = Java.Lang.Boolean;
using Feature = MaplibreMaui.Models.Features.Feature;
using GeoJsonSource = Mapbox.Mapboxsdk.Style.Sources.GeoJsonSource;
using LatLng = MaplibreMaui.Models.Layers.LatLng;
using PointF = Android.Graphics.PointF;
using Style = Mapbox.Mapboxsdk.Maps.Style;

namespace MaplibreMaui;

public class AndroidMaplibreMapService : IMaplibreMapService
{
    internal MapboxMap? MapboxMap { get; set; }
    internal MaplibreFragment? MaplibreFragment { get; set; }
    internal Style? Style { get; set; }

    public double Zoom => MapboxMap?.CameraPosition.Zoom ?? 0;
    public double Bearing => MapboxMap?.CameraPosition.Bearing ?? 0;

    public void SetStyle(float lat, float lng, float zoom, string styleUrl)
    {
        if (MapboxMap is null || MaplibreFragment is null) return;
        
        MapboxMap.CameraPosition = new CameraPosition.Builder()
            .Target(new Mapbox.Mapboxsdk.Geometry.LatLng(lat, lng))
            .Zoom(zoom) 
            .Build()!;
        
        MapboxMap.SetStyle(styleUrl, MaplibreFragment);
        MapboxMap.UiSettings.SetAttributionMargins(15, 0, 0, 15);
    }

    public void AddSource(Source s)
    {
        if (Style is null) return;
        
        var source = AndroidMaplibreModelResolver.ConvertSource(s);
        if (source is null) return;

        Style.AddSource(source);
    }

    public void AddLayer(Layer l)
    {
        if (Style is null) return;
        
        var layer = AndroidMaplibreModelResolver.ConvertLayer(l);
        if (layer is null) return;

        Style.AddLayer(layer);
    }

    public void RemoveSource(string id)
    {
        Style?.RemoveSource(id);
    }

    public void RemoveLayer(string id)
    {
        Style?.RemoveLayer(id);
    }

    public LatLng LatLngFromScreenLocation(float x, float y)
    {
        if (MapboxMap is null) return new LatLng();

        var latLng = MapboxMap.Projection.FromScreenLocation(new PointF(x, y));
        return new LatLng { Lat = latLng.Latitude, Lng = latLng.Longitude };
    }

    public (float X, float Y) ScreenLocationFromLatLng(LatLng latLng)
    {
        if (MapboxMap is null) return (0, 0);

        var pointF = MapboxMap.Projection
                .ToScreenLocation(new Mapbox.Mapboxsdk.Geometry.LatLng(latLng.Lat, latLng.Lng));
        
        return (pointF.X, pointF.Y);
    }

    public double[] ScreenLocationFromPoint(double[] point)
    {
        if (MapboxMap is null) return new double [] { 0, 0 };

        var pointF = MapboxMap.Projection
            .ToScreenLocation(new Mapbox.Mapboxsdk.Geometry.LatLng(point[1], point[0]));

        return new double[] { pointF.X, pointF.Y };
    }

    public void SetGeoJsonFeature(string geoJsonSourceId, string json)
    {
        var source = Style?.GetSource(geoJsonSourceId);
        if (source is not GeoJsonSource geoJsonSource) return;
        
        var feature = FeatureCollection.FromJson(json);
        geoJsonSource.SetGeoJson(feature);
    }

    public void SetGeoJsonFeature(string geoJsonSourceId, Models.Features.FeatureCollection featureCollection)
    {
        var source = Style?.GetSource(geoJsonSourceId);
        if (source is not GeoJsonSource geoJsonSource) return;

        var feature = AndroidMaplibreFeatureResolver.ToMaplibreFeatureCollection(featureCollection);
        geoJsonSource.SetGeoJson(feature);
    }

    public void AddBoolPropertyToFeature(string featureId, string layerId, float x, float y, string key, bool value)
    {
        if (MapboxMap is null || MaplibreFragment?.View is null) return;
    
        var features = MapboxMap.QueryRenderedFeatures(
            new PointF(x, y),
            Expression.Raw($"['==', '$id', {featureId}]"),
            layerId);
        
        var feature = features.FirstOrDefault();
        feature?.AddBooleanProperty(key, new Boolean(value));
    }

    public string? QueryFeaturePropertyByPoint(string layerId, string propertyKey, float x, float y)
    {
        if (MapboxMap is null) return default;

        var featureList = MapboxMap.QueryRenderedFeatures(new PointF(x, y), layerId);
        var feature = featureList.FirstOrDefault();

        return feature?.GetStringProperty(propertyKey);
    }

    public Dictionary<string, object?>? QueryFeaturePropertiesByPoint(string layerId, float x, float y)
    {
        if (MapboxMap is null) return default;

        var featureList = MapboxMap.QueryRenderedFeatures(new PointF(x, y), layerId);
        var feature = featureList.FirstOrDefault();
        
        if (feature is null) return default;

        var properties = feature.Properties();
        var propertiesDictionary = properties is not null ? 
            AndroidMaplibreFeatureResolver.ConvertJsonObjectToDictionary(properties) : new Dictionary<string, object?>();
        
        return propertiesDictionary;
    }

    public Feature? QueryFeatureByPoint(string layerId, float x, float y)
    {
        if (MapboxMap is null) return null;
        
        var featureList = MapboxMap.QueryRenderedFeatures(new PointF(x, y), layerId);
        
        var feature = featureList.FirstOrDefault();
        
        return feature is null ? null : AndroidMaplibreFeatureResolver.ToMauiMaplibreFeature(feature);
    }

    public List<Feature> QuerySourceFeatures(string sourceId, string sourceLayer, string filterExpression)
    {
        if (Style is null) return new List<Feature>();

        var source = Style.GetSource(sourceId);
        if (source is not Mapbox.Mapboxsdk.Style.Sources.VectorSource vectorSource) return new List<Feature>();

        var features = vectorSource
            .QuerySourceFeatures(new[] { sourceLayer }, Expression.Raw(filterExpression));
        
        if (features is null) return new List<Feature>();

        return features.Select(AndroidMaplibreFeatureResolver.ToMauiMaplibreFeature).ToList();
    }

    public List<Feature> QuerySourceFeatures(string sourceId, string filterExpression)
    {
        if (Style is null) return new List<Feature>();

        var source = Style.GetSource(sourceId);
        if (source is not Mapbox.Mapboxsdk.Style.Sources.GeoJsonSource geoJsonSource) return new List<Feature>();

        var features = geoJsonSource
            .QuerySourceFeatures(Expression.Raw(filterExpression));
        
        if (features is null) return new List<Feature>();

        return features.Select(AndroidMaplibreFeatureResolver.ToMauiMaplibreFeature).ToList();
    }

    public List<string> QuerySourceFeaturesAsJson(string sourceId, string sourceLayer, string filterExpression)
    {
        if (Style is null) return new List<string>();

        var source = Style.GetSource(sourceId);
        if (source is not Mapbox.Mapboxsdk.Style.Sources.VectorSource vectorSource) return new List<string>();

        var features = vectorSource
            .QuerySourceFeatures(new[] { sourceLayer }, Expression.Raw(filterExpression));
        
        if (features is null) return new List<string>();

        return features.Select(x => $"{x.ToJson()}").ToList();
    }

    public void SetFilter(string layerId, string filterExp)
    {
        var layer = Style?.GetLayer(layerId);
        if (layer is null) return;
        
        var type = layer.GetType();
        var propertyInfo = type.GetProperty("Filter");

        if (propertyInfo != null && propertyInfo.CanWrite)
        {
            propertyInfo.SetValue(layer, Expression.Raw(filterExp));
        }
    }

    public void SetProperty(string layerId, string property, object? val)
    {
        var layer = Style?.GetLayer(layerId);
        if (layer is null) return;
        
        var propertyValue = AndroidMaplibreModelResolver.ConvertLayerProperty(property, val);
        if (propertyValue is null) return;
        
        layer.SetProperties(propertyValue);
    }

    public void SetZoomRange(string layerId, float min, float max)
    {
        var layer = Style?.GetLayer(layerId);
        if (layer is null) return;

        layer.MinZoom = min;
        layer.MaxZoom = max;
    }

    public void FlyTo(float lng, float lat, float zoom)
    {
        if (MapboxMap is null) return;
        
        var position = new CameraPosition.Builder()
            .Target(new Mapbox.Mapboxsdk.Geometry.LatLng(lat, lng))
            .Zoom(zoom)
            .Build();
        
        MapboxMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(position), 1800);
    }

    public void ResetBearing()
    {
        if (MapboxMap is null) return;

        var bearing = Bearing;
        var position = new CameraPosition.Builder()
            .Bearing(0)
            .Build();

        var duration = (int)bearing * 2;
        if (duration is 0) return;
        
        MapboxMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(position), duration);
    }

    public void CancelTransitions()
    {
        MapboxMap?.CancelTransitions();
    }

    public void ToggleActions(bool toggle)
    {
        if (MapboxMap is null) return;
        
        MapboxMap.UiSettings.RotateGesturesEnabled = toggle;
        MapboxMap.UiSettings.ScrollGesturesEnabled = toggle;
        MapboxMap.UiSettings.ZoomGesturesEnabled = toggle;
        MapboxMap.UiSettings.DoubleTapGesturesEnabled = toggle;
    }

    public void ToggleDoubleTapActions(bool toggle)
    {
        if (MapboxMap is null) return;
        MapboxMap.UiSettings.DoubleTapGesturesEnabled = toggle;
    }

    public void ToggleQuickZoomActions(bool toggle)
    {
        if (MapboxMap is null) return;

        MapboxMap.UiSettings.QuickZoomGesturesEnabled = toggle;
    }

    public void ToggleCompass(bool toggle)
    {
        if (MapboxMap is null) return;

        MapboxMap.UiSettings.CompassEnabled = toggle;
    }

    public void ToggleDebugMode(bool toggle)
    {
        if (MapboxMap is null) return;
        MapboxMap.DebugActive = toggle;
    }
}