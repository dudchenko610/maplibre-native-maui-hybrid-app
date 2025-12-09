using Microsoft.AspNetCore.Components.WebView.Maui;
#if IOS
using UIKit;
using WebKit;
#endif

namespace FarmApp.Mobile.Controls;

public class CustomBlazorWebView : BlazorWebView
{
#if IOS
    WKWebView? _wkWebView;
#endif
    public CustomBlazorWebView()
    {
        this.BlazorWebViewInitialized += CustomWebView_BlazorWebViewInitialized;
    }

    private void CustomWebView_BlazorWebViewInitialized(object? sender, Microsoft.AspNetCore.Components.WebView.BlazorWebViewInitializedEventArgs e)
    {
#if IOS
            var webView = (WKWebView)e.WebView;
            _wkWebView = webView;
            webView.Opaque = false;
            webView.BackgroundColor = UIColor.Clear;
            
#endif
    }
}