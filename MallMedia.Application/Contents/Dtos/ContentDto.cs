using MallMedia.Domain.Entities;

namespace MallMedia.Application.Contents.Dtos;

public class ContentDto
{
    public int Id { get; set; }

    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string ContentType { get; set; } = default!;
    public Category Category { get; set; } = new Category();

    //public ICollection<Media> Media { get; set; }
    public List<string> MediaUrl { get; set; } = new List<string>();

    public string Status { get; set; } = default!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = default!;

    public DateTime? UpdatedAt { get; set; }
}


