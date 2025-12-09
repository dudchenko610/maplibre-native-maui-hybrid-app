#if IOS
using CoreGraphics;
using FarmApp.Mobile.Controls;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace FarmApp.Mobile;

public class HitRoutingAbsoluteLayoutHandler : LayoutHandler
{
    protected override LayoutView CreatePlatformView()
    {
        return new HitRoutingUIView(this);
    }
}

class HitRoutingUIView : LayoutView
{
    readonly HitRoutingAbsoluteLayoutHandler _handler;

    public HitRoutingUIView(HitRoutingAbsoluteLayoutHandler handler)
    {
        _handler = handler;
    }

    HitRoutingAbsoluteLayout? VirtualLayout => _handler.VirtualView as HitRoutingAbsoluteLayout;

    public override UIView? HitTest(CGPoint point, UIEvent? uievent)
    {
        if (!UserInteractionEnabled || Hidden || Alpha <= 0.01 || uievent is null) return null;

        var layout = VirtualLayout;
        var blazor = layout?.BlazorView?.Handler?.PlatformView as UIView;
        var map = layout?.MapView?.Handler?.PlatformView as UIView;
        
        if (layout is null || map is null || blazor is null) return base.HitTest(point, uievent);

        var pointInView = blazor.ConvertPointFromView(point, this);
        var onBlazor = SystemEventsProvider.InvokeHitTestTouch(pointInView);
        var view = onBlazor ? blazor : map;
        
        return view.HitTest(pointInView, uievent);
    }
}
#endif
