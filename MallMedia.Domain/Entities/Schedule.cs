using MallMedia.Domain.Entities;

public class Schedule
{

    public int Id { get; set; }

    public int DeviceId { get; set; }
    public Device Device { get; set; }
    public int ContentId { get; set; }
    public Content Content { get; set; }
    public bool IsDefault { get; set; } = false;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public ICollection<TimeFrame> TimeFrames { get; set; } = new List<TimeFrame>();
    public string Status { get; set; }
}

