using MallMedia.Domain.Entities;
using MallMedia.Domain.Repositories;
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Infrastructure.Repositories;

internal class MasterDataRepository(ApplicationDbContext dbContext) : IMasterDataRepository
{
    public async Task<IEnumerable<Category>> getAllCategories()
    {
        return await dbContext.Categories.ToListAsync();
    }

    public async Task<IEnumerable<Location>> getAllLocations()
    {
        return await dbContext.Locations.ToListAsync();
    }
}
