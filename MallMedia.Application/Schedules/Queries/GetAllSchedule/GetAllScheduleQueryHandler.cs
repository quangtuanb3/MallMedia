using AutoMapper;
using MallMedia.Application.Common;
using MallMedia.Application.Devices.Dto;
using MallMedia.Application.Schedules.Dto;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Schedules.Queries.GetAllSchedule
{
    public class GetAllScheduleQueryHandler(ILogger<GetAllScheduleQueryHandler>logger, IScheduleRepository scheduleRepository,IMapper mapper) : IRequestHandler<GetAllScheduleQuery, PagedResult<SchedulesDto>>

    {
        public async Task<PagedResult<SchedulesDto>> Handle(GetAllScheduleQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all schedule");
            var (schedule, totalCount) = await scheduleRepository
                                                .GetAllMatchingAsync(
                                                    request.PageSize,
                                                    request.PageNumber,
                                                    request.SortBy,
                                                    request.SortDirection);
            var scheduleDto = mapper.Map<List<SchedulesDto>>(schedule);
            return new PagedResult<SchedulesDto>(scheduleDto, totalCount, request.PageSize, request.PageNumber);
        }
    }
}
