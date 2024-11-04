using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace MallMedia.Infrastructure.Seeders;

internal class LocationSeeder(ApplicationDbContext dbContext)
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Locations.Any())
            {
                dbContext.Locations.AddRange(LocationData.Locations);
                dbContext.SaveChanges();
            }

        }
    }

}


