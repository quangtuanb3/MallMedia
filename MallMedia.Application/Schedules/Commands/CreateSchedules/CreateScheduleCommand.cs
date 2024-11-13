using MediatR;

namespace MallMedia.Application.Schedules.Commands.CreateSchedules;

public class CreateScheduleCommand : IRequest<int>
{
    public int ContentId { get; set; }
    public int DeviceId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Status { get; set; } = "SCHEDULED";
}
