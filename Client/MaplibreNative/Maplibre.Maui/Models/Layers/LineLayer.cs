namespace MaplibreMaui.Models.Layers;

public class LineLayer : Layer
{
    public string SourceLayer { get; set; } = string.Empty;

    public LineLayer(string id, string sourceId) : base(id, sourceId)
    {
    }
}