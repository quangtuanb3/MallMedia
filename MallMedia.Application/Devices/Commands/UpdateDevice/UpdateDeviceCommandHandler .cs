using AutoMapper;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MallMedia.Application.Devices.Commands.UpdateDevice
{
    public class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, Device>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public UpdateDeviceCommandHandler(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        public async Task<Device> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.DeviceUpdateDto.Id);
            if (device == null)
            {
                throw new Exception("Device not found.");
            }
            _mapper.Map(request.DeviceUpdateDto, device);
            device.UpdatedAt = DateTime.UtcNow;
            await _deviceRepository.UpdateAsync(device);

            return device;
        }
    }
}
