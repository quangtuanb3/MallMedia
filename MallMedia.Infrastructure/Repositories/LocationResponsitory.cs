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
    internal class LocationResponsitory(ApplicationDbContext dbContext) : ILocationResponsitory
    {
        public async Task<List<Location>> GetLocationsAsync()
        {
            return await dbContext.Locations.ToListAsync();
        }
    }
}
