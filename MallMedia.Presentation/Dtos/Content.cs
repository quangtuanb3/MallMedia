using MallMedia.Domain.Entities;

namespace MallMedia.Presentation.Dtos;

public class Content
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContentType { get; set; }
    public Category Category { get; set; }
    public List<Media> Media { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
