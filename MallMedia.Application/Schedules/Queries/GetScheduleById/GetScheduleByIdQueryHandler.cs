using AutoMapper;
using MallMedia.Application.Contents.Dtos;
using MallMedia.Application.Devices.Dto;
using MallMedia.Application.Schedules.Dto;
using MallMedia.Domain.Exceptions;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Schedules.Queries.GetScheduleById
{
    public class GetScheduleByIdQueryHandler(ILogger<GetScheduleByIdQueryHandler> logger,IMapper mapper,IScheduleRepository scheduleRepository) : IRequestHandler<GetScheduleByIdQuery, SchedulesDto>
    {
        public async Task<SchedulesDto> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting schedule with id : {@Scheduleid}", request.Id);

            var schedule = await scheduleRepository.GetByIdAsync(request.Id)
                 ?? throw new NotFoundException("Schedule", request.Id.ToString());

            var scheuldeDto = mapper.Map<SchedulesDto>(schedule);
            scheuldeDto.Contentdto = mapper.Map<ContentDto>(schedule.Content);
            scheuldeDto.Devicedto = mapper.Map<DeviceDto>(schedule.Device);
            return scheuldeDto;
        }
    }
}
