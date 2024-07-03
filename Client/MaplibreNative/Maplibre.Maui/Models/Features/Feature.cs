namespace MaplibreMaui.Models.Features;

public class Feature
{
    public string? Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public List<object> Coordinates { get; set; } = new();
    public Dictionary<string, object?> Properties { get; set; } = new();
}