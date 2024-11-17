using AutoMapper;
using MallMedia.Domain.Constants;
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
            var id = -1;
            if (result.Succeeded) 
            {
                await userManager.AddToRoleAsync(user,UserRoles.User);
                var device = mapper.Map<Device>(request);
                device.Configuration.Size = device.Configuration.Size.ToString()+" inches";
                device.Configuration.DeviceType = request.DeviceType;
                device.UserId = user.Id;
                device.Status = "Active";
                id = await devicesRepository.CreateAsync(device);
                if(id == -1)
                {
                    await userManager.DeleteAsync(user);
                }
                return id;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
