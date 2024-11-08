using MallMedia.Domain.Entities;

namespace MallMedia.Domain.Repositories
{
    public interface ITimeFramesRepository 
    {
        Task<List<TimeFrame>> GetAllTimeFramesAsync();
    }
}
