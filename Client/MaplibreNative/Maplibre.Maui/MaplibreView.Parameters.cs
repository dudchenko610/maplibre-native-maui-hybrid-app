using System;
using Microsoft.Maui.Controls;

namespace MaplibreMaui;

partial class MaplibreView
{
    public static readonly BindableProperty HandlerChangedProperty = 
        BindableProperty.Create(nameof(HandlerChanged), typeof(Action), typeof(MaplibreView));
    
    public static readonly BindableProperty StyleUrlProperty =
        BindableProperty.Create(nameof(StyleUrl), typeof(string), typeof(MaplibreView), default(string));

    public static readonly BindableProperty StartLongitudeProperty =
        BindableProperty.Create(nameof(StartLongitude), typeof(float), typeof(MaplibreView), 0f);

    public static readonly BindableProperty StartLatitudeProperty =
        BindableProperty.Create(nameof(StartLatitude), typeof(float), typeof(MaplibreView), 0f);

    public static readonly BindableProperty StartZoomProperty =
        BindableProperty.Create(nameof(StartZoom), typeof(float), typeof(MaplibreView), 0f);

    public Action HandlerChanged
    {
        get => (Action)GetValue(HandlerChangedProperty);
        set => SetValue(HandlerChangedProperty, value);
    }
    
    public string StyleUrl
    {
        get => (string)GetValue(StyleUrlProperty);
        set => SetValue(StyleUrlProperty, value);
    }

    public float StartLongitude
    {
        get => (float)GetValue(StartLongitudeProperty);
        set => SetValue(StartLongitudeProperty, value);
    }

    public float StartLatitude
    {
        get => (float)GetValue(StartLatitudeProperty);
        set => SetValue(StartLatitudeProperty, value);
    }

    public float StartZoom
    {
        get => (float)GetValue(StartZoomProperty);
        set => SetValue(StartZoomProperty, value);
    }
}