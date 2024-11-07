
using MallMedia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MallMedia.Infrastructure.Seeders;
public interface IInitialSeeder
{
    Task Seed();
}
internal class InitialSeeder(
    CategorySeeder categorySeeder,
    LocationSeeder locationSeeder,
    DeviceSeeder deviceSeeder,
    TimeFrameSeeder timeFrameSeeder,
    UserSeeder userSeeder,
    IServiceProvider serviceProvider,
    ApplicationDbContext dbContext) : IInitialSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            await categorySeeder.Seed();
            await locationSeeder.Seed();
            await deviceSeeder.Seed();
            await timeFrameSeeder.Seed();
            await userSeeder.Seed(serviceProvider);
        }

    }
}
