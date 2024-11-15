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
            var listSchedule = new List<SchedulesDto>();
            foreach(var item in schedule) 
            {
                var scheduleDto = mapper.Map<SchedulesDto>(item);
                scheduleDto.Devicedto = mapper.Map<DeviceDto>(item.Device);
                listSchedule.Add(scheduleDto);
            }
            

            return new PagedResult<SchedulesDto>(listSchedule, totalCount, request.PageSize, request.PageNumber);
        }
    }
}
