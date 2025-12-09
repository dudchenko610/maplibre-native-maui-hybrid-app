using System;
using System.Collections.Generic;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using MapLibre.Native.iOS;
using MaplibreMaui.Models;
using MaplibreMaui.Models.Layers;
using MaplibreMaui.Models.Sources;
using MaplibreMaui.Services;
using UIKit;
using LatLng = MaplibreMaui.Models.Layers.LatLng;
using Style = Microsoft.Maui.Controls.Style;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
// using Newtonsoft.Json;

namespace MaplibreMaui;

public class iOSMaplibreMapService : IMaplibreMapService
{
    internal MLNMapView? MapView { get; set; }
    internal MLNStyle? Style { get; set; }

    public double Zoom => MapView?.ZoomLevel ?? 0;
    public double Bearing => MapView?.Direction ?? 0;

    public void SetStyle(float lat, float lng, float zoom, string styleUrl)
    {
        if (MapView is null) return;

        var url = new NSUrl(styleUrl);
        // var url2 = MLNStyle.DefaultStyleURL;
        MapView.StyleURL = url;

        var clampedZoom = Math.Max(0, Math.Min(22, zoom));
        MapView.SetCenterCoordinate(new CoreLocation.CLLocationCoordinate2D(lat, lng), false);
        MapView.ZoomLevel = clampedZoom;
    }
    
    public void AddSource(Source s)
    {
        if (Style is null) return;

        var source = ConvertSource(s);
        if (source is null) return;

        Style.AddSource(source);
    }

    public void AddLayer(Layer l)
    {
        if (Style is null) return;

        var layer = ConvertLayer(l);
        if (layer is null) return;

        Style.AddLayer(layer);
    }

    public void RemoveSource(string id)
    {
        if (Style is null) return;
        var src = Style.SourceWithIdentifier(id);
        if (src != null) Style.RemoveSource(src);
    }

    public void RemoveLayer(string id)
    {
        if (Style is null) return;
        var layer = Style.LayerWithIdentifier(id);
        if (layer != null) Style.RemoveLayer(layer);
    }

    public LatLng LatLngFromScreenLocation(float x, float y)
    {
        if (MapView is null) return new LatLng();
        var coord = MapView.ConvertPoint(new CGPoint(x, y), MapView);
        return new LatLng { Lat = coord.Latitude, Lng = coord.Longitude };
    }

    public (float X, float Y) ScreenLocationFromLatLng(LatLng latLng)
    {
        if (MapView is null) return (0, 0);
        var point = MapView.ConvertCoordinate(new CoreLocation.CLLocationCoordinate2D(latLng.Lat, latLng.Lng), MapView);
        return ((float)point.X, (float)point.Y);
    }

    public double[] ScreenLocationFromPoint(double[] point)
    {
        if (MapView is null) return new[] { 0d, 0d };
        var cgPoint = MapView.ConvertCoordinate(new CoreLocation.CLLocationCoordinate2D(point[1], point[0]), MapView);
        return new[] { (double)cgPoint.X, (double)cgPoint.Y };
    }

    public void SetGeoJsonFeature(string geoJsonSourceId, string json)
    {
        if (Style is null) return;
        var src = Style.SourceWithIdentifier(geoJsonSourceId) as MLNShapeSource;
        if (src is null) return;

        var data = NSData.FromString(json, NSStringEncoding.UTF8);
        NSError? err;
        var shape = MLNShape.ShapeWithData(data, (nuint)NSStringEncoding.UTF8, out err!);
        if (err is null && shape is not null)
        {
            src.Shape = shape;
        }
    }

    public void SetGeoJsonFeature(string geoJsonSourceId, Models.Features.FeatureCollection featureCollection)
    {
        // TODO: Implement conversion to MLNFeature/MLNShape if needed.
        // For now, no-op to keep parity with Android API surface.
    }

    public void AddBoolPropertyToFeature(string featureId, string layerId, float x, float y, string key, bool value)
    {
        // Not implemented on iOS for now
    }

    public string? QueryFeaturePropertyByPoint(string layerId, string propertyKey, float x, float y)
    {
        if (MapView is null) return default;
        
        var point = new CGPoint(x, y);
        var ids = new Foundation.NSSet<Foundation.NSString>(new[] { new Foundation.NSString(layerId) });
        var features = MapView.VisibleFeaturesAtPoint(point, ids, predicate: null);
        var feature = features?.FirstOrDefault();
        if (feature is null) return default;
        
        var val = feature.AttributeForKey(new Foundation.NSString(propertyKey));
        return val?.ToString();
    }

    public Dictionary<string, object?>? QueryFeaturePropertiesByPoint(string layerId, float x, float y)
    {
        if (MapView is null) return default;
        
        var point = new CGPoint(x, y);
        var ids = new Foundation.NSSet<Foundation.NSString>(new[] { new Foundation.NSString(layerId) });
        var features = MapView.VisibleFeaturesAtPoint(point, ids, predicate: null);
        var feature = features?.FirstOrDefault();
        if (feature is null) return default;
        
        return iOSMaplibreFeatureResolver.AttributesToDictionary(feature.Attributes);
    }

    public Models.Features.Feature? QueryFeatureByPoint(string layerId, float x, float y)
    {
        if (MapView is null) return null;
        
        var point = new CGPoint(x, y);
        var ids = new Foundation.NSSet<Foundation.NSString>(new[] { new Foundation.NSString(layerId) });
        var features = MapView.VisibleFeaturesAtPoint(point, ids, predicate: null);
        var feature = features?.FirstOrDefault();
        if (feature is null) return null;
        
        return iOSMaplibreFeatureResolver.ToMauiMaplibreFeature(feature);
    }

    public List<Models.Features.Feature> QuerySourceFeatures(string sourceId, string sourceLayer,
        string filterExpression)
    {
        var result = new List<Models.Features.Feature>();
        if (Style is null) return result;
        var source = Style.SourceWithIdentifier(sourceId);
        if (source is null) return result;
        
        Foundation.NSPredicate? predicate = BuildPredicate(filterExpression);
        
        if (source is MLNVectorTileSource vectorSource)
        {
            var layerSet = new Foundation.NSSet<Foundation.NSString>(new[] { new Foundation.NSString(sourceLayer) });
			var features = vectorSource.FeaturesInSourceLayersWithIdentifiers(layerSet, predicate);
            if (features is null) return result;
            foreach (var f in features)
                result.Add(iOSMaplibreFeatureResolver.ToMauiMaplibreFeature(f));
        }
        else if (source is MLNShapeSource shapeSource)
        {
			var features = shapeSource.FeaturesMatchingPredicate(predicate);
            if (features is null) return result;
            foreach (var f in features)
                result.Add(iOSMaplibreFeatureResolver.ToMauiMaplibreFeature(f));
        }
        
        return result;
    }

    public List<Models.Features.Feature> QuerySourceFeatures(string sourceId, string filterExpression)
    {
        var result = new List<Models.Features.Feature>();
        if (Style is null) return result;
        var source = Style.SourceWithIdentifier(sourceId);
        if (source is null) return result;
        
        Foundation.NSPredicate? predicate = BuildPredicate(filterExpression);
        
        if (source is MLNShapeSource shapeSource)
        {
			var features = shapeSource.FeaturesMatchingPredicate(predicate);
            if (features is null) return result;
            foreach (var f in features)
                result.Add(iOSMaplibreFeatureResolver.ToMauiMaplibreFeature(f));
        }
        else if (source is MLNVectorTileSource vectorSource)
        {
            // No layer specified; fetch from all vector layers is undefined. Return empty to avoid large scans.
            // Call the overload with sourceLayer for vector sources.
        }
        
        return result;
    }

    public List<string> QuerySourceFeaturesAsJson(string sourceId, string sourceLayer, string filterExpression)
    {
        var jsonList = new List<string>();
        if (Style is null) return jsonList;
        var source = Style.SourceWithIdentifier(sourceId);
        if (source is null) return jsonList;
        
        Foundation.NSPredicate? predicate = BuildPredicate(filterExpression);
        
        if (source is MLNVectorTileSource vectorSource)
        {
            var layerSet = new Foundation.NSSet<Foundation.NSString>(new[] { new Foundation.NSString(sourceLayer) });
			var features = vectorSource.FeaturesInSourceLayersWithIdentifiers(layerSet, predicate);
            if (features is null) return jsonList;
            foreach (var f in features)
            {
				var dict = f.GeoJSONDictionary;
                var data = Foundation.NSJsonSerialization.Serialize(dict, Foundation.NSJsonWritingOptions.PrettyPrinted, out _);
                var json = data is null ? "{}" : Foundation.NSString.FromData(data, Foundation.NSStringEncoding.UTF8)?.ToString() ?? "{}";
                jsonList.Add(json);
            }
        }
        else if (source is MLNShapeSource shapeSource)
        {
			var features = shapeSource.FeaturesMatchingPredicate(predicate);
            if (features is null) return jsonList;
            foreach (var f in features)
            {
				var dict = f.GeoJSONDictionary;
                var data = Foundation.NSJsonSerialization.Serialize(dict, Foundation.NSJsonWritingOptions.PrettyPrinted, out _);
                var json = data is null ? "{}" : Foundation.NSString.FromData(data, Foundation.NSStringEncoding.UTF8)?.ToString() ?? "{}";
                jsonList.Add(json);
            }
        }
        
        return jsonList;
    }

    public void SetFilter(string layerId, string filterExp)
    {
        // Not implemented on iOS minimal bridge; consider setting MLNVectorStyleLayer.predicate if needed.
    }

    public void SetProperty(string layerId, string property, object? val)
    {
        if (Style is null) return;
        var layer = Style.LayerWithIdentifier(layerId);
        if (layer is null) return;

        ApplyLayerProperty(layer, property, val);
    }

    public void SetZoomRange(string layerId, float min, float max)
    {
        // iOS layers expose min/max zoom via style-specific API; skipping minimal impl
    }

    public void FlyTo(float lng, float lat, float zoom)
    {
        if (MapView is null) return;
		// Animate center and zoom together to avoid canceling one another.
		var clampedZoom = Math.Max(0, Math.Min(22, (double)zoom));
		MapView.SetCenterCoordinate(new CoreLocation.CLLocationCoordinate2D(lat, lng), clampedZoom, true);
    }

    public void ResetBearing()
    {
        if (MapView is null) return;
        MapView.SetDirection(0, true);
    }

    public void CancelTransitions()
    {
        // No direct API to cancel; could reset animations if needed
    }

    public void ToggleActions(bool toggle)
    {
        if (MapView is null) return;
        MapView.RotateEnabled = toggle;
        MapView.ScrollEnabled = toggle;
        MapView.ZoomEnabled = toggle;
        MapView.PitchEnabled = toggle;
    }

    public void ToggleDoubleTapActions(bool toggle)
    {
        // Not exposed directly; leave as no-op
    }

    public void ToggleQuickZoomActions(bool toggle)
    {
        // Not exposed directly; leave as no-op
    }

    public void ToggleCompass(bool toggle)
    {
        if (MapView is null) return;
        MapView.CompassView.Hidden = !toggle;
    }

    public void ToggleDebugMode(bool toggle)
    {
        if (MapView is null) return;
        MapView.DebugMask = toggle
            ? MLNMapDebugMaskOptions.TileBoundariesMask | MLNMapDebugMaskOptions.OverdrawVisualizationMask
            : 0;
    }

    MLNSource? ConvertSource(Source source)
    {
        switch (source)
        {
            case VectorSource vectorSource:
            {
                var keys = new[]
                    { Constants.MLNTileSourceOptionMinimumZoomLevel, Constants.MLNTileSourceOptionMaximumZoomLevel };
                var vals = new NSObject[]
                    { NSNumber.FromNFloat(vectorSource.MinZoom), NSNumber.FromNFloat(vectorSource.MaxZoom) };
                var options = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(vals, keys);
                var src = new MLNVectorTileSource(vectorSource.Id, vectorSource.Tiles.ToArray(), options);
                return src;
            }
            case GeoJsonSource geoJsonSource:
            {
                MLNShape? shape = null;
                if (!string.IsNullOrWhiteSpace(geoJsonSource.GeoJson))
                {
                    var data = NSData.FromString(geoJsonSource.GeoJson, NSStringEncoding.UTF8);
                    NSError? err;
                    shape = MLNShape.ShapeWithData(data, (nuint)NSStringEncoding.UTF8, out err!);
                }

                var src = shape is null
                    ? new MLNShapeSource(geoJsonSource.Id, (MLNShape?)null, null)
                    : new MLNShapeSource(geoJsonSource.Id, shape, null);
                return src;
            }
        }

        return null;
    }

    MLNStyleLayer? ConvertLayer(Layer layer)
    {
        if (Style is null) return null;
        var source = Style.SourceWithIdentifier(layer.SourceId);
        if (source is null) return null;

        MLNStyleLayer? resLayer = null;
        switch (layer)
        {
            case FillLayer fill:
            {
                var l = new MLNFillStyleLayer(layer.Id, source);
                if (!string.IsNullOrWhiteSpace(fill.SourceLayer))
                    (l as MLNVectorStyleLayer).SourceLayerIdentifier = fill.SourceLayer;
                if (!string.IsNullOrWhiteSpace(layer.Filter) && l is MLNVectorStyleLayer vlFill)
                {
                    var predicate = PredicateFromJsonString(layer.Filter) ?? BuildPredicate(layer.Filter);
                    if (predicate is not null)
                        vlFill.Predicate = predicate;
                }
                resLayer = l;
                break;
            }
            case LineLayer line:
            {
                var l = new MLNLineStyleLayer(layer.Id, source);
                if (!string.IsNullOrWhiteSpace(line.SourceLayer))
                    (l as MLNVectorStyleLayer).SourceLayerIdentifier = line.SourceLayer;
                if (!string.IsNullOrWhiteSpace(layer.Filter) && l is MLNVectorStyleLayer vlLine)
                {
                    var predicate = PredicateFromJsonString(layer.Filter) ?? BuildPredicate(layer.Filter);
                    if (predicate is not null)
                        vlLine.Predicate = predicate;
                }
                resLayer = l;
                break;
            }
            case CircleLayer:
            {
                var l = new MLNCircleStyleLayer(layer.Id, source);
                if (!string.IsNullOrWhiteSpace(layer.Filter) && l is MLNVectorStyleLayer vlCircle)
                {
                    var predicate = PredicateFromJsonString(layer.Filter) ?? BuildPredicate(layer.Filter);
                    if (predicate is not null)
                        vlCircle.Predicate = predicate;
                }
                resLayer = l;
                break;
            }
        }

        if (resLayer is null) return null;
        foreach (var kv in layer.Properties)
        {
            ApplyLayerProperty(resLayer, kv.Key, kv.Value);
        }

        return resLayer;
    }

    void ApplyLayerProperty(MLNStyleLayer layer, string property, object? value)
    {
        switch (layer)
        {
            case MLNFillStyleLayer fill:
                ApplyFillProperty(fill, property, value);
                break;
            case MLNLineStyleLayer line:
                ApplyLineProperty(line, property, value);
                break;
            case MLNCircleStyleLayer circle:
                ApplyCircleProperty(circle, property, value);
                break;
        }
    }

    static UIColor ColorFromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return UIColor.Black;
        var trimmed = value.Trim();
        // Hex strings start with # or are 6/8 hex digits
        if (trimmed.StartsWith("#") || trimmed.Length == 6 || trimmed.Length == 8)
        {
            return ColorFromHex(trimmed);
        }

        // Named colors fallback
        switch (trimmed.ToLowerInvariant())
        {
            case "white": return UIColor.White;
            case "black": return UIColor.Black;
            case "red": return UIColor.Red;
            case "green": return UIColor.Green;
            case "blue": return UIColor.Blue;
            case "yellow": return UIColor.Yellow;
            case "gray":
            case "grey": return UIColor.Gray;
            case "clear":
            case "transparent": return UIColor.Clear;
            case "cyan": return UIColor.Cyan;
            case "magenta": return UIColor.Magenta;
            case "orange": return UIColor.Orange;
            case "purple": return UIColor.Purple;
            case "brown": return UIColor.Brown;
        }

        return UIColor.Black;
    }

    static UIColor ColorFromHex(string hex)
    {
        var c = hex.TrimStart('#');
        if (c.Length == 6)
        {
            var r = Convert.ToInt32(c.Substring(0, 2), 16);
            var g = Convert.ToInt32(c.Substring(2, 2), 16);
            var b = Convert.ToInt32(c.Substring(4, 2), 16);
            return UIColor.FromRGB(r, g, b);
        }

        if (c.Length == 8)
        {
            var a = Convert.ToInt32(c.Substring(0, 2), 16);
            var r = Convert.ToInt32(c.Substring(2, 2), 16);
            var g = Convert.ToInt32(c.Substring(4, 2), 16);
            var b = Convert.ToInt32(c.Substring(6, 2), 16);
            return UIColor.FromRGBA(r, g, b, a);
        }

        return UIColor.Black;
    }

    static NSExpression Expr(object o) => NSExpression.FromConstant((NSObject)o);

    void ApplyFillProperty(MLNFillStyleLayer layer, string property, object? value)
    {
        switch (property)
        {
            case Properties.FillColor when value is string s:
                layer.FillColor = Expr(ColorFromString(s));
                break;
            case Properties.FillOpacity when value is float f:
                layer.FillOpacity = Expr(NSNumber.FromNFloat(f));
                break;
			case Properties.FillOpacity when value is string s:
			{
				// Treat string as JSON expression like "['case', ...]" to mirror Android's Expression.Raw.
				var expr = ExprFromJsonString(s);
				if (expr is not null)
				{
					layer.FillOpacity = expr;
				}
				else if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
					layer.FillOpacity = Expr(NSNumber.FromDouble(d));
				break;
			}
        }
    }

    void ApplyLineProperty(MLNLineStyleLayer layer, string property, object? value)
    {
        switch (property)
        {
            case Properties.LineColor when value is string s:
                layer.LineColor = Expr(ColorFromString(s));
                break;
            case Properties.LineWidth when value is float f:
                layer.LineWidth = Expr(NSNumber.FromNFloat(f));
                break;
			case Properties.LineDasharray when value is string pattern:
			{
				// Accept expression JSON or constant array strings.
				var expr = ExprFromJsonString(pattern);
				if (expr is not null)
				{
					layer.LineDashPattern = expr;
				}
				else
				{
					// Try to parse JSON array "[2, 2]" or simple comma/space separated "2,2" into NSNumber[]
					var nums = ParseNumberArray(pattern);
					if (nums is not null)
						layer.LineDashPattern = Expr(nums);
				}
				break;
			}
        }
    }

    void ApplyCircleProperty(MLNCircleStyleLayer layer, string property, object? value)
    {
        switch (property)
        {
            case Properties.CircleColor when value is string s:
                layer.CircleColor = Expr(ColorFromString(s));
                break;
            case Properties.CircleRadius when value is float f:
                layer.CircleRadius = Expr(NSNumber.FromNFloat(f));
                break;
			case Properties.CircleRadius when value is string rs:
			{
				// Treat string as JSON expression; fallback to numeric constant.
				var expr = ExprFromJsonString(rs);
				if (expr is not null)
				{
					layer.CircleRadius = expr;
				}
				else if (double.TryParse(rs, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
					layer.CircleRadius = Expr(NSNumber.FromDouble(d));
				break;
			}
            case Properties.CircleStrokeWidth when value is float w:
                layer.CircleStrokeWidth = Expr(NSNumber.FromNFloat(w));
                break;
            case Properties.CircleStrokeColor when value is string sc:
                layer.CircleStrokeColor = Expr(ColorFromString(sc));
                break;
        }
    }
	
	static NSArray? ParseNumberArray(string input)
	{
		try
		{
			// Try JSON first
			var trimmed = input.Trim();
			if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
			{
				var data = NSData.FromString(trimmed, NSStringEncoding.UTF8);
				var obj = NSJsonSerialization.Deserialize(data, (NSJsonReadingOptions)0, out _);
				if (obj is NSArray arrJson)
				{
					var list = new List<NSNumber>();
					foreach (var o in arrJson)
					{
						if (o is NSNumber n) list.Add(n);
						else if (o is NSString ss && double.TryParse(ss.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var dv))
							list.Add(NSNumber.FromDouble(dv));
					}
					return list.Count > 0 ? NSArray.FromNSObjects(list.Cast<NSObject>().ToArray()) : null;
				}
			}
			// Fallback: comma/space separated
			var parts = input.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
			var nums = new List<NSNumber>();
			foreach (var p in parts)
			{
				if (double.TryParse(p, NumberStyles.Any, CultureInfo.InvariantCulture, out var dv))
					nums.Add(NSNumber.FromDouble(dv));
			}
			return nums.Count > 0 ? NSArray.FromNSObjects(nums.Cast<NSObject>().ToArray()) : null;
		}
		catch
		{
			return null;
		}
	}
	
	static NSExpression? ExprFromJsonString(string input)
	{
		try
		{
			var trimmed = input?.Trim();
			if (string.IsNullOrEmpty(trimmed)) return null;
			if (!(trimmed.StartsWith("[") && trimmed.EndsWith("]"))) return null;
			// Normalize single quotes to double quotes to form valid JSON
			var normalized = trimmed.Replace("'", "\"");
			var data = NSData.FromString(normalized, NSStringEncoding.UTF8);
			if (data is null) return null;
			var obj = NSJsonSerialization.Deserialize(data, (NSJsonReadingOptions)0, out _);
			if (obj is null) return null;
			// Invoke generated category method via reflection to avoid compile-time dependency.
			var expr = CreateExpressionFromJsonObject(obj);
			return expr;
		}
		catch
		{
			return null;
		}
	}

	static NSExpression? CreateExpressionFromJsonObject(NSObject obj)
	{
		try
		{
			var cls = ObjCRuntime.Class.GetHandle("NSExpression");
			var sel = ObjCRuntime.Selector.GetHandle("expressionWithMLNJSONObject:");
			if (cls != IntPtr.Zero && sel != IntPtr.Zero)
			{
				var h = IntPtr_objc_msgSend_IntPtr(cls, sel, obj.Handle);
				if (h != IntPtr.Zero)
					return ObjCRuntime.Runtime.GetNSObject<NSExpression>(h);
			}
			return null;
		}
		catch
		{
			return null;
		}
	}
	
	static NSPredicate? PredicateFromJsonString(string input)
	{
		try
		{
			var trimmed = input?.Trim();
			if (string.IsNullOrEmpty(trimmed)) return null;
			if (!(trimmed.StartsWith("[") && trimmed.EndsWith("]"))) return null;
			var normalized = trimmed.Replace("'", "\"");
			var data = NSData.FromString(normalized, NSStringEncoding.UTF8);
			if (data is null) return null;
			var obj = NSJsonSerialization.Deserialize(data, (NSJsonReadingOptions)0, out _);
			if (obj is null) return null;
			var cls = ObjCRuntime.Class.GetHandle("NSPredicate");
			var sel = ObjCRuntime.Selector.GetHandle("predicateWithMLNJSONObject:");
			if (cls != IntPtr.Zero && sel != IntPtr.Zero)
			{
				var h = IntPtr_objc_msgSend_IntPtr(cls, sel, obj.Handle);
				if (h != IntPtr.Zero)
					return ObjCRuntime.Runtime.GetNSObject<NSPredicate>(h);
			}
			return null;
		}
		catch
		{
			return null;
		}
	}
	
	[DllImport(ObjCRuntime.Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
	static extern IntPtr IntPtr_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);
    
    static Foundation.NSPredicate? BuildPredicate(string? filterExpression)
    {
        if (string.IsNullOrWhiteSpace(filterExpression)) return null;
        try
        {
            // Support a minimal subset like: ['==', '$id', 123] or ['==', 'prop', 'value'] or ['==', ['get','prop'], true]
            var expr = filterExpression.Trim();
            string? op = null;
            string? prop = null;
            string? valueRaw = null;
            
            // Normalize quotes
            expr = expr.Replace("\"", "'");
            if (expr.StartsWith("['==',"))
            {
                var inner = expr.Substring("['==',".Length).Trim();
                // split by first comma
                var commaIdx = inner.IndexOf(',');
                if (commaIdx > 0)
                {
                    var left = inner.Substring(0, commaIdx).Trim();
                    var right = inner.Substring(commaIdx + 1).Trim().TrimEnd(']').Trim();
                    
                    // left could be '$id' or "['get','prop']" or 'prop'
                    if (left.StartsWith("['get',"))
                    {
                        var pInner = left.Substring("['get',".Length).Trim().TrimEnd(']').Trim().Trim('\'');
                        prop = pInner;
                    }
                    else
                    {
                        prop = left.Trim('\'', ' ');
                    }
                    
                    valueRaw = right.Trim();
                }
                op = "==";
            }
            
            if (op == "==" && !string.IsNullOrEmpty(prop) && valueRaw is not null)
            {
                // Identifier handling
                if (prop == "$id")
                {
                    // We cannot predicate on identifier reliably; return null and let caller post-filter if needed.
                    return null;
                }
                
                // Parse value
                object? valObj = null;
                if (valueRaw.Equals("true", StringComparison.OrdinalIgnoreCase) || valueRaw.Equals("false", StringComparison.OrdinalIgnoreCase))
                {
                    valObj = valueRaw.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                else if (valueRaw.StartsWith("'") && valueRaw.EndsWith("'"))
                {
                    valObj = valueRaw.Trim('\'');
                }
                else if (double.TryParse(valueRaw, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var dv))
                {
                    valObj = dv;
                }
                
                if (valObj is string s)
                    return Foundation.NSPredicate.FromFormat("%K == %@", new Foundation.NSObject[] { new Foundation.NSString(prop), new Foundation.NSString(s) });
                if (valObj is bool b)
                    return Foundation.NSPredicate.FromFormat("%K == %@", new Foundation.NSObject[] { new Foundation.NSString(prop), Foundation.NSNumber.FromBoolean(b) });
                if (valObj is double d)
                    return Foundation.NSPredicate.FromFormat("%K == %@", new Foundation.NSObject[] { new Foundation.NSString(prop), Foundation.NSNumber.FromDouble(d) });
            }
        }
        catch
        {
            // ignore parsing errors -> fallback to no predicate
        }
        return null;
    }
}