using System;
using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using Foundation;
using MapLibre.Native.iOS;
using MaplibreMaui.Models.Features;
using UIKit;

namespace MaplibreMaui;

internal static class iOSMaplibreFeatureResolver
{
	public static Feature ToMauiMaplibreFeature(IMLNFeature feature)
    {
        var result = new Feature
        {
            Id = feature.Identifier?.ToString(),
            Properties = AttributesToDictionary(feature.Attributes),
            Type = string.Empty,
            Coordinates = new List<object>()
        };

        try
        {
            var dict = feature.GeoJSONDictionary;
            if (dict is NSDictionary geo)
            {
                var geometryObj = geo["geometry"];
                var geometryDict = geometryObj as NSDictionary;
                var typeObj = geometryDict?["type"];
                result.Type = typeObj?.ToString() ?? string.Empty;

                var coordsObj = geometryDict?["coordinates"];
                var coords = ConvertCoordinatesNSObject(coordsObj);
                result.Coordinates = coords;
            }
        }
        catch
        {
            // ignore and leave defaults
        }

        return result;
    }

    static List<object> ConvertCoordinatesNSObject(NSObject? node)
    {
        var result = new List<object>();
        if (node is null) return result;

        if (node is NSArray arr)
        {
            // Detect if this is a simple [lon, lat] pair
			var items = arr.ToArray<NSObject>();
			bool isPair = items.Length >= 2 && items[0] is NSNumber && items[1] is NSNumber && items.Length <= 3;
            if (isPair)
            {
				var lon = (items[0] as NSNumber)!.DoubleValue;
				var lat = (items[1] as NSNumber)!.DoubleValue;
                result.Add(new[] { lon, lat });
                return result;
            }

            // Otherwise recursively convert each element
            foreach (var item in arr)
            {
                var sub = ConvertCoordinatesNSObject(item as NSObject);
                if (sub.Count == 1 && sub[0] is double[])
                {
                    // Flatten one level of pairs for common LineString ring
                    result.Add(sub[0]);
                }
                else
                {
                    result.Add(sub);
                }
            }
            return result;
        }

        // Fallback: return stringified node
        result.Add(node.ToString() ?? string.Empty);
        return result;
    }

    public static Dictionary<string, object?> AttributesToDictionary(NSDictionary<NSString, NSObject> attributes)
    {
        var dict = new Dictionary<string, object?>();
        foreach (var keyObj in attributes.Keys)
        {
            if (keyObj is NSString key)
            {
                var val = attributes.ObjectForKey(key);
                dict[key.ToString()] = ConvertNSObject(val);
            }
        }
        return dict;
    }

    static object? ConvertNSObject(NSObject? obj)
    {
        if (obj is null) return null;
        switch (obj)
        {
            case NSNull:
                return null;
            case NSString s:
                return s.ToString();
            case NSNumber n:
                // NSNumber can be bool or number; attempt boolean detection via ObjCType 'c'
                try
                {
                    return n.ObjCType == "c" ? n.BoolValue : n.DoubleValue;
                }
                catch
                {
                    return n.DoubleValue;
                }
            case UIColor c:
                return c.ToString();
            case NSDictionary dict:
            {
                var res = new Dictionary<string, object?>();
                foreach (var key in dict.Keys)
                {
                    if (key is NSString ks)
                        res[ks.ToString()] = ConvertNSObject(dict.ObjectForKey(ks));
                }
                return res;
            }
            case NSArray arr:
            {
                var list = new List<object?>();
                foreach (var item in arr)
                    list.Add(item is NSObject no ? ConvertNSObject(no) : null);
                return list;
            }
            default:
                return obj.ToString();
        }
    }
}

