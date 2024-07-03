namespace MaplibreMaui.Models.Sources;

public class VectorSource : Source
{
    public List<string> Tiles { get; set; } = new();
    public float MinZoom { get; set; }
    public float MaxZoom { get; set; }
    
    public VectorSource(string id) : base(id)
    {
        
    }
}