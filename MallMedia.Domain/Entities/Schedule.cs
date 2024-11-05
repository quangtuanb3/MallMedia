using MallMedia.Domain.Entities;

public class Schedule
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public Device Device { get; set; }
    public int ContentId { get; set; }
    public Content Content { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public ICollection<TimeFrame> TimeFrames { get; set; } = new List<TimeFrame>();
<<<<<<< HEAD

=======
>>>>>>> 601004681c5e2b77cb698c34b98b29639a597e71
    public string Status { get; set; }
}

