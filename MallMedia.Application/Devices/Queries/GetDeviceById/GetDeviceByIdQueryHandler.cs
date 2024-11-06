using MallMedia.Application.Devices.Command.GetDeviceById;
using MallMedia.Domain.Repositories;
using MediatR;
using MallMedia.Application.Devices.Queries.GetDeviceById;

namespace MallMedia.Application.Devices.Queries.GetDeviceById
{
    public class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, DeviceDto>
    {
        private readonly IDevicesRepository _deviceRepository;

        public GetDeviceByIdQueryHandler(IDevicesRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<DeviceDto> Handle(GetDeviceByIdQuery query, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(query.DeviceId);

            if (device == null) return null;

            return new DeviceDto
            {
                Id = device.Id,
                DeviceName = device.DeviceName,
                DeviceType = device.DeviceType,
                LocationName = device.Location?.Name,
                Resolution = device.Configuration?.Resolution,
                Size = device.Configuration?.Size,
                Status = device.Status,
                CreatedAt = device.CreatedAt,
                UpdatedAt = device.UpdatedAt
            };
        }
    }
}
