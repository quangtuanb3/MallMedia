using MallMedia.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Commands
{
    public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, bool>
    {
        private readonly IDeviceRepository _deviceRepository;

        public UpdateDeviceCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<bool> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.DeviceId);

            if (device == null)
                return false;

            device.DeviceName = request.DeviceName;
            device.DeviceType = request.DeviceType;
            device.LocationId = request.LocationId;
            device.Status = request.Status;
            device.UpdatedAt = request.UpdatedAt;

            await _deviceRepository.UpdateAsync(device);

            return true;
        }
    }
}
