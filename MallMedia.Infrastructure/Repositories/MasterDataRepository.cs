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

    public async Task<(List<FloorDeviceResult>, List<DepartmentDeviceResult>)> GetOptionSelectLocations(string deviceType)
    {
        if (!string.IsNullOrEmpty(deviceType))
        {
            // Get the distinct list of floors where the devices match the given deviceType
            var floors = await dbContext.Devices
                  .Where(d => d.Configuration.DeviceType.ToLower() == deviceType.ToLower())
                  .Join(dbContext.Locations,
                      d => d.LocationId,
                      l => l.Id,
                      (d, l) => new { l.Floor, d.Configuration.DeviceType })
                  .GroupBy(x => new { x.Floor, x.DeviceType })
                  .OrderBy(x => x.Key.Floor)
                  .Select(g => new FloorDeviceResult
                  {
                      Floor = g.Key.Floor,
                      DeviceType = g.Key.DeviceType
                  })
                  .ToListAsync();

            // Get the distinct list of departments where the devices match the given deviceType
            var departments = await dbContext.Devices
                       .Where(d => d.Configuration.DeviceType.ToLower() == deviceType.ToLower())
                       .Join(dbContext.Locations,
                           d => d.LocationId,
                           l => l.Id,
                           (d, l) => new { l.Department, d.Configuration.DeviceType })
                       .GroupBy(x => new { x.Department, x.DeviceType })
                       .OrderBy(x => x.Key.Department)
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
            var floors = await dbContext.Devices
                  .Where(d => d.Configuration.DeviceType == "Digital Poster" ||
                              d.Configuration.DeviceType == "LED" ||
                              d.Configuration.DeviceType == "Vertical LCD")
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

            // Get the distinct list of departments where the devices match the given deviceType
            var departments = await dbContext.Devices
                       .Where(d => d.Configuration.DeviceType == "Digital Poster" ||
                              d.Configuration.DeviceType == "LED" ||
                              d.Configuration.DeviceType == "Vertical LCD")
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
                        }).ToListAsync();
            return (floors, departments);  // Return both lists as a tuple
        }
    }
}

