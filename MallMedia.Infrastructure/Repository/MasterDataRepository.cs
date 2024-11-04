using MallMedia.Domain.Entities;
using MallMedia.Domain.Repository;
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Infrastructure.Repository;

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
