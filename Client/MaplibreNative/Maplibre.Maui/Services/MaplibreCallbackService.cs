namespace MaplibreMaui.Services;

public class MaplibreCallbackService
{
    public event Action? OnMapReady;
    public event Action? OnStyleLoaded;
    public event Action? OnMapMoveStart;
    public event Action? OnMapMoveEnd;
    public event Action? OnCameraIdle;
    public event Action? OnMapRotate;

    internal void InvokeMapReady() => OnMapReady?.Invoke();
    internal void InvokeStyleLoaded() => OnStyleLoaded?.Invoke();
    internal void InvokeMapMoveStart() => OnMapMoveStart?.Invoke();
    internal void InvokeMapMoveEnd() => OnMapMoveEnd?.Invoke();
    internal void InvokeCameraIdle() => OnCameraIdle?.Invoke();
    internal void InvokeMapRotate() => OnMapRotate?.Invoke();
}