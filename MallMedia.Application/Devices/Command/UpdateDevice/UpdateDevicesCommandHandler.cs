using AutoMapper;
using MallMedia.Domain.Exceptions;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Devices.Command.UpdateDevice
{
    public class UpdateDevicesCommandHandler(ILogger<UpdateDevicesCommandHandler>logger,IDevicesRepository devicesRepository,IMapper mapper) : IRequestHandler<UpdateDevicesCommand, int>
    {
        public async Task<int> Handle(UpdateDevicesCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updatting device with id : {@DevicesId} with {Devices} ", request.Id,request);
            
            var devices = await devicesRepository.GetByIdAsync(request.Id)
                    ?? throw new NotFoundException("Devices", request.Id.ToString());
            mapper.Map(request, devices);
            var id = await devicesRepository.UpdateDevicesAsync(devices);
            return id;
        }
    }
}
