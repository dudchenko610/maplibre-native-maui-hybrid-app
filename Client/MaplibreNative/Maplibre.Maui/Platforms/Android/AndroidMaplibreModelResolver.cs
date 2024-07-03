using Java.Lang;
using Mapbox.Mapboxsdk.Style.Expressions;
using Mapbox.Mapboxsdk.Style.Layers;
using Mapbox.Mapboxsdk.Style.Sources;
using MaplibreMaui.Models;
using Color = Android.Graphics.Color;

namespace MaplibreMaui;

internal static class AndroidMaplibreModelResolver
{
    public static Source? ConvertSource(MaplibreMaui.Models.Sources.Source source)
    {
        switch (source)
        {
            case MaplibreMaui.Models.Sources.VectorSource vectorSource:
            {
                var steadsTileSet = new TileSet("2.1.0", vectorSource.Tiles.ToArray());

                steadsTileSet.MinZoom = vectorSource.MinZoom;
                steadsTileSet.MaxZoom = vectorSource.MaxZoom;

                return new VectorSource(vectorSource.Id, steadsTileSet);
            }

            case MaplibreMaui.Models.Sources.GeoJsonSource geoJsonSource:
            {
                return !string.IsNullOrEmpty(geoJsonSource.GeoJson)
                    ? new GeoJsonSource(geoJsonSource.Id, geoJsonSource.GeoJson)
                    : new GeoJsonSource(geoJsonSource.Id);
            }
        }

        return null;
    }

    public static Layer? ConvertLayer(MaplibreMaui.Models.Layers.Layer layer)
    {
        Layer? resLayer = null;

        switch (layer)
        {
            case MaplibreMaui.Models.Layers.FillLayer fillLayer:
            {
                var layerMapLibre = new FillLayer(fillLayer.Id, fillLayer.SourceId);
                layerMapLibre.SourceLayer = fillLayer.SourceLayer;

                if (!string.IsNullOrWhiteSpace(layer.Filter))
                    layerMapLibre.Filter = Expression.Raw(layer.Filter);

                resLayer = layerMapLibre;
                break;
            }

            case MaplibreMaui.Models.Layers.LineLayer lineLayer:
            {
                var layerMapLibre = new LineLayer(lineLayer.Id, lineLayer.SourceId);
                layerMapLibre.SourceLayer = lineLayer.SourceLayer;

                if (!string.IsNullOrWhiteSpace(layer.Filter))
                    layerMapLibre.Filter = Expression.Raw(layer.Filter);

                resLayer = layerMapLibre;
                break;
            }

            case MaplibreMaui.Models.Layers.CircleLayer circleLayer:
            {
                var layerMapLibre = new CircleLayer(circleLayer.Id, circleLayer.SourceId);

                if (!string.IsNullOrWhiteSpace(layer.Filter))
                    layerMapLibre.Filter = Expression.Raw(layer.Filter);

                resLayer = layerMapLibre;
                break;
            }
        }

        if (resLayer is null) return resLayer;

        foreach (var propKeyValue in layer.Properties)
        {
            var propertyValue = ConvertLayerProperty(propKeyValue.Key, propKeyValue.Value);
            if (propertyValue is not null) resLayer.SetProperties(propertyValue);
        }

        return resLayer;
    }

    public static PropertyValue? ConvertLayerProperty(string property, object? value)
    {
        switch (property)
        {
            case Properties.FillColor:
                if (value is string fillColor) return PropertyFactory.FillColor(Color.ParseColor(fillColor));
                break;

            case Properties.FillOpacity:
                if (value is float opacity) return PropertyFactory.FillOpacity(Float.ValueOf(opacity));
                if (value is string fillOpacityStr) return PropertyFactory.FillOpacity(Expression.Raw(fillOpacityStr));
                break;

            case Properties.LineColor:
                if (value is string lineColor) return PropertyFactory.LineColor(Color.ParseColor(lineColor));
                break;

            case Properties.LineWidth:
                if (value is float lineWidth) return PropertyFactory.LineWidth(Float.ValueOf(lineWidth));
                break;

            case Properties.CircleColor:
                if (value is string circleColor) return PropertyFactory.CircleColor(Color.ParseColor(circleColor));
                break;

            case Properties.CircleRadius:
                if (value is float circleRadius) return PropertyFactory.CircleRadius(Float.ValueOf(circleRadius));
                if (value is string circleRadiusExpr)
                    return PropertyFactory.CircleRadius(Expression.Raw(circleRadiusExpr));
                break;

            case Properties.CircleStrokeWidth:
                if (value is float circleStrokeWidth)
                    return PropertyFactory.CircleStrokeWidth(Float.ValueOf(circleStrokeWidth));
                break;

            case Properties.CircleStrokeColor:
                if (value is string circleStrokeColor)
                    return PropertyFactory.CircleStrokeColor(Color.ParseColor(circleStrokeColor));
                break;

            case Properties.LineDasharray:
                if (value is string lineDashboard) return PropertyFactory.LineDasharray(Expression.Raw(lineDashboard));
                break;
        }

        return null;
    }
}