using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using FarmApp.Mobile.Controls;
using MaplibreMaui;

namespace FarmApp.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
                .UseMauiApp<App>()
                .UseMaplibre()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

#if IOS
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler<HitRoutingAbsoluteLayout, HitRoutingAbsoluteLayoutHandler>();
        });
#endif
        
        var services = builder.Services;

        services.AddTransient<MainPage>();
        services.AddMauiBlazorWebView();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}