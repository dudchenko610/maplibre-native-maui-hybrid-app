using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Core.View;
using AndroidReal = Android;
using View = Android.Views.View;

namespace FarmApp.Mobile;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity, IOnApplyWindowInsetsListener
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        // WebViewSoftInputPatch.Initialize();

        // Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits,  Android.Views.WindowManagerFlags.LayoutNoLimits);
        // Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
        // Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
        
        RequestedOrientation = ScreenOrientation.Portrait;
        WindowCompat.SetDecorFitsSystemWindows(Window, false);
        ViewCompat.SetOnApplyWindowInsetsListener(Window.DecorView, this);
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent? intent)
    {
        base.OnActivityResult(requestCode, resultCode, intent);

        if (requestCode == 999)
        {
            MessagingCenter.Send(this, resultCode == Result.Ok ? "AuthorizationComplete" : "AuthorizationCancelled", intent);
        }
    }

    public WindowInsetsCompat OnApplyWindowInsets(View view, WindowInsetsCompat windowInsets)
    {
        var systemInsets = windowInsets.GetInsets(WindowInsetsCompat.Type.SystemGestures());
        var statusBarInsets = windowInsets.GetInsets(WindowInsetsCompat.Type.StatusBars());
        
        // Apply the insets as padding to the view. Here, set all the dimensions
        // as appropriate to your layout. You can also update the view's margin if
        // more appropriate.
        view.SetPadding(0, 0, 0, 0);

        Console.WriteLine($"Left: {systemInsets.Left}, Top: {statusBarInsets.Top}, Right: {systemInsets.Right}, Bottom: {systemInsets.Bottom}");

        var density = Resources?.DisplayMetrics?.Density ?? 1;
        
        ScreenOffsetProvider.Top = (int) (statusBarInsets.Top / density);
        ScreenOffsetProvider.Bottom = (int) (systemInsets.Bottom / density);
        ScreenOffsetProvider.Left = systemInsets.Left;
        ScreenOffsetProvider.Right = systemInsets.Right;
        ScreenOffsetProvider.Density = density;
        ScreenOffsetProvider.ScreenWidth = Resources?.DisplayMetrics?.WidthPixels ?? 0;
        ScreenOffsetProvider.ScreenHeight = (Resources?.DisplayMetrics?.HeightPixels ?? 0) + statusBarInsets.Top + systemInsets.Bottom;
        
        Console.WriteLine($"DENSITY: {density}");

        // if (Resources?.DisplayMetrics != null)
        // {
        //     var topPx = (int) (insets.Top * Resources.DisplayMetrics.Density);
        //     Console.WriteLine($"TOP PX {topPx}");
        //     Console.WriteLine($"TOP PX {topPx}");
        // }
        
        // Return CONSUMED if you don't want the window insets to keep passing down
        // to descendant views.
        return windowInsets;
    }

    public override bool DispatchTouchEvent(MotionEvent? ev) {
        var continueProcessing = ev is not null && SystemEventsProvider.InvokeTouch(ev);
        
        if (continueProcessing)
            base.DispatchTouchEvent(ev);
        
        if (ev is not null && ev.ActionMasked == MotionEventActions.Up)
            SystemEventsProvider.InvokeLastTouchLifted();

        return continueProcessing;
    }
}