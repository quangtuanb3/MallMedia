using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories
{
    public interface ILocationResponsitory
    {
        Task<List<Location>> GetLocationsAsync();
    }
}
