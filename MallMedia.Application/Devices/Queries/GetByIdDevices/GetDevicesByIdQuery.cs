using MallMedia.Application.Devices.Dto;
using MediatR;

namespace MallMedia.Application.Devices.Queries.GetByIdDevices
{
    public class GetDevicesByIdQuery(int devicesId) : IRequest<DeviceDto>
    {
        public int Id { get;} = devicesId;
        public int DeviceId { get; set; }
    }
}
