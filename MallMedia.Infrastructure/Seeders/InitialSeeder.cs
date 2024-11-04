

namespace MallMedia.Infrastructure.Seeders;
public interface IInitialSeeder
{
    Task Seed();
}
internal class InitialSeeder(CategorySeeder categorySeeder, LocationSeeder locationSeeder, DeviceSeeder deviceSeeder) : IInitialSeeder
{
    public async Task Seed()
    {
        await categorySeeder.Seed();
        await locationSeeder.Seed();
        await deviceSeeder.Seed();
    }
}
