using MallMedia.Domain.Constants;
using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MallMedia.Infrastructure.Repositories
{
    internal class DevicesRepository(ApplicationDbContext dbContext) : IDevicesRepository
    {
        public async Task<int> CreateAsync(Device entity)
        {
            try
            {
                bool exists = await dbContext.Devices.AnyAsync(d => d.DeviceName.Equals(entity.DeviceName));
                // Throw a specific exception if a match is found
                if (exists)
                {
                    throw new ArgumentException("A device with the name already exists.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            dbContext.Devices.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<(List<Device>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var search = searchPhrase?.ToLower();
            //query
            var baseQuery = dbContext.Devices.Include(r => r.Location)
                .Where(r => search == null || r.DeviceName.ToLower().Contains(search)
                        || r.Configuration.DeviceType.ToString().ToLower().Contains(search));
            //total items
            var totalCount = await baseQuery.CountAsync();
            // sort
            if (sortBy != null)
            {
                var columsSelector = new Dictionary<string, Expression<Func<Device, object>>>
                { 
                    {nameof(Device.DeviceName),r=>r.DeviceName},
                    {nameof(Device.Configuration.Resolution),r=>r.Configuration.Resolution},
                    {nameof(Device.Configuration.Size),r=>r.Configuration.Size},
                };
                var selectedColum = columsSelector[sortBy];
                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColum)
                    : baseQuery.OrderByDescending(selectedColum);
            }
            //pagination
            var devies = await baseQuery.Skip(pageSize * (pageNumber - 1))
                 .Take(pageSize).ToListAsync();
            return (devies, totalCount);
        }

        
        public Task<Device?> GetByIdAsync(int id)
        {
            return dbContext.Devices.Include(d => d.Location).Include(d => d.Schedules).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Device> GetByUserIdAsync(string userId)
        {
            return await dbContext.Devices.FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public async Task<int> UpdateDevicesAsync(Device entity)
        {
            try
            {
                bool exists = await dbContext.Devices.AnyAsync(d => d.DeviceName.Equals(entity.DeviceName));
                // Throw a specific exception if a match is found
                if (exists)
                {
                    throw new ArgumentException("A device with the name already exists.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            dbContext.Devices.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> CheckNameDevice(string name)
        {
            var device = await dbContext.Devices.FirstOrDefaultAsync(d=>d.DeviceName.Equals(name));
            if (device == null) return false;
            return true;
        }

        public async Task<List<Device>> GetByTypeAndFloorOrDepartmant(List<string> types, List<int>? floors, List<string>? departments)
        {
            if (floors != null && floors.Any())
            {
                var query = from d in dbContext.Devices
                            join l in dbContext.Locations on d.LocationId equals l.Id into deviceLocations
                            from l in deviceLocations.DefaultIfEmpty()  // Dùng DefaultIfEmpty() để mô phỏng LEFT JOIN
                            where floors.Contains(l.Floor) && types.Contains(d.Configuration.DeviceType)
                            select d;

                var result = await query.ToListAsync();
                return result;
            }
            if (departments != null && departments.Any())
            {
                var query = from d in dbContext.Devices
                            join l in dbContext.Locations on d.LocationId equals l.Id into deviceLocations
                            from l in deviceLocations.DefaultIfEmpty()  // Dùng DefaultIfEmpty() để mô phỏng LEFT JOIN
                            where departments.Contains(l.Department) && types.Contains(d.Configuration.DeviceType)
                            select d;

                var result = await query.ToListAsync();
                return result;
            }
            return null;
        }
    }
}
