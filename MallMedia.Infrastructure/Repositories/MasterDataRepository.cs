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
}
