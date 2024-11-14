using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MallMedia.Infrastructure.Repositories;

internal class MasterDataRepository(ApplicationDbContext dbContext) : IMasterDataRepository
{
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await dbContext.Categories.ToListAsync();
    }

    public async Task<IEnumerable<Location>> GetAllLocations()
    {
        return await dbContext.Locations.ToListAsync();
    }

    public async Task<IEnumerable<Location>> GetLocations(int? floor, string department)
    {
        if (floor.HasValue && floor.Value > 0) 
        {
            return await dbContext.Locations.Where(l => l.Floor == floor).ToListAsync();
        }
        if (!string.IsNullOrWhiteSpace(department)) 
        {  
            return await dbContext.Locations.Where(l=>l.Department.ToLower().Equals(department.ToLower())).ToListAsync();
        }
        if((floor.HasValue && floor.Value > 0) && !string.IsNullOrWhiteSpace(department))
        {
            return await dbContext.Locations.Where(l => l.Department.ToLower().Equals(department.ToLower()) && l.Floor == floor).ToListAsync();
        }
        return await this.GetAllLocations();
    }

    public async Task<(List<FloorDeviceResult>, List<DepartmentDeviceResult>)> GetOptionSelectLocations(string[] deviceTypes)
    {
        if (deviceTypes != null && deviceTypes.Any())
        {
            // Get the distinct list of floors where the devices match any of the given deviceTypes
            var floors = await dbContext.Devices
                  .Where(d => deviceTypes.Contains(d.Configuration.DeviceType.ToLower()))  // Filter based on multiple deviceTypes
                  .Join(dbContext.Locations,
                      d => d.LocationId,
                      l => l.Id,
                      (d, l) => new { l.Floor, d.Configuration.DeviceType })
                  .GroupBy(x => new { x.Floor, x.DeviceType })
                  .OrderBy(x => x.Key.DeviceType)
                  .Select(g => new FloorDeviceResult
                  {
                      Floor = g.Key.Floor,
                      DeviceType = g.Key.DeviceType
                  })
                  .ToListAsync();

            // Get the distinct list of departments where the devices match any of the given deviceTypes
            var departments = await dbContext.Devices
                       .Where(d => deviceTypes.Contains(d.Configuration.DeviceType.ToLower()))  // Filter based on multiple deviceTypes
                       .Join(dbContext.Locations,
                           d => d.LocationId,
                           l => l.Id,
                           (d, l) => new { l.Department, d.Configuration.DeviceType })
                       .GroupBy(x => new { x.Department, x.DeviceType })
                       .OrderBy(x => x.Key.DeviceType)
                       .Select(g => new DepartmentDeviceResult
                       {
                           Department = g.Key.Department,
                           DeviceType = g.Key.DeviceType
                       })
                       .ToListAsync();

            return (floors, departments);  // Return both lists as a tuple
        }
        else
        {
            // Default to certain device types if deviceTypes is empty or null
            var defaultDeviceTypes = new[] { "Digital Poster", "LED", "Vertical LCD" };

            var floors = await dbContext.Devices
                  .Where(d => defaultDeviceTypes.Contains(d.Configuration.DeviceType))  // Use default device types
                  .Join(dbContext.Locations,
                      d => d.LocationId,
                      l => l.Id,
                      (d, l) => new { l.Floor, d.Configuration.DeviceType })
                  .GroupBy(x => new { x.Floor, x.DeviceType })
                  .OrderBy(x => x.Key.DeviceType)
                  .Select(g => new FloorDeviceResult
                  {
                      Floor = g.Key.Floor,
                      DeviceType = g.Key.DeviceType
                  })
                  .ToListAsync();

            // Get the distinct list of departments where the devices match the default deviceTypes
            var departments = await dbContext.Devices
                       .Where(d => defaultDeviceTypes.Contains(d.Configuration.DeviceType))  // Use default device types
                       .Join(dbContext.Locations,
                           d => d.LocationId,
                           l => l.Id,
                           (d, l) => new { l.Department, d.Configuration.DeviceType })
                       .GroupBy(x => new { x.Department, x.DeviceType })
                       .OrderBy(x => x.Key.DeviceType)
                       .Select(g => new DepartmentDeviceResult
                       {
                           Department = g.Key.Department,
                           DeviceType = g.Key.DeviceType
                       })
                       .ToListAsync();

            return (floors, departments);  // Return both lists as a tuple
        }
    }

}

