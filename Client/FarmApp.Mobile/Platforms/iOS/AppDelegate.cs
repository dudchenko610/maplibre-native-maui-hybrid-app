using Foundation;
using Microsoft.Maui;
using UIKit;

namespace FarmApp.Mobile;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    
    public override void OnActivated(UIApplication uiApplication)
    {
        base.OnActivated(uiApplication);
        UpdateScreenOffsets(uiApplication);
    }
    
    UIWindow? GetActiveWindow(UIApplication app)
    {
        UIWindow? keyWindow = null;
        try
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                var scene = app.ConnectedScenes?
                                .OfType<UIWindowScene>()
                                .FirstOrDefault(s => s.ActivationState == UISceneActivationState.ForegroundActive)
                            ?? app.ConnectedScenes?.OfType<UIWindowScene>().FirstOrDefault();

                keyWindow = scene?.Windows?.FirstOrDefault(w => w.IsKeyWindow)
                            ?? scene?.Windows?.FirstOrDefault();
            }
            keyWindow ??= app.KeyWindow ?? app.Windows?.FirstOrDefault();
        }
        catch
        {
            // ignore
        }
        return keyWindow;
    }

    void UpdateScreenOffsets(UIApplication app)
    {
        try
        {
            var keyWindow = GetActiveWindow(app);
            var safe = keyWindow?.SafeAreaInsets ?? new UIEdgeInsets(0, 0, 0, 0);

            // Work in points to keep consistency with our touch coordinates (also in points)
            var scale = (float)UIScreen.MainScreen.Scale;
            var bounds = UIScreen.MainScreen.Bounds; // points

            // Use points for ScreenWidth/Height and set Density=1 so cssWidth = ScreenWidth / Density = points
            ScreenOffsetProvider.Top = (int)safe.Top;
            ScreenOffsetProvider.Bottom = (int)safe.Bottom;
            ScreenOffsetProvider.Left = (int)safe.Left;
            ScreenOffsetProvider.Right = (int)safe.Right;
            ScreenOffsetProvider.Density = 1f;
            ScreenOffsetProvider.ScreenWidth = (int)(bounds.Width - safe.Left - safe.Right);
            ScreenOffsetProvider.ScreenHeight = (int)(bounds.Height - safe.Top - safe.Bottom);
        }
        catch
        {
            // Leave as-is on failure
        }
    }
}