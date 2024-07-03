using System.Text.Json;
using GoogleGson;
using Mapbox.Geojson;
using JsonElement = GoogleGson.JsonElement;
using Feature = MaplibreMaui.Models.Features.Feature;
using FeatureCollection = MaplibreMaui.Models.Features.FeatureCollection;
using LineString = Mapbox.Geojson.LineString;
using Point = Mapbox.Geojson.Point;
using Polygon = Mapbox.Geojson.Polygon;

namespace MaplibreMaui;

internal static class AndroidMaplibreFeatureResolver
{
    public static Feature ToMauiMaplibreFeature(Mapbox.Geojson.Feature feature)
    {
        // Get geometry
        var geometry = feature.Geometry();
        List<object> coordinates = new();
        var type = string.Empty;

        switch (geometry)
        {
            case Point pointGeometry:
            {
                var lat = pointGeometry.Latitude();
                var lng = pointGeometry.Longitude();
                coordinates.Add(new double[] { lng, lat });
                type = "Point";
        
                break;
            }
            case LineString lineStringGeometry:
            {
                var points = lineStringGeometry.Coordinates();
                coordinates.AddRange(points.Select(x => new double[] { x.Longitude(), x.Latitude() }).ToList());
                type = "Line";
        
                break;
            }
            case Polygon polygonGeometry:
            {
                var points = polygonGeometry.Coordinates();
                coordinates.AddRange(points.Select(x => x.Select(l => new double[] { l.Longitude(), l.Latitude() }).ToList()).ToList());
                type = "Polygon";
        
                break;
            }
            
            default:
                // throw new System.Exception("Undefined type");
                break;
        }
        
        var properties = feature.Properties();
        var propertiesDictionary = properties is not null ? 
            ConvertJsonObjectToDictionary(properties) : new Dictionary<string, object?>();

        return new Feature
        {
            Id = feature.Id(),
            Type = type,
            Coordinates = coordinates,
            Properties = propertiesDictionary
        };
    }
    
    private static object? ConvertJsonElementToObject(JsonElement element)
    {
        switch (element)
        {
            case JsonPrimitive jsonPrimitive:
                return ConvertJsonPrimitive(jsonPrimitive);
            case JsonObject jsonObject:
                return ConvertJsonObjectToDictionary(jsonObject);
            case JsonArray jsonArray:
                return ConvertJsonArrayToList(jsonArray);
            default:
                return null; // Or handle other cases as needed
                // return element.AsString; // Or handle other cases as needed
        }
    }
    
    private static object? ConvertJsonPrimitive(JsonPrimitive jsonPrimitive)
    {
        if (jsonPrimitive.IsBoolean)
            return jsonPrimitive.AsBoolean;
        if (jsonPrimitive.IsNumber)
            return jsonPrimitive.AsNumber;
        if (jsonPrimitive.IsString)
            return jsonPrimitive.AsString;

        return null;
    }
    
    private static List<object?> ConvertJsonArrayToList(JsonArray jsonArray)
    {
        var arrayList = jsonArray.AsList();
        return arrayList is null ? new List<object?>() : arrayList.Select(ConvertJsonElementToObject).ToList();
    }
    
    public static Dictionary<string, object?> ConvertJsonObjectToDictionary(JsonObject jsonObj)
    {
        var keySet = jsonObj.KeySet();
        if (keySet is null) return new Dictionary<string, object?>();

        return keySet.Select(x => (Key: x, Value: jsonObj.Get(x)))
            .Where(x => x.Value is not null)
            .ToDictionary(x => x.Key, x => ConvertJsonElementToObject(x.Value!));
    }

    public static Mapbox.Geojson.Feature? ToMaplibreFeature(Feature feature)
    {
        IGeometry? geometry = null;
        
        switch (feature.Type)
        {
            case "Point":
            {
                var latLng = (double[]) feature.Coordinates.First();
                geometry = Point.FromLngLat(latLng[0], latLng[1]);
                
                break;
            }
            
            case "Polygon":
            {
                // Console.WriteLine(JsonSerializer.Serialize(feature.Coordinates));
                var coordinates = feature.Coordinates.Select(x => (double[])x)
                    .ToList();
                
                // Console.WriteLine("points");
                // Console.WriteLine(JsonSerializer.Serialize(coordinates));

                var lst = coordinates.Select(x => Mapbox.Geojson.Point.FromLngLat(x[0], x[1])).ToArray();


                var lineString = LineString.FromLngLats(lst!);
                geometry = Polygon.FromLngLats(new List<IList<Point>> { lineString!.Coordinates().ToList() });
                
                break;
            }
        }

        if (geometry is null) return null;
        var maplibreFeature = Mapbox.Geojson.Feature.FromGeometry(geometry);
        
        // foreach (var propertyPair in feature.Properties)
        // {
        //     switch (propertyPair.Value)
        //     {
        //         case bool boolVal:
        //             maplibreFeature?.AddBooleanProperty(propertyPair.Key, new Java.Lang.Boolean(boolVal));
        //             break;
        //         case string strVal:
        //             maplibreFeature?.AddStringProperty(propertyPair.Key, strVal);
        //             break;
        //         case int intVal:
        //             maplibreFeature?.AddNumberProperty(propertyPair.Key, new Java.Lang.Integer(intVal));
        //             break;
        //         case float floatVal:
        //             maplibreFeature?.AddNumberProperty(propertyPair.Key, new Java.Lang.Float(floatVal));
        //             break;
        //     }
        // }
        
        return maplibreFeature;
    }
    
    public static Mapbox.Geojson.FeatureCollection ToMaplibreFeatureCollection(FeatureCollection featureCollection)
    {
        return Mapbox.Geojson.FeatureCollection
            .FromFeatures(featureCollection.Features
                .Select(ToMaplibreFeature).Where(x => x is not null).ToList()!)!;
    }
}