
namespace MallMedia.Domain.Constants;

public class UploadFileModel
{
    public string? FileName { get; set; }

    public string? Type { get; set; }

    public string? Path { get; set; }

    public int? Size { get; set; }

    public TimeSpan? Duration { get; set; }

    public string? Resolution { get; set; }
}
