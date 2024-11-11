//using AutoMapper;
//using MallMedia.Application.Devices.Queries.GetByIdDevices;
//using MallMedia.Application.Schedules.Dto;
//using MallMedia.Domain.Exceptions;
//using MallMedia.Domain.Repositories;
//using MediatR;
//using Microsoft.Extensions.Logging;
//using System.Threading;
//using System.Threading.Tasks;

//namespace MallMedia.Application.Devices.Command.GetDeviceById
//{
//    public class GetDeviceByIdQueryHandler(ILogger<GetDeviceByIdQueryHandler> logger, IDevicesRepository devicesRepository, IMapper mapper) : IRequestHandler<GetDevicesByIdQuery, DeviceDto>
//    {
//        public async Task<DeviceDto> Handle(GetDevicesByIdQuery request, CancellationToken cancellationToken)
//        {
//            logger.LogInformation("Getting device by id with {@devicesId}", request.Id);
//            var device = await devicesRepository.GetByIdAsync(request.Id)
//                ?? throw new NotFoundException("Devices", request.Id.ToString());
//            var deviceDto = mapper.Map<DeviceDto>(device);
//            deviceDto.SchedulesDtos = mapper.Map<List<SchedulesDto>>(device.Schedules);
//            return deviceDto;
//        }
//    }
//}