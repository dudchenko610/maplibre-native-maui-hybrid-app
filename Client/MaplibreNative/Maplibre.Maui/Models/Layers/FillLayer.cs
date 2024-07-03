namespace MaplibreMaui.Models.Layers;

public class FillLayer : Layer
{
    public string SourceLayer { get; set; } = string.Empty;

    public FillLayer(string id, string sourceId) : base(id, sourceId)
    {
    }
}