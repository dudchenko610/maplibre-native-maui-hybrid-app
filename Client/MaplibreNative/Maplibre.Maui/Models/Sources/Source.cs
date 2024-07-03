namespace MaplibreMaui.Models.Sources;

public abstract class Source
{
    public string Id { get; set; }

    public Source(string id)
    {
        Id = id;
    }
}