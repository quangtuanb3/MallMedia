namespace MallMedia.Presentation.Dtos;

public class Media
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string Type { get; set; }
    public string Path { get; set; }
    public int Size { get; set; }
    public string? Duration { get; set; }
    public string Resolution { get; set; }
    public int ContentId { get; set; }
}