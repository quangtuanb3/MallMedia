using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories;

public interface IMasterDataRepository
{
    Task<IEnumerable<Category>> GetAllCategories();
    Task<IEnumerable<Device>> GetAllDevices();
    Task<IEnumerable<Location>> GetAllLocations();
}
