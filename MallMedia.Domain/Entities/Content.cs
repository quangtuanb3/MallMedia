namespace MallMedia.Domain.Entities;

public class Content
{
    public bool isDeFault { get; set; }

    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ContentType { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public ICollection<Media> Media { get; set; }

    public string Status { get; set; } = "UNUSED";

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public ICollection<DeviceSchedule> DeviceSchedules { get; set; }
    public int DeviceId { get; set; }
    public bool IsUpdated { get; set; }
    public object UpdateDate { get; set; }

    public bool isDefault { get; set; }
    public DateTime Schedule { get; set; }
}
