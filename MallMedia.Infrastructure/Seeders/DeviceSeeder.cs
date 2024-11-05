
using MallMedia.Domain.Entities;
using MallMedia.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MallMedia.Infrastructure.Seeders;


internal class DeviceSeeder
{
    private const string DefaultPassword = "Password123!";
    private readonly ApplicationDbContext dbContext;
    private readonly UserManager<User> userManager;

    public DeviceSeeder(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    public async Task Seed()
    {
        if (!dbContext.Devices.Any())
        {
            var devicesToAdd = new List<Device>();

            foreach (var device in DeviceData.Devices)
            {
                var user = new User
                {
                    UserName = device.DeviceName,
                    NormalizedUserName = device.DeviceName.ToUpper(),
                    Email = null
                };

                var result = await userManager.CreateAsync(user, DefaultPassword);

                if (result.Succeeded)
                {
                    device.UserId = user.Id;
                    devicesToAdd.Add(device);
                }
                else
                {
                    Console.WriteLine($"Error creating user for device {device.DeviceName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    Console.WriteLine(result.Errors);
                }
            }


            if (devicesToAdd.Any())
            {
                dbContext.Devices.AddRange(devicesToAdd);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
