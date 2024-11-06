using AutoMapper;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MallMedia.Application.Devices.Command.CreateDevice
{
    public class CreateDeviceCommandHandler(ILogger<CreateDeviceCommandHandler> logger,IDevicesRepository devicesRepository, UserManager<User> userManager,IMapper mapper) : IRequestHandler<CreateDeviceCommand, int>
    {
        private const string DefaultPassword = "Password123!";
        public async Task<int> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creatting new devices with {@Device}", request);
            var user = new User
            {
                UserName = request.DeviceName,
                NormalizedUserName = request.DeviceName.ToUpper(),
                Email = null
            };
            var result = await userManager.CreateAsync(user, DefaultPassword);

            if (result.Succeeded) 
            {
                var device = mapper.Map<Device>(request);
                device.UserId = user.Id;
                device.Status = "Active";
                return await devicesRepository.CreateAsync(device);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
