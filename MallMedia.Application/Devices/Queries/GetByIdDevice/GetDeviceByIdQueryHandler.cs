using AutoMapper;
using MallMedia.Application.Devices.Dto;
using MallMedia.Application.Schedules.Dto;
using MallMedia.Domain.Exceptions;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Devices.Queries.GetByIdDevices
{
    public class GetDeviceByIdQueryHandler(ILogger<GetDevicesByIdQueryHandler> logger, IDevicesRepository devicesRepository, IMapper mapper) : IRequestHandler<GetDevicesByIdQuery, DeviceDto>
    {
        public async Task<DeviceDto> Handle(GetDevicesByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting device by id with {@devicesId}", request.Id);
            var device = await devicesRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Devices", request.Id.ToString());
            var deviceDto = mapper.Map<DeviceDto>(device);
            deviceDto.SchedulesDtos = mapper.Map<List<SchedulesDto>>(device.Schedules);
            return deviceDto;
        }
    }
}