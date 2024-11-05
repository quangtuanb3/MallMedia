using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Commands
{
    public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, Device>
    {
        private readonly IDeviceRepository _deviceRepository;

        public UpdateDeviceCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<Device> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.DeviceUpdateDto.Id);
            if (device == null)
            {
                throw new Exception("Device not found.");
            }

            // Update device properties
            device.LocationId = request.DeviceUpdateDto.LocationId;
            device.DeviceType = request.DeviceUpdateDto.DeviceType;
            device.DeviceName = request.DeviceUpdateDto.DeviceName;
            device.Configuration.Id = request.DeviceUpdateDto.configuration.Id;
            device.Status = request.DeviceUpdateDto.Status;
            device.UpdatedAt = DateTime.UtcNow;

            // Save changes to the repository directly
            await _deviceRepository.UpdateAsync(device); // Assuming this method exists and handles saving the changes

            return device;
        }
    }
}
