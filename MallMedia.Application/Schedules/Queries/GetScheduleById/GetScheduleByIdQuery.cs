using MallMedia.Application.Schedules.Dto;
using MediatR;

namespace MallMedia.Application.Schedules.Queries.GetScheduleById
{
    public class GetScheduleByIdQuery(int id) : IRequest<SchedulesDto>
    {
        public int Id { get; set; } = id;
    }
}
