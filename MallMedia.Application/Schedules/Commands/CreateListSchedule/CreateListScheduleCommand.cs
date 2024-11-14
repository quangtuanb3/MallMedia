using MediatR;

namespace MallMedia.Application.Schedules.Commands.CreateListSchedule;

public class CreateListScheduleCommand : IRequest<List<int>>
{
    public List<CreateSchedule> CreateSchedules { get; set; } = new();
}
