
using System.ComponentModel.DataAnnotations;


namespace MallMedia.Domain.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public ICollection<Content> Contents { get; set; } = new List<Content>();

}
