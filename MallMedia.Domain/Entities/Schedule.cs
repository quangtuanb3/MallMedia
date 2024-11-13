using MallMedia.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class Schedule
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public Device Device { get; set; }
    public int ContentId { get; set; }
    public Content Content { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = "SCHEDULED";
    public string TimeFrames { get; set; }
    public object? Title { get; set; }
}

