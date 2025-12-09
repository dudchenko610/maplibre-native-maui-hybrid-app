#if IOS
using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using WebKit;

namespace FarmApp.Mobile;

[Register("FarmAppUIApplication")]
public class FarmAppUIApplication : UIApplication
{
    public override void SendEvent(UIEvent uievent)
    {
        var touches = uievent.AllTouches;
        if (touches is null)
        {
            base.SendEvent(uievent);
            return;
        }

        var anyTouch = uievent.AllTouches?.AnyObject as UITouch;
        if (anyTouch is null)
        {
            base.SendEvent(uievent);
            return;
        }

        // Get screen coordinates
        var touchView = anyTouch.View;
        var localPoint = anyTouch.LocationInView(touchView);
        var screenPoint = touchView is not null
            ? touchView.ConvertPointToView(localPoint, null)
            : anyTouch.LocationInView(null);
        
        var x = (float)screenPoint.X;
        var y = (float)screenPoint.Y;

        var _ = SystemEventsProvider.InvokeTouch(uievent);

        base.SendEvent(uievent);
    }
}

#endif