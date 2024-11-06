using MallMedia.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Queries.GetDeviceById
{
    public class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, DeviceDto>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetDeviceByIdQueryHandler(IDeviceRepository deviceRepository)
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
