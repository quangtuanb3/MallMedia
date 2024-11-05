using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _context;
        
        public DeviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Device> GetByIdAsync(int id)
        {
            return await _context.Devices.Include(d => d.Location)
                .Include(d => d.Configuration)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateAsync(Device device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }
    }
}
