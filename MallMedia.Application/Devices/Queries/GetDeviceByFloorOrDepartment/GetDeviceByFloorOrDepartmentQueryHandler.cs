using AutoMapper;
using MallMedia.Application.Devices.Dto;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Devices.Queries.GetDeviceByFloorOrDepartment
{
    public class GetDeviceByFloorOrDepartmentQueryHandler(ILogger<GetDeviceByFloorOrDepartmentQueryHandler> logger, IDevicesRepository devicesRepository, IMapper mapper) : IRequestHandler<GetDeviceByFloorOrDepartmentQuery, List<DeviceDto>>
    {
        public async Task<List<DeviceDto>> Handle(GetDeviceByFloorOrDepartmentQuery request, CancellationToken cancellationToken)
        {
            var devices = await devicesRepository.GetByTypeAndFloorOrDepartmant(request.DeviceType, request.Floor, request.Department);
            var result = mapper.Map<List<DeviceDto>>(devices);
            return result;
        }
    }
}
