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
                        || r.DeviceType.ToLower().Contains(search));
            //total items
            var totalCount = await baseQuery.CountAsync();
            // sort
            if (sortBy != null)
            {
                var columsSelector = new Dictionary<string, Expression<Func<Device, object>>>
                {
                    {nameof(Device.DeviceType),r=>r.DeviceType},
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

        public Task<Device> GetDeviceByIdAsync(int deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateDevicesAsync(Device entity)
        {
            dbContext.Devices.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }
    }
}
