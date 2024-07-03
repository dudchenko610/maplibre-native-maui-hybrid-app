namespace MaplibreMaui.Models.Sources;

public class GeoJsonSource : Source
{
    public string? GeoJson { get; set; }
    
    public GeoJsonSource(string id) : base(id)
    {
    }
}