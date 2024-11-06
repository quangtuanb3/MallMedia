using MallMedia.Domain.Entities;

namespace MallMedia.Application.Contents.Dtos;

public class ContentDto
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string ContentType { get; set; } = default!;
    public Category Category { get; set; } = default!;
    public List<Media> Media { get; set; } = new();

    public string Status { get; set; } = default!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = default!;

    public DateTime? UpdatedAt { get; set; }
}


