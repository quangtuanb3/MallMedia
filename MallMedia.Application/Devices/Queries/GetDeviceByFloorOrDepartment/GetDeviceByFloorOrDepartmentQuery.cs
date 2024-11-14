using MallMedia.Application.Devices.Dto;
using MediatR;

namespace MallMedia.Application.Devices.Queries.GetDeviceByFloorOrDepartment
{
    public class GetDeviceByFloorOrDepartmentQuery : IRequest<List<DeviceDto>>
    {
        public List<string> DeviceType { get; set; }
        public List<int>? Floor { get; set; }
        public List<string>? Department { get; set; }
    }
}
