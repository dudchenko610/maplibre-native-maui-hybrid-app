using MaplibreMaui.Models.Features;
using MaplibreMaui.Models.Layers;
using MaplibreMaui.Models.Sources;
using LatLng = MaplibreMaui.Models.Layers.LatLng;

namespace MaplibreMaui.Services;

public interface IMaplibreMapService
{
    double Zoom { get; }
    double Bearing { get; }
    
    void SetStyle(float lat, float lng, float zoom, string styleUrl);
    void AddSource(Source s);
    void AddLayer(Layer l);

    void RemoveSource(string id);
    void RemoveLayer(string id);
    LatLng LatLngFromScreenLocation(float x, float y);
    (float X, float Y) ScreenLocationFromLatLng(LatLng latLng);
    double[] ScreenLocationFromPoint(double[] point);

    void SetGeoJsonFeature(string geoJsonSourceId, string json);
    void SetGeoJsonFeature(string geoJsonSourceId, FeatureCollection featureCollection);
    void AddBoolPropertyToFeature(string featureId, string layerId, float x, float y, string key, bool value);

    string? QueryFeaturePropertyByPoint(string layerId, string propertyKey, float x, float y);
    Dictionary<string, object?>? QueryFeaturePropertiesByPoint(string layerId, float x, float y);
    Feature? QueryFeatureByPoint(string layerId, float x, float y);
    List<Feature> QuerySourceFeatures(string sourceId, string sourceLayer, string filterExpression);
    List<Feature> QuerySourceFeatures(string sourceId, string filterExpression);
    List<string> QuerySourceFeaturesAsJson(string sourceId, string sourceLayer, string filterExpression);
    
    void SetFilter(string layerId, string filterExp);
    void SetProperty(string layerId, string property, object? val);
    void SetZoomRange(string layerId, float min, float max);
    void FlyTo(float x, float y, float zoom);
    void ResetBearing();
    void CancelTransitions();

    void ToggleActions(bool toggle);
    void ToggleDoubleTapActions(bool toggle);
    void ToggleQuickZoomActions(bool toggle);
    void ToggleCompass(bool toggle);
    void ToggleDebugMode(bool toggle);
}