namespace MaplibreMaui;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseMaplibre(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(collection =>
        {
            collection.AddHandler(
                typeof(MaplibreView),
                typeof(MaplibreViewHandler)
            );
        });

        return builder;
    }
}