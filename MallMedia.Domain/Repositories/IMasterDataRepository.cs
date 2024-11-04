using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories;

public interface IMasterDataRepository
{
    Task<IEnumerable<Category>> getAllCategories();
    Task<IEnumerable<Location>> getAllLocations();
}
