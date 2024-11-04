using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace MallMedia.Infrastructure.Seeders;

internal class CategorySeeder(ApplicationDbContext dbContext)
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Categories.Any())
            {
                dbContext.Categories.AddRange(CategoryData.Categories);
                dbContext.SaveChanges();
            }

        }
    }

}


