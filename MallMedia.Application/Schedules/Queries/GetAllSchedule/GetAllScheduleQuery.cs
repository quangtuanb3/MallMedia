using MallMedia.Application.Common;
using MallMedia.Application.Schedules.Dto;
using MallMedia.Domain.Constants;
using MediatR;

namespace MallMedia.Application.Schedules.Queries.GetAllSchedule
{
    public class GetAllScheduleQuery : IRequest<PagedResult<SchedulesDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
