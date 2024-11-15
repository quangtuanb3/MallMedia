namespace MallMedia.Application.Schedules.Commands.CreateListSchedule
{
    public class CreateSchedule
    {
        public int ContentId { get; set; }
        public int DeviceId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; } = "SCHEDULED";
    }
}
