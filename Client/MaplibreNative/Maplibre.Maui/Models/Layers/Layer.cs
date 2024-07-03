namespace MaplibreMaui.Models.Layers;

public class Layer
{
    public string Id { get; set; }
    public string SourceId { get; set; }
    public Dictionary<string, object?> Properties { get; set; } = new();
    public string? Filter { get; set; }

    public Layer(string id, string sourceId)
    {
        Id = id;
        SourceId = sourceId;
    }
}