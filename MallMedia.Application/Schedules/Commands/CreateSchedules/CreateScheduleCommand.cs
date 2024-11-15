using MediatR;

namespace MallMedia.Application.Schedules.Commands.CreateSchedules;

public class CreateScheduleCommand : IRequest<List<int>>
{
    public int ContentId { get; set; }
    public List<string> DeviceType { get; set; }
    public List<int>? Floors { get; set; }
    public List<string>? Departments { get; set; } 
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Status { get; set; } = "SCHEDULED";
}



